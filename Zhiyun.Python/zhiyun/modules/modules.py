from .core import NodeBase, nn


class Module(NodeBase):
    pass


class Linear(NodeBase):
    def __init__(self, InFeatures, OutFeatures, Bias, **kwargs):
        super().__init__()

        self._linear = nn.Linear(in_features=InFeatures, out_features=OutFeatures, bias=Bias)

    def forward(self, data):
        return self._linear.forward(data)


class Convolution2D(NodeBase):
    def __init__(self, InChannels, OutChannels, KernelWidth, KernelHeight, Stride, Padding, Bias, **kwargs):
        super().__init__()

        self._conv2d = nn.Conv2d(in_channels=InChannels,
                                 out_channels=OutChannels,
                                 kernel_size=(KernelWidth, KernelHeight),
                                 stride=Stride,
                                 padding=Padding,
                                 bias=Bias)

    def forward(self, data):
        return self._conv2d.forward(data)
