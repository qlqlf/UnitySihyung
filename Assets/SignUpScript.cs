using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using Newtonsoft.Json;

public class signupBtn_Event : MonoBehaviour
{
    public GameObject Sign_Up_Panel, Initial_Sign_Button, Panel_Sign_Button, Panel_Cancel_Button, idcheck_Button, Panel_FindID_Button, Panel_FindPW_Button;
    public InputField ID_text, Password_text, Email_text;
    public string ip = "192.168.43.36";

    public void SignupBtn_Event()
    {
        if (Sign_Up_Panel.activeSelf == false)
        {
            Debug.Log("sign up 클릭되었습니다.");
            Sign_Up_Panel.SetActive(true);
            Initial_Sign_Button.SetActive(false);
        }
    }

    public void panel_SignBtn_Event()
    {
        if (Panel_Sign_Button.activeSelf == true)
        {
            if (string.IsNullOrEmpty(ID_text.text) == false && string.IsNullOrEmpty(Password_text.text) == false && string.IsNullOrEmpty(Email_text.text) == false)
            {
                StartCoroutine(PostNetworkingWithWWW());
                Sign_Up_Panel.SetActive(false);
                Initial_Sign_Button.SetActive(true);
                ID_text.text = "";
                Password_text.text = "";
                Email_text.text = "";
                Debug.Log("창을 닫아요");
            }
            else
            {
                Debug.Log("공백을 입력해주세요");
                Debug.Log(string.IsNullOrEmpty(ID_text.text));
            }
        }
    }
    public void panel_CancelBtn_Event()
    {
        if (Panel_Cancel_Button.activeSelf == true)
        {
            Sign_Up_Panel.SetActive(false);
            Initial_Sign_Button.SetActive(true);
            ID_text.text = "";
            Password_text.text = "";
            Email_text.text = "";
            Debug.Log("창을 닫아요");
        }
    }
    public void idcheck_Event()
    {
        if (string.IsNullOrEmpty(ID_text.text) == false)
        {
            StartCoroutine(idcheckWithWWW());
        }
        else
        {
            Debug.Log("공백을 입력해주세요");
        }
    }

    public void FindID_Event()
    {
        if (string.IsNullOrEmpty(Email_text.text) == false)
        {
            StartCoroutine(FindIDWithWWW());
        }
        else
        {
            Debug.Log("공백을 입력해주세요");
        }
    }

    public void FindPW_Event()
    {
        if (string.IsNullOrEmpty(ID_text.text) == false && string.IsNullOrEmpty(Email_text.text) == false)
        {
            StartCoroutine(FindPWWithWWW());
        }
        else
        {
            Debug.Log("공백을 입력해주세요");
        }
    }
    IEnumerator PostNetworkingWithWWW()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", ID_text.text);
        form.AddField("password", Password_text.text);
        form.AddField("email", Email_text.text);

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.0.6:3000/process/adduser", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    public class req_login
    {
        public string id;
        public string userName;
    }
    public class res_idcheck
    {
        public string message;
    }
    IEnumerator idcheckWithWWW()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", ID_text.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/process/idcheck", form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            var res_idcheck = JsonConvert.DeserializeObject<res_idcheck>(www.downloadHandler.text);
            Debug.LogFormat("{0}", res_idcheck.message);
        }
    }

    public class res_findid
    {
        public string id;
    }
    IEnumerator FindIDWithWWW()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", Email_text.text);

        UnityWebRequest www = UnityWebRequest.Post("http://" + ip + ":8080/process/findid", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            var res_findid = JsonConvert.DeserializeObject<res_findid>(www.downloadHandler.text);
            Debug.LogFormat("{0}", res_findid.id);
        }
    }
    public class res_findpw
    {
        public string pw;
    }
    IEnumerator FindPWWithWWW()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", ID_text.text);
        form.AddField("email", Email_text.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/process/findpw", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            var res_findpw = JsonConvert.DeserializeObject<res_findpw>(www.downloadHandler.text);
            Debug.LogFormat("{0}", res_findpw.pw);
        }
    }
}