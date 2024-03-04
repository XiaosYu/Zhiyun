from .core import NodeBase, nn, torch


class Operation(NodeBase):
    pass


class UnaryOperation(Operation):
    def forward(self, data):
        pass


class Flatten(UnaryOperation):
    def forward(self, data):
        return data.reshape(data.shape[0], -1)


class BinaryOperation(Operation):
    def forward(self, data1, data2):
        pass


class Addition(BinaryOperation):
    def forward(self, data1, data2):
        return torch.add(data1, data2)


class Concatenate(BinaryOperation):
    def __init__(self, ConnectedDim, **kwargs):

        super().__init__()

        self.dim = ConnectedDim

    def forward(self, data1, data2):
        return torch.cat([data1, data2], dim=self.dim)
