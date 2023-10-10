using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.Net;
using System.Text;

public class UDP
{
    private Socket _socket = null;

    public UDP()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    public void Send(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        _socket.SendTo(data, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7777));
    }

    public void Close()
    {
        _socket.Close();
    }

    public string Receive()
    {
        byte[] data = new byte[1024];
        int length = _socket.Receive(data);
        return Encoding.ASCII.GetString(data, 0, length);
    }
}
