from torch.nn import Linear, Sequential, Sigmoid, Module, Softmax
from json import loads
from random import randint
from models.component import InLayer, OutLayer, Residuals, Add

MODULES_DICTS = {
    'Linear': Linear, 'Sequential': Sequential, 'Sigmoid': Sigmoid, 'Softmax': Softmax,
    'InLayer': InLayer, 'OutLayer': OutLayer, 'Residuals': Residuals, 'Add': Add
}

MODULE_TEMPLATE_HEADER = '''
class NeuralNetwork(Module):
    def __init__(self, {}):
        super(NeuralNetwork, self).__init__()
'''
MODULE_TEMPLATE_FORWARD = '''
    def forward(self, data):
'''


def parse(modules, graphs) -> Module:
    def generate_field_name():
        source = 'qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM'
        return ''.join(source[randint(0, len(source) - 1)] for idx in range(10))

    def generate_weights(module):
        if module['Type'] == 'Sequential':
            layer = MODULES_DICTS[module['Type']]()
            for son in module['Parameters']['Layers']:
                son_layer = generate_weights(son)
                layer.append(son_layer)
            return layer
        else:
            layer = MODULES_DICTS[module['Type']](**module['Parameters'])
            return layer

    # 生成字段名
    fields = list(generate_field_name() for idx in range(len(graphs)))
    # 写入类头
    module_code = MODULE_TEMPLATE_HEADER.format(','.join(fields))
    # 写入字段文件
    for field in fields:
        module_code += f'        self.{field} = {field}\n'
    # 生成前馈函数头
    module_code += MODULE_TEMPLATE_FORWARD
    fields = list(zip(fields, graphs))
    # 前馈临时变量表
    forwards = []
    # 生成前馈函数
    for (field, graph) in fields:
        name = generate_field_name()
        forwards.append(name)
        if graph['Input'] is None:
            # 如果是输入层
            module_code += f'        {name} = self.{field}.forward(data)\n'
        elif graph['Output'] is None:
            # 如果是输出层
            last_name = forwards[graph['Input'][0]]
            module_code += f'        {name} = self.{field}.forward({last_name})\n'
        else:
            # 普通层级映射
            if len(graph['Input']) == 1:
                last_name = forwards[graph['Input'][0]]
                module_code += f'        {name} = self.{field}.forward({last_name})\n'
            if len(graph['Input']) != 1:
                # 多输入层
                last_names = ','.join(map(lambda x: forwards[x], graph['Input']))
                module_code += f'        {name} = self.{field}.forward({last_names})\n'
    module_code += f'        return {forwards[-1]}\n'
    print(module_code)
    # 生成解析模型
    model_dict = {}
    exec(module_code, model_dict, model_dict)
    # 产生模型参数
    modules = list(map(lambda x: generate_weights(x), modules))
    module = model_dict['NeuralNetwork'](*modules)
    return module
