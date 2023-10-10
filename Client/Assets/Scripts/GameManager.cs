using MessagePack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(true)]
public class MyClass
{
    // Key attributes take a serialization index (or string name)
    // The values must be unique and versioning has to be considered as well.
    // Keys are described in later sections in more detail.
    [Key(0)]
    public int Age { get; set; }

    [Key(1)]
    public string FirstName { get; set; }

    [Key(2)]
    public string LastName { get; set; }

    // All fields or properties that should not be serialized must be annotated with [IgnoreMember].
    [IgnoreMember]
    public string FullName { get { return FirstName + LastName; } }
}

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
        //_udp.Send("Hello World!");

        MyClass myClass = new MyClass();
        myClass.Age = 20;
        myClass.FirstName = "John";
        myClass.LastName = "Doe";

        byte[] myClassBytes = MessagePackSerializer.Serialize(myClass);
        MyClass myClass2 = MessagePackSerializer.Deserialize<MyClass>(myClassBytes);

        Debug.Log(myClass2.Age);
        Debug.Log(myClass2.FullName);
    }
}

