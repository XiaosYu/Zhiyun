import pandas as pd
import torch

from torch.utils.data import Dataset
from torchvision.datasets import ImageFolder, mnist
from torchvision.transforms import Compose, Resize


class ImageFolderDataset(Dataset):
    def __init__(self, path, image_width, image_height):

        transform = Compose([
            Resize((image_width, image_height))
        ])

        self.dataset = ImageFolder(root=path, transform=transform)

    def __len__(self):
        return len(self.dataset)

    def __getitem__(self, idx):
        return self.dataset[idx]


class MnistDataset(Dataset):
    def __init__(self, path, download, train=True):
        self.dataset = mnist.MNIST(path, download=download, train=True)

    def __len__(self):
        return len(self.dataset)

    def __getitem__(self, item):
        return self.dataset[item]