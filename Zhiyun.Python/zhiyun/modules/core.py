from torch import nn
import torch


class NodeBase(nn.Module):
    def __init__(self):
        super().__init__()