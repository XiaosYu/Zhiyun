from zhiyun import SyntaxHandler
from zhiyun.utils import json_loads

import torch

with open('network.zyn', 'r', encoding='utf-8') as f:
    data = json_loads(f.read())['Nodes']
converter = SyntaxHandler()
model_type = converter.handle('TestNet', data)

model = model_type()

data = torch.rand(16, 1, 28, 28)
out = model(data)
print(model)
print(out.shape)
