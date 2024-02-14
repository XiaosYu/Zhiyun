from torch.nn import Module


class InLayer(Module):
    def __init__(self):
        super(InLayer, self).__init__()

    def forward(self, data):
        return data


class OutLayer(Module):
    def __init__(self):
        super(OutLayer, self).__init__()

    def forward(self, data):
        return data


class Add(Module):
    def __init__(self):
        super(Add, self).__init__()

    def forward(self, data1, data2):
        return data1 + data2


class View(Module):
    def __init__(self, shape):
        super(View, self).__init__()
        self.shape = list(map(lambda x: int(x), shape.split(',')))

    def forward(self, data):
        return data.reshape(*self.shape)


class Residuals(Module):
    def __init__(self, weights, shortcut, active):
        super(Residuals, self).__init__()
        self.weights = weights
        self.shortcut = shortcut
        self.active = active

    def forward(self, data):
        return self.active(self.weights(data) + self.shortcut(data))

