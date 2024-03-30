import torch

from .core import NodeBase, nn


class Activate(NodeBase):
    def __init__(self):
        super().__init__()

        self.active = None

    def forward(self, data):
        return self.active(data)


class Factor(Activate):
    def __init__(self, InitialValue, Learnable):
        super().__init__()

        self.initial_value = nn.Parameter(torch.tensor([InitialValue]), requires_grad=Learnable)

    def forward(self, data):
        return self.initial_value * data


class BatchNormalization2D(Activate):
    def __init__(self, FeatureNumber, **kwargs):
        super().__init__()

        self.active = nn.BatchNorm2d(num_features=FeatureNumber)


class ReLU(Activate):
    def __init__(self, **kwargs):
        super().__init__()

        self.active = nn.ReLU()


class Sigmoid(Activate):
    def __init__(self, **kwargs):
        super().__init__()

        self.active = nn.Sigmoid()


class Dropout(Activate):
    def __init__(self, DropRate, **kwargs):
        super().__init__()

        self.active = nn.Dropout(p=DropRate)
