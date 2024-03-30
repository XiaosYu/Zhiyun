import socket
import _thread

import zhiyun.utils as utils

class LongNetConnectionContext:
    def __init__(self, port):
        self.server: socket.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

        self.server.bind(('0.0.0.0', port))
        self.server.listen(1)
        self.client: socket.socket = None
        self.running = False
        self.on_receive_event_callback = []

    def wait_connect(self):
        client, _ = self.server.accept()
        self.client = client
        self.running = True
        _thread.start_new_thread(self.receive_message, (self, ))

    def send_message(self, obj):
        data_bytes = utils.json_dumps(obj).encode('utf-8')
        data_bytes_length = len(data_bytes).to_bytes(4, 'big')
        self.client.send(data_bytes_length)
        self.client.send(data_bytes)

    def receive_message(self):
        while self.running:
            try:
                data_bytes_length = int.from_bytes(self.client.recv(4), 'big')
                data_bytes = self.client.recv(data_bytes_length).decode('utf-8')
                obj = utils.json_loads(data_bytes)
                for callback in self.on_receive_event_callback:
                    callback(obj)
            except:
                pass
