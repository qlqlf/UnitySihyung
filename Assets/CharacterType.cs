using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class CharacterType : MonoBehaviour
{
    public InputField ID_text;
    public Toggle Character;
    public static string ip = "192.168.43.217";

    private void Awake()
    {
        Character.onValueChanged
    }
    public class res_character
    {
        public string message;
    }
    IEnumerator UpdateCharacterWithWWW()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", ID_text.text);
        form.AddField("character", Character.name);

        UnityWebRequest www = UnityWebRequest.Post("http://" + ip + ":8080/process/updatecharacter", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Toast.Instance.Show("서버 접속에 실패했습니다.", 3f);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            var res_character = JsonConvert.DeserializeObject<res_character>(www.downloadHandler.text);
            Debug.LogFormat("{0}", res_character.message);
            if (res_character.message.Equals("캐릭터 설정 성공"))
            {
                Debug.LogFormat("{0}", res_character.message);
            }
            else
            {
                Toast.Instance.Show("캐릭터 설정 실패.", 3f);
            }
        }
    }
}
