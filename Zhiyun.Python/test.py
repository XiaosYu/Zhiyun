from socket import socket

client = socket()
client.connect(('127.0.0.1', 5888))
print('连接成功')

while True:
    msg = str(client.recv(4096), encoding='utf-8')
    print(msg)
    if 'End' in msg:
        client.close()
        break