using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;

public class SocketManger : MonoBehaviour
{
    string url = "http://203.237.200.136:8080/";
    public static Client Socket { get; private set; }

    void Awake()
    {
        Socket = new Client(url);
        Socket.Opened += SocketOpened;
        Socket.Connect();
        Debug.Log("소켓 연결됨");
    }

    private void SocketOpened(object sender, System.EventArgs e)
    {
        Debug.Log("Socket Opened");
    }

    void OnDisable()
    {
        Socket.Close();
    }
}
