using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }

    private UDP _udp = null;

    [SerializeField]
    private GameObject _playerPrefab = null;

    private void Awake()
    {
        _instance = this;

        _udp = new UDP();
    }

    private void Start()
    {

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
