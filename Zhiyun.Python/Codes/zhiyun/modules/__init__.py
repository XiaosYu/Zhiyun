from .core import NodeBase
from .modules import Linear, Module, Convolution2D
from .activates import ReLU, BatchNormalization2D, Dropout, Sigmoid, Factor
from .structures import ImageInput, ImageOutput, VectorInput, VectorOutput
from .operations import Addition, Concatenate, Flatten

__node_types__ = {}
local_dict = {**locals()}
for type_name in local_dict:
    if isinstance(local_dict[type_name], type):
        if issubclass(local_dict[type_name], NodeBase):
            __node_types__[type_name] = local_dict[type_name]


__class_template__ = """
class {0}(Module):
    def __init__({1}, *args, **kwargs):
        super(Module, self).__init__()
{2}

    def forward(self, data):
{3}
        return {4}       
"""

def build_net_text(net_name, net_properties, net_forwards):
    text = __class_template__.format(net_name,
                                     ', '.join(['self']),
                                     '\n'.join(map(lambda x: f"        {x}", net_properties)),
                                     '\n'.join(map(lambda x: f"        {x}", net_forwards)),
                                     net_forwards[-1].split(' ')[0])
    exec(text, globals(), locals())
    print(text)
    node_type = locals()[net_name]
    __node_types__[net_name] = node_type
    return node_type




