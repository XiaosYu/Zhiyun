from typing import Tuple

from zhiyun.utils import json_loads, set_seed

from torch.optim import SGD, Adam
from torch.nn import MSELoss, CrossEntropyLoss
from torch.utils.data import Dataset, DataLoader, random_split
import torch

from zhiyun import SyntaxHandler, LongNetConnectionContext
from zhiyun import MnistDataset

__optimizers__ = {'SgdOptimizer': SGD, 'AdamOptimizer': Adam}
__criterions__ = {'MESLoss': MSELoss, 'CrossEntropyLoss': CrossEntropyLoss}
__datasets__ = {'mnist': MnistDataset}


class Error:
    def __init__(self, code, message):
        self.Code = code
        self.Message = message


class TrainOptions:
    def __init__(self):
        self.task_id: str = None
        self.task_name: str = None
        self.optimizer: dict = None
        self.criterion: dict = None
        self.batch_size: int = None
        self.device: str = None
        self.model: type = None
        self.port: int = None
        self.seed: int = None
        self.epochs: int = None
        self.task_type: str = None
        self.dataset: Tuple[Dataset, Dataset] = None

        # Runtime
        self.train_status: bool = False


def initialize(options_text):
    try:
        json_options = json_loads(options_text)
    except:
        return Error(401, 'Raise Error On Unpack Options Text')
    options = TrainOptions()

    try:
        options.device = json_options['Device']
        options.batch_size = json_options['BatchSize']
        options.task_id = json_options['TaskId']
        options.task_name = json_options['TaskName']
        options.port = json_options['Port']
        options.seed = json_options['Seed']
        options.epochs = json_options['Epochs']
    except:
        return Error(402, 'Raise Error On Loading(Device,BatchSize,TaskID,TaskName,Port)')

    try:
        json_criterion = json_options['Criterion']
        options.criterion = [__criterions__[json_criterion['Name']], json_criterion['Parameters']]
        json_optimizer = json_options['Optimizer']
        options.optimizer = [__optimizers__[json_optimizer['Name']], json_optimizer['Parameters']]
    except:
        return Error(403, 'Raise Error On Loading(Criterion,Optimizer)')

    try:
        json_model = json_loads(json_options['ModuleMessage'])
        handler = SyntaxHandler()
        model = handler.handle(json_model['Name'], json_model['Monolithic']['Nodes'])
        options.model = model
    except:
        return Error(404, 'Raise Error On Handle Model')

    try:
        task_type = json_options['TaskType']
        dataset_path = json_options['DatasetPath']
        dataset_type = json_options['DatasetType']
        dataset = __datasets__[dataset_type](path=dataset_path)
        train_set, eval_set = random_split(dataset, [0.8, 0.2])
        options.dataset = train_set, eval_set
        options.task_type = task_type
    except:
        return Error(405, 'Raise Error On Handle Dataset')

    return options


def train(options: TrainOptions):
    options.train_status = True

    def callback(event):
        if event['EventType'] == 'CLOSE':
            network.running = False
            options.train_status = False

    set_seed(options.seed)

    network = LongNetConnectionContext(options.port)
    network.on_receive_event_callback.append(callback)

    network.wait_connect()

    device = torch.device(options.device)

    model: torch.nn.Module = options.model()
    criterion = options.criterion[0](**options.criterion[1])
    optimizer = options.optimizer[0](model.parameters(), **options.optimizer[1])

    network.send_message({
        'code': 200,
        'content': {
            'message': f'initial the model at {str(model)}, with criterion {str(criterion)} and optimizer {str(optimizer)}'
        }
    })

    train_set, eval_set = options.dataset
    train_loader, eval_loader = DataLoader(train_set, batch_size=options.batch_size, shuffle=True), DataLoader(eval_set,
                                                                                                               batch_size=options.batch_size,
                                                                                                               shuffle=True)

    def calibrate(prediction, validation):
        return torch.nonzero(prediction.cpu().detach() == validation.cpu().detach()).sum()

    def forward(batch_x, batch_y, eval, calibrate=None):
        calibration = None
        if not eval:
            batch_out = model(batch_x)
            batch_loss = criterion(batch_out, batch_y)

            if calibrate is not None:
                calibration = calibrate(batch_out, batch_y)

            batch_loss.backward()
            optimizer.step()
            optimizer.zero_grad()
        else:
            with torch.no_grad():
                batch_out = model(batch_x)

                if calibrate is not None:
                    calibration = calibrate(batch_out, batch_y)

                batch_loss = criterion(batch_out, batch_y)

        return batch_loss, calibration

    def save_checkpoint():
        run_message = {
            'epoch': epoch
        }

    network.send_message({
        'code': 201,
        'content': {
            'dataset': options.dataset.__name__,
            'batch_size': options.batch_size,
            'train_length': len(train_set),
            'eval_length': len(eval_set),
            'max_train_iteration': len(train_loader),
            'max_eval_iteration': len(eval_loader)
        }
    })

    model = model.to(device)
    for epoch in range(options.epochs):
        if not options.train_status:
            break

        model.train()
        for idx, (features, targets) in enumerate(train_loader):
            loss, calibration = forward(features.to(device), targets.to(device), False, calibrate)
            network.send_message({
                'code': 202,
                'content': {
                    'epoch': epoch,
                    'status': 'train',
                    'iteration': idx,
                    'loss': float(loss),
                    'calibration': float(calibration)
                }
            })

        model.eval()
        for idx, (features, targets) in enumerate(train_loader):
            loss, calibration = forward(features.to(device), targets.to(device), True, calibrate)
            network.send_message({
                'code': 203,
                'content': {
                    'epoch': epoch,
                    'status': 'eval',
                    'iteration': idx,
                    'loss': float(loss),
                    'calibration': float(calibration)
                }
            })

        save_checkpoint()
