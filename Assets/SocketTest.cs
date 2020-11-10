using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SocketTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SocketManger.Socket.On("MsgRes", (data) =>
        {
            string json = JsonConvert.SerializeObject(data.Json.args[0]);
            Debug.Log(data.Json.args[0].GetType());
            Debug.Log(json);
            Debug.Log(json.GetType());

            Data_Move_x_y response = JsonUtility.FromJson<Data_Move_x_y>(json);
            Debug.Log(response.x);
            Debug.Log(response.y);
        });
    }

    void OnGUI()
    {
        /*if (GUI.Button(new Rect(10, 10, 150, 100), "SEND"))
        {
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
        }*/
    }
}
public class Data_Move_x_y
{
    public float x;
    public float y;
}
