from torch import max, min, mean, var


class PreProcess:
    def __init__(self):
        pass

    def forward(self, data):
        pass


class Compose(PreProcess):
    def __init__(self, *composes):
        super(Compose, self).__init__()

        self.composes = []
        self.composes.extend(composes)

    def forward(self, data):
        for compose in self.composes:
            data = compose.forward(data)
        return data

    def add(self, compose):
        self.composes.append(compose)


class LinearNormalize(PreProcess):
    def __init__(self):
        super(LinearNormalize, self).__init__()
        pass

    def forward(self, data):
        fmax, fmin = max(data), min(data)
        data = (data - fmin) / (fmax - fmin)
        return data


class StandardNormalize(PreProcess):
    def __init__(self):
        super(StandardNormalize, self).__init__()
        pass

    def forward(self, data):
        fstd = mean(data)
        fvar = var(data)
        data = (data - fstd) / fvar
        return data


class MaximumNormalize(PreProcess):
    def __init__(self):
        super(MaximumNormalize, self).__init__()
        pass

    def forward(self, data):
        fmax = max(data)
        data = data / fmax
        return data

