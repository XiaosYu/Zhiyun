import pandas as pd
import torch

from abc import ABCMeta, abstractmethod


class ResourceLoader(metaclass=ABCMeta):
    @abstractmethod
    def load(self) -> torch.Tensor:
        pass


class TableLoader(ResourceLoader, metaclass=ABCMeta):
    def __init__(self, df: pd.DataFrame, columns):
        super().__init__()

        self.df = df
        self.columns = columns

    def load(self) -> torch.Tensor:
        data = self.df[self.columns]
        return torch.from_numpy(data.to_numpy().copy())


class CsvLoader(TableLoader):
    def __init__(self, filename, columns, seq=','):
        df = pd.read_csv(filename, sep=seq)
        super().__init__(df, columns)


class ImageFolderLoader(ResourceLoader):
    def __init__(self, folder_name):
        self.folder = folder_name

    def load(self) -> torch.Tensor:
        pass
