using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomScript : MonoBehaviour
{
    public Text RoomName, StartCount, UserName;

    public void JoinBtnclick()
    {
        Debug.Log("join");
        /*
        var data = new Data_Move_x_y();
        //Move_x_y move_x_y = new Move_x_y();
        //move_x_y.set_x_y(10,12);
        data.x = 10;
        data.y = 12;
        string message = JsonUtility.ToJson(data, prettyPrint: true);
        //Debug.Log (message);
        // string data = move_x_y.SaveToString();

        // socket.EmitJson("move_friend", data);
        SocketManger.Socket.Emit("Msg", message);
        //SocketManager.Socket.Emit("Msg", "Hello, World!");
        */
    }


}
