using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI사용
using UnityEngine.Networking;//네트워킹 사용(데이터베이스)
using Newtonsoft.Json;//Json 통신 사용(소켓활용)

public class CharacterType : MonoBehaviour
{
    public InputField ID_text;//사용자의 ID를 가져올 InputField 선언
    public Toggle Character;//사용자가 선택한 캐릭터를 가져올 토글 선언
    //전역 변수 선언
    public static string ipget = ip.IP;//ip스크립트의 ip저장.
    public static string character_name;//각 캐릭터의 이름 저장할 변수 선언
    public int c_character_number;//(배열은 숫자를 사용하기 때문에)캐릭터 정보를 int로 저장하기 위한 변수 선언.

    private void Awake()
    {
        Character.onValueChanged.AddListener((On) => //토글의 값이 변하면
        {
            if(On)//토글이 체크되면
            {
                character_name = Character.GetComponentInChildren<Text>().text;//토글의 케릭터이름 저장
                Debug.Log(character_name);
            }
        });
    }
    //Buy 버튼을 누르면 실행
    public void BuyBtnClick()
    {
        Debug.Log("Buy");
    }
    //Select 버튼을 누르면 실행
    public void SelectBtnClick()
    {
        StartCoroutine(UpdateCharacterWithWWW());//UpdateCharacterWithWWW 클래스 실행
        //캐릭터 정보(string)를 숫자의 형태(int)로 바꿈.
        switch (character_name)//character_name(캐릭터의 이름)이
        {
            case "Man"://Man 이라면
                c_character_number = 0;
                break;
            case "Woman"://Woman 이라면
                c_character_number = 1;
                break;
            case "Dracula"://Dracula 이라면
                c_character_number = 2;
                break;
            case "Franken"://Franken 이라면
                c_character_number = 3;
                break;
            case "KangSi"://KangSi 이라면
                c_character_number = 4;
                break;
            case "Mummy"://Mummy 이라면
                c_character_number = 5;
                break;
            case "Witch"://Witch 이라면
                c_character_number = 6;
                break;
        }
    }
    //서버에 데이터를 보내기 위한 클래스
    public class res_character
    {
        public string message;//string 형태로 변수 선언.
    }
    IEnumerator UpdateCharacterWithWWW()
    {
        //서버 통신을 위한 form 생성.
        WWWForm form = new WWWForm();
        //form에 데이터 저장.
        form.AddField("id", ID_text.text);//유저의 ID
        form.AddField("character", character_name);//유저의 캐릭터
        //서버에 접근
        UnityWebRequest www = UnityWebRequest.Post("http://" + ipget + "process/updatecharacter", form);
        yield return www.SendWebRequest();
        //서버 접속에 실패했을 경우
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Toast.Instance.Show("서버 접속에 실패했습니다.", 3f);//토스트 메시지 생성
        }
        //서버 접속에 성공한 경우
        else
        {
            Debug.Log(www.downloadHandler.text);
            //res_character 클래스를 사용해 서버에 데이터 전송
            var res_character = JsonConvert.DeserializeObject<res_character>(www.downloadHandler.text);
            Debug.LogFormat("{0}", res_character.message);
            //캐릭터 설정이 성공하면
            if (res_character.message.Equals("캐릭터 설정 성공"))
            {
                Toast.Instance.Show("캐릭터를" + character_name + "로 설정했습니다.", 3f);//토스트 메시지 생성
            }
            //캐릭터 설정이 실패하면
            else
            {
                Toast.Instance.Show("캐릭터 설정 실패.", 3f);//토스트 메시지 생성
            }
        }
    }
}