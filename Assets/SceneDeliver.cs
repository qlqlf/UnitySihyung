using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//로그인 씬에서 게임 씬으로 넘어갈 때 파괴되지 않고 데이터 넘겨주는 스크립트
public class SceneDeliver : MonoBehaviour
{
    public static SceneDeliver sceneDeliver;//스크립트를 바로 사용할 수 있게 static으로 선언.
    public UserInfo playerInfo = new UserInfo();//RoomScript의 UserInfo클래스를 가져옴.
    public string userID;//사용자의 ID를 따로 저장.
    //start보다 빠르게 선언.
    private void Awake()
    {
        sceneDeliver = this;//이 스크립트를 불러오고
        DontDestroyOnLoad(gameObject);//씬이 변경되도 파괴되지 않게 설정.
    }
    // Start is called before the first frame update
    void Start()
    {

    }
}
