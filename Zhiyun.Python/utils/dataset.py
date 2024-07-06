from torch.utils.data import Dataset
from pandas import read_csv
from torch import from_numpy
from utils.preprocess import Compose, MaximumNormalize, LinearNormalize, StandardNormalize

PREPROCESSES = {
    'MaximumNormalize': MaximumNormalize, 'LinearNormalize': LinearNormalize, 'StandardNormalize': StandardNormalize
}


class RegressionSet(Dataset):

    @staticmethod
    def create_dataset(data):
        path = data['Path']
        train, test, eval = data['TrainRate'], data['TestRate'], data['EvalRate']
        features, targets = data['Features'], data['Targets']
        source = read_csv(path, header=None, sep='\t')
        source = source.to_numpy().astype('float')
        source = from_numpy(source).float()
        # 载入预处理
        features_compose = Compose()
        for deal in data['Preprocess']['Features']:
            features_compose.add(PREPROCESSES[deal]())
        targets_compose = Compose()
        for deal in data['Preprocess']['Targets']:
            targets_compose.add(PREPROCESSES[deal]())

        count = len(source)
        train, test, eval = count * train, count * test, count * eval
        train, test, eval = int(train), int(train + test), int(train + test + eval)
        return RegressionSet(source[: train], features, targets, features_compose, targets_compose), \
            RegressionSet(source[train: test], features, targets, features_compose, targets_compose), \
            RegressionSet(source[test:], features, targets, features_compose, targets_compose)

    def __init__(self, source, features, targets, features_transformer, targets_transformer):
        super(RegressionSet, self).__init__()

        self.features = source[:, features]
        self.targets = source[:, targets]
        self.features = features_transformer.forward(self.features)
        self.targets = targets_transformer.forward(self.targets)

    def __len__(self):
        return len(self.features)

    def __getitem__(self, idx):
        return self.features[idx].reshape(-1).clone(), self.targets[idx].reshape(-1).clone()


class ClassificationSet(Dataset):
    @staticmethod
    def create_dataset(data):
        path = data['Path']
        train, test, eval = data['TrainRate'], data['TestRate'], data['EvalRate']
        features, targets = data['Features'], data['Targets']
        source = read_csv(path, header=None, sep='\t')
        source = source.to_numpy().astype('float')
        source = from_numpy(source).float()
        # 载入预处理
        features_compose = Compose()
        for deal in data['Preprocess']['Features']:
            features_compose.add(PREPROCESSES[deal]())

        count = len(source)
        train, test, eval = count * train, count * test, count * eval
        train, test, eval = int(train), int(train + test), int(train + test + eval)
        return ClassificationSet(source[: train], features, targets, features_compose), \
            ClassificationSet(source[train: test], features, targets, features_compose), \
            ClassificationSet(source[test:], features, targets, features_compose)

    def __init__(self, source, features, targets, features_transformer):
        super(ClassificationSet, self).__init__()

        self.features = source[:, features]
        self.targets = source[:, targets].long()
        self.features = features_transformer.forward(self.features)

    def __len__(self):
        return len(self.features)

    def __getitem__(self, idx):
        return self.features[idx].reshape(-1).clone(), self.targets[idx].reshape(-1).clone()
