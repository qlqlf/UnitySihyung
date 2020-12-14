using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI사용
using UnityEngine.Networking;//Network사용
using Newtonsoft.Json;//Socket Network사용
using System;

public class BtnType : MonoBehaviour
{
    public BTNType currentType; //MenuType 스크립트의 enum(BTNType) 가져옴.
    //사용하는 CanvasGroup 선언.
    public CanvasGroup StartMenu, LoginMenu, SignUpMenu, FindMenu, MainMenu, PlayMenu, TutorialMenu, CashShopMenu, JoinMenu;
    //입력을 가져올 InputField 선언.
    public InputField ID_text, Password_text, Email_text, Name_text;
    public static string ipget= ip.IP; //ip스크립트에 저장된 ip값 가져옴.
    //아이디 중복체크가 되었는지 확인하는 boolean값(IDcheck) 선언.
    public static Boolean IDcheck = false;
    //게임이 시작했을 경우.
    public void Start()
    {
        //StartMenu를 키고 나머지는 끈다. 기능은 CanvasGroupOn과 CanvasGroupOff 클래스를 따로 만들어 구현.
        CanvasGroupOn(StartMenu);
        CanvasGroupOff(PlayMenu);
        CanvasGroupOff(LoginMenu);
        CanvasGroupOff(SignUpMenu);
        CanvasGroupOff(FindMenu);
        CanvasGroupOff(MainMenu);
        CanvasGroupOff(TutorialMenu);
        CanvasGroupOff(CashShopMenu);
        CanvasGroupOff(JoinMenu);
    }
    //버튼이 클릭 되면
    public void OnBtnClick()
    {
        switch (currentType)//어떤 종류의 버튼이 클릭되었는지 확인.
        {
            case BTNType.Login://Login 버튼이라면
                //LoginMenu를 키고 나머지는 끈다.
                CanvasGroupOn(LoginMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
                break;
            case BTNType.SignUp://SignUp 버튼이라면
                //SignUpMenu를 키고 나머지는 끈다.
                CanvasGroupOn(SignUpMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
                break;
            case BTNType.Pl_Login://Login 패널의 Login버튼이라면
                //ID_text와 Password_text에 값이 있으면(null이 아니면) 
                if (string.IsNullOrEmpty(ID_text.text) == false && string.IsNullOrEmpty(Password_text.text) == false)
                {
                    StartCoroutine(loginWithWWW());//loginWithWWW클래스 실행.
                }//ID_text와 Password_text 둘 중 하나라도 값이 null이면
                else
                {
                    Toast.Instance.Show("ID와 PassWord를 입력해주세요", 3f);//토스트 메세지 띄움.
                }
                break;
            case BTNType.Pl_SignUp://SignUp 패널의 SignUp버튼이라면
                if(string.IsNullOrEmpty(ID_text.text) == false)//ID_text의 값이 null이 아니라면
                {
                    if(IDcheck)//IDcheck가 true라면
                    {
                        //Email_text와 Password_text에 값이 있으면(null이 아니면) 
                        if (string.IsNullOrEmpty(Password_text.text) == false && string.IsNullOrEmpty(Email_text.text) == false)
                        {
                            StartCoroutine(PostNetworkingWithWWW());//PostNetworkingWithWWW클래스 실행.
                            //StartMenu키고 나머지 끔.
                            CanvasGroupOn(StartMenu);
                            CanvasGroupOff(LoginMenu);
                            CanvasGroupOff(SignUpMenu);
                            CanvasGroupOff(FindMenu);
                            //InputField에 들어있는 값 초기화.
                            ID_text.text = "";
                            Password_text.text = "";
                            Email_text.text = "";
                            Debug.Log("창을 닫아요");
                        }//Email_text와 Password_text 둘 중 하나라도 값이 null이면
                        else
                        {
                            Toast.Instance.Show("E-mail과 PW를 입력해주세요", 3f);//토스트 메시지 띄움.
                        }
                    }//IDcheck가 false면(ID중복체크가 돼있지 않으면)
                    else
                    {
                        Toast.Instance.Show("ID중복체크를 해주세요.", 3f);//토스트 메시지 띄움.
                    }
                }//ID_text의 값이 null이면
                else
                {
                    Toast.Instance.Show("ID를 입력해주세요", 3f);//토스트 메시지 띄움.
                }
                break;
            case BTNType.Cancel://Cancel 버튼이라면
                //StartMenu를 키고 나머지는 끈다.
                CanvasGroupOn(StartMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
                break;
            case BTNType.Find://Find 버튼이라면
                //FindMenu를 키고 나머지는 끈다.
                CanvasGroupOn(FindMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
                break;
            case BTNType.Find_ID://Find_ID(ID찾기)버튼이라면
                if (string.IsNullOrEmpty(Email_text.text) == false)//Email_text의 값이 null이 아니라면
                {
                    StartCoroutine(FindIDWithWWW());//FindIDWithWWW 클래스 실행.
                }
                //Email_text의 값이 null이면
                else
                {
                    Toast.Instance.Show("Email을 입력해주세요", 3f);// 토스트메시지 띄움.
                }
                break;
            case BTNType.Find_PW://Find_PW(PW찾기)버튼이라면
                //ID_text와 Email_text의 값이 null이 아니라면
                if (string.IsNullOrEmpty(ID_text.text) == false && string.IsNullOrEmpty(Email_text.text) == false)
                {
                    StartCoroutine(FindPWWithWWW());//FindPWWithWWW 클래스 실행.
                }
                //ID_text와 Email_text의 값중 null값이 있으면
                else
                {
                    Toast.Instance.Show("ID와 Email을 입력해주세요", 3f);// 토스트메시지 띄움.
                }
                break;
            case BTNType.IDcheck://IDcheck버튼이라면
                //ID_text의 값이 null이 아나라면
                if (string.IsNullOrEmpty(ID_text.text) == false)
                {
                    StartCoroutine(idcheckWithWWW());//idcheckWithWWW 클래스 실행.
                }//ID_text의 값이 null이면
                else
                {
                    Toast.Instance.Show("ID를 입력해주세요",3f);// 토스트메시지 띄움.
                }
                break;
            case BTNType.Play://Play 버튼이라면
                //Name_text의 값이 null이 아니라면
                if (string.IsNullOrEmpty(Name_text.text) == false)
                {
                    StartCoroutine(UpdateNameWithWWW());//UpdateNameWithWWW 클래스 실행
                }//Name_text의 값이 null이라면
                else
                {
                    Toast.Instance.Show("닉네임을 설정해주세요");// 토스트메시지 띄움.
                }
                    break;
            case BTNType.Tutorial://Tutorial버튼이라면
                //TutorialMenu 키고 나머지 끔.
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOn(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
                break;
            case BTNType.CashShop://CashShop버튼이라면
                //CashShopMenu 키고 나머지 끔.
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOn(CashShopMenu);
                CanvasGroupOff(JoinMenu);
                break;
            case BTNType.Logout://Logout 버튼이라면
                //StartMenu 키고 나머지 끔.
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOn(StartMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
                break;
            case BTNType.Join://Join 버튼이라면
                //JoinMenu 키고 나머지 끔.
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOn(JoinMenu);
                break;
            case BTNType.Back://Back버튼이라면
                //MainMenu 키고 나머지 끔.
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOn(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
                break;
        }
    }
    //CanvasGroup 키는 클래스
    public void CanvasGroupOn(CanvasGroup cg)//CanvasGroup받고
    {
        //해당 CanvasGroup의 동작수행을 켜준다.
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    //CanvasGroup 끄는 클래스
    public void CanvasGroupOff(CanvasGroup cg)//CanvasGroup받고
    {
        //해당 CanvasGroup의 동작수행을 꺼준다.
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    //서버에 회원가입 정보 넣어주는 클래스
    IEnumerator PostNetworkingWithWWW()
    {
        WWWForm form = new WWWForm();//form을 생성하고
        //form에 전송할 값을 넣어준다.
        form.AddField("id", ID_text.text);
        form.AddField("password", Password_text.text);
        form.AddField("email", Email_text.text);
        //접근할 데이터베이스 선언.
        UnityWebRequest www = UnityWebRequest.Post("http://"+ipget+"process/adduser", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)//서버에서 오류가 생길 경우
        {
            Debug.Log(www.error);
            Toast.Instance.Show("회원가입에 실패했습니다.", 3f);//토스트 메시지 생성.
        }
        //서버가 정상적으로 작동하면
        else
        {
            Debug.Log("Form upload complete!");
            Toast.Instance.Show("회원가입이 성공했습니다.", 3f);//토스트 메시지 생성.
        }
    }
    //서버로부터 받아오는 값을 저장할 클래스
    public class res_idcheck
    {
        public string message;//id중복 정보 저장할 변수
    }
    //서버에 ID중복확인 정보 넣어주는 클래스
    IEnumerator idcheckWithWWW()
    {
        WWWForm form = new WWWForm();//form을 생성하고
        //form에 전송할 값을 넣어준다.
        form.AddField("id", ID_text.text);
        //접근할 데이터베이스 선언.
        UnityWebRequest www = UnityWebRequest.Post("http://" + ipget + "process/idcheck", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)//서버에서 오류가 생길 경우
        {
            Debug.Log(www.error);
            Toast.Instance.Show("서버 접속에 실패했습니다.", 3f);//토스트 메시지 생성.
        }
        //서버가 정상적으로 작동하면
        else
        {
            Debug.Log(www.downloadHandler.text);
            //서버에서 값을 받아올 변수 생성.
            var res_idcheck = JsonConvert.DeserializeObject<res_idcheck>(www.downloadHandler.text);//res_idcheck클래스의 변수에 저장
            Debug.LogFormat("{0}", res_idcheck.message);
            //서버로부터 받아온 값이 "yes"라면
            if (res_idcheck.message.Equals("yes")) {
                Toast.Instance.Show("사용가능한 ID입니다.", 3f);//토스트 메시지 생성.
                IDcheck = true;//IDcheck(bool값)를 true로 바꿈.
            }//서버로부터 받아온 값이 "yes"가 아니라면(중복된 ID라면)
            else
            {
                Toast.Instance.Show("중복된 ID입니다. 다른 ID를 입력해주세요", 3f);//토스트 메시지 생성.
            }
        }
    }
    //서버로부터 받아오는 값을 저장할 클래스
    public class res_findid
    {
        public string id;//ID값을 저장할 변수
    }
    //서버에 ID찾기 정보 넣어주는 클래스
    IEnumerator FindIDWithWWW()
    {
        WWWForm form = new WWWForm();//form을 생성하고
        //form에 전송할 값을 넣어준다.
        form.AddField("email", Email_text.text);
        //접근할 데이터베이스 선언.
        UnityWebRequest www = UnityWebRequest.Post("http://" +ipget + "process/findid", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)//서버에서 오류가 생길 경우
        {
            Debug.Log(www.error);
            Toast.Instance.Show("네트워크 연결에 실패했습니다", 3f);//토스트 메시지 생성.
        }
        //서버가 정상적으로 작동하면
        else
        {
            Debug.Log(www.downloadHandler.text);
            //서버로부터 받아온 값의 12번째 글자부터 4개의 글자가 NULL이라면(ID가 없다면)
            if (www.downloadHandler.text.Substring(12,4).Equals("NULL"))
            {
                Toast.Instance.Show("ID가 없습니다.", 3f);//토스트 메시지 생성.
            }
            //서버로부터 받아온 값이 null이 아니라면
            else
            {
                //서버로부터 받은 값의 11번째 문자부터 토스트 메시지로 출력(같은 Email의 모든 ID 출력)
                Toast.Instance.Show(www.downloadHandler.text.Substring(11), 3f);
            }
        }
    }
    //서버로부터 받아오는 값을 저장할 클래스
    public class res_findpw
    {
        public string pw;//PW값을 저장할 변수
    }
    //서버에 PW찾기 정보 넣어주는 클래스
    IEnumerator FindPWWithWWW()
    {
        WWWForm form = new WWWForm();//form을 생성하고
        //form에 전송할 값을 넣어준다.
        form.AddField("id", ID_text.text);
        form.AddField("email", Email_text.text);
        //접근할 데이터베이스 선언.
        UnityWebRequest www = UnityWebRequest.Post("http://" + ipget + "process/findpw", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)//서버에서 오류가 생길 경우
        {
            Debug.Log(www.error);
            Toast.Instance.Show("네트워크 연결에 실패했습니다", 3f);//토스트 메시지 생성.
        }
        //서버가 정상적으로 작동하면
        else
        {
            Debug.Log(www.downloadHandler.text);
            //서버에서 값을 받아올 변수 생성.
            var res_findpw = JsonConvert.DeserializeObject<res_findpw>(www.downloadHandler.text);//res_findpw클래스의 변수에 저장
            Debug.LogFormat("{0}", res_findpw.pw);
            //PW값을 토스트 메시지로 생성.
            Toast.Instance.Show(res_findpw.pw, 3f);
        }
    }
    //서버로부터 받아오는 값을 저장할 클래스
    public class res_login
    {
        public string message;//login 정보를 저장하는 변수 선언.
    }
    //서버에 로그인 정보 넣어주는 클래스
    IEnumerator loginWithWWW()
    {
        WWWForm form = new WWWForm();//form을 생성하고
        //form에 전송할 값을 넣어준다.
        form.AddField("id", ID_text.text);
        form.AddField("password", Password_text.text);
        //접근할 데이터베이스 선언.
        UnityWebRequest www = UnityWebRequest.Post("http://" + ipget + "process/login", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)//서버에서 오류가 생길 경우
        {
            Debug.Log(www.error);
            Toast.Instance.Show("서버 접속에 실패했습니다.", 3f);//토스트 메시지 생성.
        }
        //서버가 정상적으로 작동하면
        else
        {
            Debug.Log(www.downloadHandler.text);
            //서버에서 값을 받아올 변수 생성.
            var res_login = JsonConvert.DeserializeObject<res_login>(www.downloadHandler.text);//res_login클래스의 변수에 저장
            Debug.LogFormat("{0}", res_login.message);
            //서버로부터 받은 메세지가 "로그인 성공"과 같다면(로그인이 성공하면)
            if (res_login.message.Equals("로그인 성공"))
            {
                Toast.Instance.Show("Login Sucess.", 3f);//토스트 메시지 생성.
                //MainMenu키고 나머지 끔.
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOn(MainMenu);
                CanvasGroupOff(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
            }
            //로그인이 실패하면
            else
            {
                Toast.Instance.Show("ID와 PW가 다릅니다.", 3f);//토스트 메시지 생성.
            }
        }
    }
    //서버로부터 받아오는 값을 저장할 클래스
    public class res_name
    {
        public string message;//서버로부터 받는 값을 저장할 변수 선언.
    }
    //서버에 닉네임 정보 넣어주는 클래스
    IEnumerator UpdateNameWithWWW()
    {
        WWWForm form = new WWWForm();//form을 생성하고
        //form에 전송할 값을 넣어준다.
        form.AddField("id", ID_text.text);
        form.AddField("name", Name_text.text);
        //접근할 데이터베이스 선언.
        UnityWebRequest www = UnityWebRequest.Post("http://" + ipget + "process/nickname", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)//서버에서 오류가 생길 경우
        {
            Debug.Log(www.error);
            Toast.Instance.Show("서버 접속에 실패했습니다.", 3f);//토스트 메시지 생성.
        }
        //서버가 정상적으로 작동하면
        else
        {
            Debug.Log(www.downloadHandler.text);
            //서버에서 값을 받아올 변수 생성.
            var res_name = JsonConvert.DeserializeObject<res_name>(www.downloadHandler.text);//res_name클래스의 변수에 저장
            Debug.LogFormat("{0}", res_name.message);
            //서버로부터 받은 메세지가 "닉네임 설정성공"과 같다면(닉네임 설정이 성공하면)
            if (res_name.message.Equals("닉네임 설정성공"))
            {
                Toast.Instance.Show("닉네임 설정 성공.", 3f);//토스트 메시지 생성.
                //PlayMenu 키고 나머지 끔.
                CanvasGroupOff(FindMenu);
                CanvasGroupOff(LoginMenu);
                CanvasGroupOff(SignUpMenu);
                CanvasGroupOff(StartMenu);
                CanvasGroupOff(MainMenu);
                CanvasGroupOn(PlayMenu);
                CanvasGroupOff(TutorialMenu);
                CanvasGroupOff(CashShopMenu);
                CanvasGroupOff(JoinMenu);
            }
            else
            {
                Toast.Instance.Show("닉네임 설정 실패.", 3f);//토스트 메시지 생성.
            }
        }
    }
}