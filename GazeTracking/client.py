import socket

client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

IP="127.0.0.1"
PORT=8080

client.connect((IP, PORT))

while True:
    data=client.recv(1024)
    print("Ответ сервера: "+data.decode("utf-8"))
    client.send(input("Отправить: ").encode("utf-8"))