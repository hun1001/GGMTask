using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UDP
{
    private Socket _socket = null;

    public UDP()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    public void Send(string message)
    {
        byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
        _socket.Send(data);
    }

    public void Close()
    {
        _socket.Close();
    }

    public void Connect(string ip, int port)
    {
        _socket.Connect(ip, port);
    }

    public string Receive()
    {
        byte[] data = new byte[1024];
        int length = _socket.Receive(data);
        return System.Text.Encoding.ASCII.GetString(data, 0, length);
    }

    public bool IsConnected()
    {
        return _socket.Connected;
    }
}
