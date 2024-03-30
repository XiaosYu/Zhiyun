from .utils import json_loads
from torch.optim import SGD, Adam

def load_options(options_text):
    options = json_loads(options_text)
    

