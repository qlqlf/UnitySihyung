using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI사용
using Newtonsoft.Json;//Json 통신 사용(소켓활용)
using UnityEngine.SceneManagement;//씬 전환 사용

public class RoomScript : MonoBehaviour
{
    //PlayMenu의 Roomname, Usercount Text지정.
    public Text RoomName_Play, UserCount1, UserCount2;
    public InputField Name_text, ID_text;//Nickname, userID InputField 지정.
    //JoinMenu의 Roomname, Startcount Text지정.
    public Text RoomName_Join, StartCount;
    //user들의 캐릭터 Image, Text와 각 자리의 Panel image를 배열로 지정.
    public Image[] UserCharacter = new Image[5];
    public Text[] UserName = new Text[5];
    public Image[] UserPannel = new Image[5];
    //임시로 유저의 정보를 저장할 배열 선언.
    public static string[] input_userName = new string[5];//유저의 이름
    public static int[] input_userCharacter = new int[5];//유저의 캐릭터
    public static bool[] UserReadycount = new bool[5];//유저의 ready여부
    //inspecter창에서 직접넣는 가변적인 sprite배열 생성.
    [SerializeField]
    private Sprite[] sprites;
    //Ready버튼을 누른 유저의 수(ReadyCount), 방에 접속한 유저의 수(length), 접속한 유저들 중 Ready버튼을 누른 유저의 수(length2)선언.
    public static int ReadyCount = 0, length = 0, length2 = 0;//각각 0으로 초기화.
    //Room1과 Room2의 string값 "?(점속한 유저의 수)/5"을 저장할 변수 선언.
    public static string usercount1="", usercount2="";//각각 초기화
    //Play버튼이 눌렸는지 확인할 변수(isPlayBtnClick), 서버에서 받은 게임시작 정보를 알려줄 변수(GameStart)선언.
    public static bool isPlayBtnClick = false, GameStart = false;//각각 false로 초기화.
    //게임을 시작할 때(소켓통신의 경우 시작과 함께 연결해야하기 때문에 stat에 쓴다.)
    void Start()
    {
        //소켓서버에서 데이터를 받을 때
        SocketManger.Socket.On("resWaitingRoom", (data) => //"resWaitingRoom" 이벤트(PlayMenu의 각 방의 참여자 수)의 데이터
        {
            //서버로부터 데이터를 받기 위한 변수 선언.
            string json = JsonConvert.SerializeObject(data.Json.args[0]);
            //서버의 데이터를 받기 위한 클래스 선언
            RoomUserCount response_room = JsonUtility.FromJson<RoomUserCount>(json);//RoomUserCount 클래스에 데이터 저장.
            //전역 변수에 각 데이터 저장.
            usercount1 = response_room.room1 + "/5";
            usercount2 = response_room.room2 + "/5";
            isPlayBtnClick = true;//FixedUpdate문의 if문을 동작시키기 위한 isPlayBtnClick(boolean) True로 변경.
        });
        //소켓서버에서 데이터를 받을 때
        SocketManger.Socket.On("JoinRoomUserInfo", (data) => //"JoinRoomUserInfo" 이벤트(한 유저가 방에 참가할 때)의 데이터
        {
            //서버로부터 데이터를 받기 위한 변수 선언.
            string json = JsonConvert.SerializeObject(data.Json.args[0]);
            //서버의 데이터를 받기 위한 클래스 선언
            UserInfo response_user = JsonUtility.FromJson<UserInfo>(json);//Userinfo 클래스에 데이터 저장.
            //전역 변수에 각 데이터 저장.
            length = response_user.USER.Length;//length에 한 방에 접속한 유저의 수 저장.
            length2 = response_user.USER.Length;//length2에 한 방에 접속한 유저의 수 저장.
            for (int i = 0; i < response_user.USER.Length; i++)//한 방에 접속한 유저의 수 만큼 반복
            {
                //전역 변수(배열)에 각 데이터 저장.
                input_userCharacter[i] = response_user.USER[i].UserCharacter;//각 유저의 캐릭터 정보 저장
                input_userName[i] = response_user.USER[i].UserName;//각 유저의 닉네임 정보 저장
                ReadyCount = response_user.GameStartCount;//Ready버튼을 누른 유저의 수 저장.
                UserReadycount[i] = response_user.USER[i].readyCheck;//각 유저의 Ready 정보 저장
            }
        });
        //소켓서버에서 데이터를 받을 때
        SocketManger.Socket.On("GameStartCount", (data) => //"GameStartCount" 이벤트(참여한 유저가 Ready버튼을 누를 때)의 데이터
        {
            //서버로부터 데이터를 받기 위한 변수 선언.
            string json = JsonConvert.SerializeObject(data.Json.args[0]);
            //서버의 데이터를 받기 위한 클래스 선언
            UserInfo g_response_user = JsonUtility.FromJson<UserInfo>(json);//Userinfo 클래스에 데이터 저장.
            //전역 변수에 각 데이터 저장.
            length = g_response_user.USER.Length;//length에 한 방에 접속한 유저의 수 저장.
            length2 = g_response_user.USER.Length;//length2에 한 방에 접속한 유저의 수 저장.
            ReadyCount = g_response_user.GameStartCount;//Ready버튼을 누른 유저의 수 저장.
            for (int i=0; i<g_response_user.USER.Length; i++) UserReadycount[i] = g_response_user.USER[i].readyCheck;
        });
        //소켓서버에서 데이터를 받을 때
        SocketManger.Socket.On("GameStart", (data) => //"GameStart" 이벤트(유저 5명 모두 Ready버튼을 눌렀을 때)의 데이터
        {
            //서버로부터 데이터를 받기 위한 변수 선언.
            string json = JsonConvert.SerializeObject(data.Json.args[0]);
            SceneDeliver.sceneDeliver.playerInfo = JsonUtility.FromJson<UserInfo>(json);//UserInfo클래스에 저장되어있는 정보를 SceneDeliver스크립트의 playerInfo에 저장.
            SceneDeliver.sceneDeliver.userID = ID_text.text;//ID_text의 값(사용자의 ID)을 SceneDeliver스크립트의 userID(string)에 저장.
            GameStart = true;//FixedUpdate문의 if문을 동작시키기 위한 GameStart(boolean) True로 변경.
        });
        //소켓서버에서 데이터를 받을 때
        SocketManger.Socket.On("NewLeaveRoomUserInfo", (data) => //"NewLeaveRoomUserInfo" 이벤트(한 유저가 방을 떠날 때)의 데이터
        {
            //서버로부터 데이터를 받기 위한 변수 선언.
            string json = JsonConvert.SerializeObject(data.Json.args[0]);
            //서버의 데이터를 받기 위한 클래스 선언
            UserInfo leave_user = JsonUtility.FromJson<UserInfo>(json);//Userinfo 클래스에 데이터 저장.
            //전역 변수에 각 데이터 저장.
            length = leave_user.USER.Length;//length에 한 방에 접속한 유저의 수 저장.
            length2 = leave_user.USER.Length;//length2에 한 방에 접속한 유저의 수 저장.
            for (int i = 0; i < leave_user.USER.Length; i++)//한 방에 접속한 유저의 수 만큼 반복
            {
                input_userCharacter[i] = leave_user.USER[i].UserCharacter;//각 유저의 캐릭터 정보 저장
                input_userName[i] = leave_user.USER[i].UserName;//각 유저의 닉네임 정보 저장
                ReadyCount = leave_user.GameStartCount;//Ready버튼을 누른 유저의 수 저장.
                UserReadycount[i] = leave_user.USER[i].readyCheck;//각 유저의 Ready 정보 저장
            }
        });
    }
    //Play 버튼이 클릭되면 실행
    public void PlayBtncclick()
    {
        string data = "";//소켓서버에 보낼 데이터
        SocketManger.Socket.Emit("Waiting", data);//소켓서버에 데이터 전송
    }
    //Join 버튼이 클릭되면 실행
    public void JoinBtnclick()
    {
        //서버에 내 유저정보 전송
        RoomPushData roomPushData = new RoomPushData();//RoomPushData 클래스선언
        CharacterType c_user_Character = GameObject.Find("SelectBtn").GetComponent<CharacterType>();//CharacterType 스크립트 선언
        //RoomPushData 클래스에 각 데이터 저장.
        roomPushData.RoomName = RoomName_Play.GetComponent<Text>().text;//방 이름(Room1 or Room2)저장.
        roomPushData.UserID = ID_text.text;//유저의 ID 저장.
        roomPushData.UserName = Name_text.text;//유저의 이름 저장.
        roomPushData.Character = c_user_Character.c_character_number;//유저의 캐릭터 정보 저장.
        RoomName_Join.GetComponent<Text>().text = roomPushData.RoomName;//접속할 방(JoinMenu)의 이름을 접속한 방(Room1 or Room2)으로 바꿔줌.(Room? -> Room1 or 2)
        //Json네트워크를 이용해 소켓 통신.
        string data = JsonUtility.ToJson(roomPushData, prettyPrint: true);//서버에 RoomPushData 클래스 보냄.
        SocketManger.Socket.Emit("JoinRoom", data);//처리할 이벤트 "JoinRoom"에 데이터 전송
    }
    //Start 버튼이 클릭되면 실행
    public void StartBtnclick()
    {
        //서버에 내 유저정보 전송
        UserReadyCheck userreadycheck = new UserReadyCheck();//UserReadyCheck 클래스선언
        if (userreadycheck.ReadyCheck)//UserReadyCheck 클래스의 ReadyCheck이 true라면(유저가 레디 상태라면)
        {
            //UserReadyCheck 클래스에 각 데이터 저장.
            userreadycheck.ReadyCheck = false;//ReadyCheck을 다시 false로 바꿈.
            userreadycheck.RoomName = RoomName_Play.GetComponent<Text>().text;//RoomName 정보 저장(Room1 or Room2)
            userreadycheck.UserID = ID_text.text;//(start 버튼을 누른)유저의 ID정보 저장
            //Json네트워크를 이용해 소켓 통신.
            string data = JsonUtility.ToJson(userreadycheck, prettyPrint: true);//서버에 RoomPushData 클래스 보냄.
            SocketManger.Socket.Emit("ReadyButton", data);//처리할 이벤트 "ReadyButton"에 데이터 전송
        }
        else
        {
            //UserReadyCheck 클래스에 각 데이터 저장.
            userreadycheck.ReadyCheck = true;//ReadyCheck을 true로 바꿈.
            userreadycheck.RoomName = RoomName_Join.GetComponent<Text>().text;//RoomName 정보 저장(Room1 or Room2)
            userreadycheck.UserID = ID_text.text;//(start 버튼을 누른)유저의 ID정보 저장
            //Json네트워크를 이용해 소켓 통신.
            string data = JsonUtility.ToJson(userreadycheck, prettyPrint: true);//서버에 RoomPushData 클래스 보냄.
            SocketManger.Socket.Emit("ReadyButton", data);//처리할 이벤트 "ReadyButton"에 데이터 전송
            Toast.Instance.Show("Ready. 5명 모두 준비가 될 경우 게임을 시작합니다.", 1f);//토스트 메시지 생성.
        }
    }
    //Back 버튼이 클릭되면 실행
    public void BackBtnclick()
    {
        //서버에 내 유저정보 전송
        RoomPushData roomPushData = new RoomPushData();//RoomPushData 클래스선언
        //RoomPushData 클래스에 각 데이터 저장.
        roomPushData.UserID = ID_text.text;//(Back 버튼을 누른)유저의 ID정보 저장
        roomPushData.RoomName = RoomName_Join.GetComponent<Text>().text;//RoomName 정보 저장(Room1 or Room2)
        //Json네트워크를 이용해 소켓 통신.
        string data = JsonUtility.ToJson(roomPushData, prettyPrint: true);//서버에 RoomPushData 클래스 보냄.
        SocketManger.Socket.Emit("LeaveRoom", data);//처리할 이벤트 "LeaveRoom"에 데이터 전송
    }
    private void FixedUpdate()
    {
        //각 방의 참여자 수 띄우기.
        if(isPlayBtnClick)//isPlayBtnClick이 true라면
        {
            UserCount1.GetComponent<Text>().text = usercount1;//Room1의 참여자 수 저장
            UserCount2.GetComponent<Text>().text = usercount2;//Room2의 참여자 수 저장
            isPlayBtnClick = false;//isPlayBtnClick 초기화
        }
        //유저가 방에 참여하거나 떠날 때 참여자 정보를 갱신하여 띄우기.
        if (length > 0)//참여자가 한명 이상이면
        {
            //panel 5칸 초기화
            for (int i=0; i<5;i++)
            {
                UserCharacter[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);//유저 캐릭터 투명하게 초기화
                UserName[i].GetComponent<Text>().color = new Color(0, 0, 0, 0);//유저의 닉네임 투명하게 초기화
            }
            //참여자 수 만큼 참여자 정보 입력
            for (int i = 0; i < length; i++)
            {
                UserCharacter[i].sprite = sprites[input_userCharacter[i]];//참여자의 캐릭터(sprite) 띄우기.
                UserCharacter[i].color = new Color(255, 255, 255, 255);//참여자 캐릭터 투명해제.
                UserName[i].text = input_userName[i];//참여자의 닉네임 띄우기
                UserName[i].color = new Color(0, 0, 0, 255);//참여자 닉네임 투명해제.
            }
            length = 0;//length 초기화
        }
        //참여자의 Start버튼 클릭 여부 정보를 갱신하여 띄우기.
        if (length2 >0)//참여자가 한명 이상이면
        {
            //panel 5칸 초기화
            for (int i=0; i<5;i++) UserPannel[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);//panel 5칸 흰색으로 초기화
            //Start 버튼을 누른 참여자 정보 띄우기
            for (int i = 0; i<length2; i++)//참여자 수 만큼 반복
            {
                if (UserReadycount[i]) UserPannel[i].GetComponent<Image>().color = new Color(0, 255, 0, 255);//유저가 Start버튼을 눌렀다면(true) panel을 녹색으로 바꿈. 
                else UserPannel[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);//유저가 Start버튼을 해제했다면(false) panel을 흰색으로 바꿈. 
            }
            StartCount.GetComponent<Text>().text = "Ready\n" + ReadyCount + "/5";//Start 버튼을 누른 유저의 수 띄우기
            length2 = 0;//length2 초기화
        }
        //5명의 참가자가 모두 Start버튼을 누르면(true)
        if(GameStart)
        {
            GameStart = false;//GameStart 초기화
            SceneManager.LoadScene("LoadingScene");//LoadingScene 불러오기
        }
    }
}
//서버와 데이터를 주고 받기 위한 클래스
public class RoomUserCount {//각 방의 참여자 수 정보(받기)
    public int room1, room2;
}
//서버와 데이터를 주고 받기 위한 클래스
public class RoomPushData //방에 참여하거나 떠난 유저의 정보(보내기)
{
    public string RoomName, UserID, UserName;
    public int Character;
}
//서버와 데이터를 주고 받기 위한 클래스
[System.Serializable]
public class AddThisPlayer //방에 참여한 유저의 정보(받기)
{
    public string UserID, UserName;
    public int joinRoomNumber, imageNumber, UserCharacter;
    public bool readyCheck;
    public bool isImposter;
}
//서버와 데이터를 주고 받기 위한 클래스
[System.Serializable]
public class UserInfo //방에 참여한 유저의 정보(받기)
{
    public AddThisPlayer[] USER; //AddThisPlayer 클래스를 배열의 형태로 선언.
    public int GameStartCount;//몇 명이 Ready했는지 저장하는 변수 선언.
}
//서버와 데이터를 주고 받기 위한 클래스
public class UserReadyCheck//Start 버튼을 클릭한 유저의 정보
{
    public bool ReadyCheck = false;
    public string UserID;
    public string RoomName;
}
