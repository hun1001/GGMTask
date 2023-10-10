using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }

    private UDP _udp = null;

    private void Awake()
    {
        _instance = this;

        _udp = new UDP();
    }

    private void Start()
    {
        _udp.Bind(12345);
    }

    private void Update()
    {
        if(!_udp.IsConnected())
        {
            return;
        }

        string message = _udp.Receive();
        Debug.Log(message);
    }
}
