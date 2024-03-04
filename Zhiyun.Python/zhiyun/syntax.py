from .modules import __node_types__, build_net_text


class ParameterHandler:
    def __init__(self):
        pass

    def handle(self, parameter_messages: list):
        parameters = {}
        for parameter in parameter_messages:
            parameter_name = parameter["Name"]
            parameters[parameter_name] = parameter['Value']
        return parameters


class NodeHandler:
    def handle(self, node_message):
        node_type = node_message['Type']
        node_name = node_message['Name']
        parameters = node_message['Parameters']

        monolithic = None
        if node_type == 'CustomModule':
            monolithic = list(filter(lambda x: x['Type'] == 'Monolithic', parameters))[0]
            node_type = monolithic['Id']

        # 分析节点类型来解析节点
        parameters_convert = ParameterHandler()

        # 去除Monolithic
        parameters = list(filter(lambda x: x['Type'] != 'Monolithic', parameters))

        if node_type in __node_types__:
            node_parameters = parameters_convert.handle(parameters)
            return f"self.{node_name} = __node_types__['{node_type}']({', '.join(map(lambda x: f'{x[0]}={x[1]}', node_parameters.items()))})"

        elif monolithic is not None:

            nodes = monolithic['Value']['Nodes']
            node_parameters = parameters_convert.handle(parameters)

            syntax = SyntaxHandler()
            syntax.handle(monolithic['Id'], nodes)

            return f"self.{node_name} = __node_types__['{node_type}']({', '.join(map(lambda x: f'{x[0]}={x[1]}', node_parameters.items()))})"


class SyntaxHandler:
    class NodeStorager:
        def __init__(self):
            self.node_inputs = {}
            self.node_outputs = {}
            self.node_names = {}

            self.node_expressions = {}
            self.connected_expressions = []

        def append_node(self, node_id, node_name, node_expression, node_input, node_output):
            self.node_inputs[node_id] = node_input
            self.node_outputs[node_id] = node_output
            self.node_names[node_id] = node_name
            self.node_expressions[node_id] = node_expression

        def append_connected(self, connected):
            self.connected_expressions.append(connected)

        def __getitem__(self, node_id):
            return self.node_names[node_id], self.node_inputs[node_id], self.node_outputs[node_id]

    def __init__(self):
        pass

    def handle(self, name, nodes):

        storager = SyntaxHandler.NodeStorager()

        # 分解单个模块,对模块进行转换,转换为self.[PropertyName] = [ModuleName]([ModuleParameter])
        node_handler = NodeHandler()
        input_node_id = None
        for node in nodes:
            node_id = node['Id']
            node_name = node['Name']
            node_type = node['Type']
            if 'Input' in node_type:
                input_node_id = node_id
            node_inputs = node['Inputs']
            node_outputs = node['Outputs']

            node_expressions = node_handler.handle(node)
            storager.append_node(node_id, node_name, node_expressions, node_inputs, node_outputs)

        if input_node_id is None:
            raise Exception(f'{nodes}中未发现Input节点')

        # 构造可回溯表
        ids = [node_id for node_id in storager.node_inputs.keys()]
        requests = [inputs for inputs in storager.node_inputs.values()]

        records = []
        records_idx = []

        def backtracking(idx):
            if idx in records_idx:
                return
            request = requests[idx]
            for r in request:
                if request not in records:
                    backtracking(ids.index(r))

            if ids[idx] != input_node_id:
                storager.append_connected(
                    f"data_{ids[idx]} = self.{storager.node_names[ids[idx]]}.forward({', '.join(map(lambda x: f'data_{x}', request))})")
            else:
                storager.append_connected(
                    f"data_{ids[idx]} = self.{storager.node_names[ids[idx]]}.forward(data)")

            records.append(ids[idx])
            records_idx.append(idx)

        for i in range(len(ids)):
            backtracking(i)

        net_type = build_net_text(name, storager.node_expressions.values(), storager.connected_expressions)
        return net_type
