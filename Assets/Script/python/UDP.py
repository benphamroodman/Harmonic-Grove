import socket

UDP_IP = "127.0.0.1"
UDP_IP = "192.168.50.254"
UDP_LISTEN_PORT_DETECTED = 5005
UDP_LISTEN_PORT_MATERIAL = 5006
UDP_LISTEN_PORT_PITCH = 5007

print("UDP target IP:", UDP_IP)
print("UDP target port:", UDP_LISTEN_PORT_DETECTED)
print("UDP target port:", UDP_LISTEN_PORT_MATERIAL)
print("UDP target port:", UDP_LISTEN_PORT_PITCH)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)  # UDP


def send_message(message, port):
    sock.sendto(bytes(message, "utf-8"), (UDP_IP, port))


def close_connection():
    sock.close()
