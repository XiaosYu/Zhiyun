from .core import NodeBase, nn


class Input(NodeBase):
    def forward(self, data):
        return data


class Output(NodeBase):
    def forward(self, data):
        return data


class ImageInput(Input):
    def __init__(self, **kwargs):
        super(ImageInput, self).__init__()


class ImageOutput(Output):
    def __init__(self, **kwargs):
        super(ImageOutput, self).__init__()


class VectorInput(Input):
    def __init__(self, **kwargs):
        super(VectorInput, self).__init__()


class VectorOutput(Output):
    def __init__(self, **kwargs):
        super(VectorOutput, self).__init__()
