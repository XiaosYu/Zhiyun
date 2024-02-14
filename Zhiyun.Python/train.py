from utils.logger import Logger
from torch.cuda import get_device_properties, set_per_process_memory_fraction
from utils.parse import parse
from torch import load
from utils.dataset import RegressionSet, ClassificationSet
from torch.utils.data import DataLoader
from torch.autograd import Variable
from torch.nn import MSELoss, L1Loss, KLDivLoss, SmoothL1Loss, CrossEntropyLoss
from torch.optim import Adam, SGD, LBFGS
from torch import Tensor, no_grad, mean, save, argmax
from json import dumps
from os import makedirs

import matplotlib.pyplot as plt
import torch

CRITERIONS = {
    'MSELoss': MSELoss, 'L1LOSS': L1Loss, 'KLDivLoss': KLDivLoss, 'SmoothL1Loss': SmoothL1Loss,
    'CrossEntropyLoss': CrossEntropyLoss
}

OPTIMIZERS = {
    'Adam': Adam, 'SGD': SGD, 'LBFGS': LBFGS
}


def train(config):
    type = config['Type']
    if type == 'regression':
        regression(config)
    elif type == 'classification':
        classification(config)


def regression(config):
    # 获取通讯端口
    port = config["Port"]
    logger = Logger(port=port)
    # 等待从机连接
    logger.accept()
    # 限制显存
    device = config['HyperParameters']['Device']
    device = torch.device(device)
    memory = config['Memory']
    try:
        if device == 'cuda':
            device_info = get_device_properties(device)
            rate = (device_info.total_memory / 1024 / 1024) / memory
            set_per_process_memory_fraction(rate, device)
    except Exception as e:
        print(str(e))
        logger.error('内存分配失败')
    logger.info('分配内存成功')
    # 载入模型
    network = parse(config['Module'], config['Graph'])
    logger.info('载入模型成功')
    # 载入预训练参数
    pretrain = config['Pre-training']
    if pretrain != '':
        try:
            network.load_state_dict(load(pretrain))
        except:
            logger.error('载入预训练参数失败')
        logger.info('载入预训练参数成功')
    # 载入设备
    network = network.to(device)
    logger.info('载入设备成功')
    # 载入训练数据
    data = config['runs']
    train_set, test_set, eval_set = RegressionSet.create_dataset(data)
    logger.info(f'载入数据成功,训练集:{len(train_set)},测试集:{len(test_set)},验证集:{len(eval_set)}')
    # 载入loader
    batch_size = config['HyperParameters']['BatchSize']
    train_loader = DataLoader(train_set, batch_size=batch_size, shuffle=True)
    logger.info(f'载入训练Loader成功,batch-size；{batch_size}')
    # 载入损失函数优化器
    lr = config['HyperParameters']['LR']
    criterion = CRITERIONS[config['Criterion']]()
    optimizer = OPTIMIZERS[config['Optimizer']](network.parameters(), lr=lr)
    logger.info(f'载入优化器成功,学习率为{lr}')
    # 开始训练
    all_train_loss, all_eval_loss, all_test_loss = [], [], []
    epochs = config['HyperParameters']['Epochs']
    for epoch in range(epochs):
        train_loss = []
        if logger.state == 1:
            for (features, targets) in train_loader:
                def closure():
                    optimizer.zero_grad()
                    # 批数据载入
                    batch_x = Variable(features).to(device)
                    batch_y = Variable(targets).to(device)
                    # 前馈获得输出
                    batch_out = network(batch_x)
                    # 损失函数计算损失
                    loss = criterion(batch_out, batch_y)
                    # 反向传播优化参数
                    loss.backward()
                    return loss

                loss = optimizer.step(closure)
                train_loss.append(float(loss.data))
        else:
            logger.error('用户已停止训练')
        loss = mean(Tensor(train_loss))
        all_train_loss.append(loss)
        logger.train(epoch=epoch, loss=loss, type='train')
        # 是否需要验证
        if epoch % (epochs * 0.1) == 0 and len(eval_set) != 0:
            eval_loader = DataLoader(eval_set, batch_size=batch_size)
            eval_loss = []
            network.eval()
            with no_grad():
                for (features, targets) in eval_loader:
                    # 批数据载入
                    batch_x = Variable(features).to(device)
                    batch_y = Variable(targets).to(device)
                    # 前馈获得输出
                    batch_out = network(batch_x)
                    # 损失函数计算损失
                    loss = criterion(batch_out, batch_y)
                    eval_loss.append(float(loss))
            loss = mean(Tensor(eval_loss))
            all_eval_loss.append(loss)
            logger.train(epoch=epoch, loss=loss, type='eval')
            network.train()
    # 开始测试
    if len(test_set) != 0:
        test_loader = DataLoader(test_set, batch_size=int(len(test_set) * 0.1))
        network.eval()
        with no_grad():
            for (features, targets) in test_loader:
                test_loss = []
                # 批数据载入
                batch_x = Variable(features).to(device)
                batch_y = Variable(targets).to(device)
                # 前馈获得输出
                batch_out = network(batch_x)
                # 损失函数计算损失
                loss = criterion(batch_out, batch_y)
                test_loss.append(float(loss))
                loss = mean(Tensor(test_loss))
                all_test_loss.append(loss)
        loss = mean(Tensor(all_test_loss))
        logger.train(epoch=epochs, loss=loss, type='test')

    task_id = config['TaskID']
    dirs = f'runs/Run/Train/{task_id}'
    makedirs(dirs, exist_ok=True)
    # 保存参数文件
    save(network, f'{dirs}/weights.pt')
    # 保存训练信息
    with open(f'{dirs}/train.json', 'w') as f:
        f.write(dumps(config))
    # 画训练损失图
    plt.title('Epoch Train Loss')
    plt.plot(all_train_loss, label='Loss')
    plt.legend()
    plt.savefig(f'{dirs}/train_loss.jpg')
    plt.cla()
    # 画训练验证图
    if len(all_eval_loss) > 0:
        plt.title('Epoch Eval Loss')
        plt.plot(all_eval_loss, label='Loss')
        plt.legend()
        plt.savefig(f'{dirs}/eval_loss.jpg')
        plt.cla()

    # 画测试图
    if len(all_test_loss) > 0:
        plt.title('Test Loss')
        plt.plot(all_test_loss, label='Loss')
        plt.legend()
        plt.savefig(f'{dirs}/test_loss.jpg')
        plt.cla()

    logger.close()


def classification(config):
    # 获取通讯端口
    port = config["Port"]
    logger = Logger(port=port)
    # 等待从机连接
    logger.accept()
    # 限制显存
    device = config['HyperParameters']['Device']
    device = torch.device(device)
    memory = config['Memory']
    try:
        if device == 'cuda':
            device_info = get_device_properties(device)
            rate = (device_info.total_memory / 1024 / 1024) / memory
            set_per_process_memory_fraction(rate, device)
    except Exception as e:
        print(str(e))
        logger.error('内存分配失败')
    logger.info('分配内存成功')
    # 载入模型
    network = parse(config['Module'], config['Graph'])
    logger.info('载入模型成功')
    # 载入预训练参数
    pretrain = config['Pre-training']
    if pretrain != '':
        try:
            network.load_state_dict(load(pretrain))
        except:
            logger.error('载入预训练参数失败')
        logger.info('载入预训练参数成功')
    # 载入设备
    network = network.to(device)
    logger.info('载入设备成功')
    # 载入训练数据
    data = config['Data']
    train_set, test_set, eval_set = ClassificationSet.create_dataset(data)
    logger.info(f'载入数据成功,训练集:{len(train_set)},测试集:{len(test_set)},验证集:{len(eval_set)}')
    # 载入loader
    batch_size = config['HyperParameters']['BatchSize']
    train_loader = DataLoader(train_set, batch_size=batch_size, shuffle=True)
    logger.info(f'载入训练Loader成功,batch-size；{batch_size}')
    # 载入损失函数优化器
    lr = config['HyperParameters']['LR']
    criterion = CRITERIONS[config['Criterion']]()
    optimizer = OPTIMIZERS[config['Optimizer']](network.parameters(), lr=lr)
    logger.info(f'载入优化器成功,学习率为{lr}')
    # 载入验证,测试步长
    eval_step = config['EvalTimes']
    # 开始训练
    all_train_loss, all_eval_loss, all_test_loss = [], [], []
    all_train_accuracy, all_eval_accuracy, all_test_accuracy = [], [], []
    epochs = config['HyperParameters']['Epochs']
    for epoch in range(epochs):
        train_loss, train_accuracy = [], []
        if logger.state == 1:
            for (features, targets) in train_loader:
                def closure():
                    optimizer.zero_grad()
                    # 批数据载入
                    batch_x = Variable(features).to(device)
                    batch_y = Variable(targets).to(device).view(-1)
                    # 前馈获得输出
                    batch_out = network(batch_x)
                    # 损失函数计算损失
                    loss = criterion(batch_out, batch_y)
                    # 反向传播优化参数
                    loss.backward()
                    # 计算训练准确率
                    accuracy = float(mean((argmax(batch_out, dim=1) == batch_y).float()).data)
                    train_accuracy.append(accuracy)
                    return loss

                loss = optimizer.step(closure)
                train_loss.append(float(loss.data))
        else:
            logger.error('用户已停止训练')
        loss = float(mean(Tensor(train_loss)).data)
        accuracy = float(mean(Tensor(train_accuracy)).data)
        all_train_loss.append(loss)
        all_train_accuracy.append(accuracy)
        logger.train(epoch=epoch, loss=loss, accuracy=accuracy, type='train')
        # 是否需要验证
        if epoch % (epochs * 0.1) == 0 and len(eval_set) != 0:
            eval_loader = DataLoader(eval_set, batch_size=batch_size)
            eval_loss, eval_accuracy = [], []
            network.eval()
            with no_grad():
                for (features, targets) in eval_loader:
                    # 批数据载入
                    batch_x = Variable(features).to(device)
                    batch_y = Variable(targets).to(device).view(-1)
                    # 前馈获得输出
                    batch_out = network(batch_x)
                    # 损失函数计算损失
                    loss = criterion(batch_out, batch_y)
                    eval_loss.append(float(loss))
                    # 计算验证准确度
                    accuracy = float(mean((argmax(batch_out, dim=1) == batch_y).float()).data)
                    eval_accuracy.append(accuracy)
            loss = float(mean(Tensor(eval_loss)).data)
            accuracy = float(mean(Tensor(eval_accuracy)).data)
            all_eval_loss.append(loss)
            all_eval_accuracy.append(accuracy)
            logger.train(epoch=epoch, loss=loss, accuracy=accuracy, type='eval')
            network.train()
    # 开始测试
    if len(test_set) != 0:
        test_loader = DataLoader(test_set, batch_size=int(len(test_set) * 0.1))
        network.eval()
        with no_grad():
            for (features, targets) in test_loader:
                test_loss, test_accuracy = [], []
                # 批数据载入
                batch_x = Variable(features).to(device)
                batch_y = Variable(targets).to(device).view(-1)
                # 前馈获得输出
                batch_out = network(batch_x)
                # 损失函数计算损失
                loss = criterion(batch_out, batch_y)
                test_loss.append(float(loss))
                # 计算测试准确度
                accuracy = float(mean((argmax(batch_out, dim=1) == batch_y).float()).data)
                loss = mean(Tensor(test_loss))
                all_test_loss.append(loss)
                all_test_accuracy.append(accuracy)
        loss = float(mean(Tensor(all_test_loss)).data)
        accuracy = float(mean(Tensor(all_test_accuracy)).data)
        logger.train(epoch=epochs, loss=loss, accuracy=accuracy, type='test')

    task_id = config['TaskID']
    dirs = f'runs/Run/Train/{task_id}'
    makedirs(dirs, exist_ok=True)
    # 保存参数文件
    save(network, f'{dirs}/weights.pt')
    # 保存训练信息
    with open(f'{dirs}/train.json', 'w') as f:
        f.write(dumps(config))
    # 画训练损失图
    plt.title('Epoch Train Loss')
    plt.plot(all_train_loss, label='Loss')
    plt.legend()
    plt.savefig(f'{dirs}/train_loss.jpg')
    plt.cla()
    # 画训练准确率图
    plt.title('Epoch Train Accuracy')
    plt.plot(all_train_accuracy, label='Accuracy')
    plt.legend()
    plt.savefig(f'{dirs}/train_accuracy.jpg')
    plt.cla()
    # 画训练验证图
    if len(all_eval_loss) > 0:
        plt.title('Epoch Eval Loss')
        plt.plot(all_eval_loss, label='Loss')
        plt.legend()
        plt.savefig(f'{dirs}/eval_loss.jpg')
        plt.cla()

        plt.title('Epoch Eval Accuracy')
        plt.plot(all_eval_accuracy, label='Accuracy')
        plt.legend()
        plt.savefig(f'{dirs}/eval_accuracy.jpg')
        plt.cla()
    # 画测试图
    if len(all_test_loss) > 0:
        plt.title('Test Loss')
        plt.plot(all_test_loss, label='Loss')
        plt.legend()
        plt.savefig(f'{dirs}/test_loss.jpg')
        plt.cla()

        plt.title('Test Accuracy')
        plt.plot(all_test_accuracy, label='Accuracy')
        plt.legend()
        plt.savefig(f'{dirs}/test_accuracy.jpg')
        plt.cla()

    logger.close()
