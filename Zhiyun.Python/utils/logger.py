from socket import socket
from datetime import datetime
from _thread import start_new_thread
from json import loads


class Logger:

    def __init__(self, port):
        server = socket()
        server.bind(('0.0.0.0', port))
        server.listen(5)
        self.server = server
        self.connect = None
        self.state = 0

    def accept(self):
        connect, address = self.server.accept()
        self.connect = connect
        self.state = 1

        def recv():
            try:
                with True:
                    command = str(self.connect.recv(4096), encoding='utf-8')
                    command = loads(command)
                    self.on_command(command)
            except:
                pass
        start_new_thread(recv, ())

    def on_command(self, command):
        cmd = command['Type']
        args = command['Arguments']
        if cmd == 'STOP':
            self.state = 0
        elif cmd == 'RUN':
            self.state = 1

    def log(self, type, text):
        msg = str({
            'DateTime': str(datetime.now()),
            'Type': type,
            'Content': text
        })
        self.connect.send(bytes(msg, 'utf-8'))

    def info(self, text):
        self.log('Info', text)

    def error(self, text):
        self.log('Error', text)
        self.connect.close()
        self.server.close()
        raise Exception()

    def warning(self, text):
        self.log('Warning', text)

    def train(self, **kwargs):
        self.log('Train', kwargs)

    def close(self):
        self.log('End', '')
        self.connect.close()
        self.server.close()
