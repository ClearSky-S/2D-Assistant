using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine.Networking;

public class MSGraphManager : MonoBehaviour
{
    public string clientId;
    public TMP_InputField authorizationCodeTMP;

    public String authorizationCode;
    public String acessToken;

    public void getAuthorizationCode()
    {
        Application.OpenURL(@"https://login.microsoftonline.com/common/oauth2/v2.0/authorize?
client_id=" + clientId +
@"& response_type=code
& redirect_uri=https://login.microsoftonline.com/common/oauth2/nativeclient
& response_mode=query
& scope=offline_access user.read
");
    }
    IEnumerator IEnumGetToken()
    {
        authorizationCode = authorizationCodeTMP.text;
        WWWForm form = new WWWForm();
        form.AddField("client_id", clientId);
        form.AddField("scope", "offline_access user.read");
        form.AddField("code", authorizationCode);
        form.AddField("redirect_uri", "https://login.microsoftonline.com/common/oauth2/nativeclient");
        form.AddField("grant_type", "authorization_code");

        string url = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string strResult = www.downloadHandler.text;
                JObject jsonResult = JObject.Parse(strResult);
                Debug.Log(strResult.ToString());
            }
        }
    }
    public void getToken()
    {
        StartCoroutine(IEnumGetToken());

        /*
        authorizationCode = authorizationCodeTMP.text;
        // 요청을 보내는 URI
        string strUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

        // POST, GET 보낼 데이터 입력
        StringBuilder dataParams = new StringBuilder();
        dataParams.Append("client_id="+clientId);
        dataParams.Append("&scope=user.read offline_access");
        dataParams.Append("&code="+ authorizationCode);
        dataParams.Append("&redirect_uri=https://login.microsoftonline.com/common/oauth2/nativeclient");
        dataParams.Append("&grant_type=authorization_code");

        // 요청 String -> 요청 Byte 변환
        byte[] byteDataParams = UTF8Encoding.UTF8.GetBytes(dataParams.ToString());

        /////////////////////////////////////////////////////////////////////////////////////
        // POST
        // HttpWebRequest 객체 생성, 설정
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri);
        request.Method = "POST";    // 기본값 "GET"
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteDataParams.Length;

        // GET 
        // GET 방식은 Uri 뒤에 보낼 데이터를 입력하시면 됩니다.
        
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri + "?" + dataParams);
        //request.Method = "GET";
        
        //////////////////////////////////////////////////////////////////////////////////////


        // 요청 Byte -> 요청 Stream 변환
        Stream stDataParams = request.GetRequestStream();
        stDataParams.Write(byteDataParams, 0, byteDataParams.Length);
        stDataParams.Close();

        // 요청, 응답 받기
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        // 응답 Stream 읽기
        Stream stReadData = response.GetResponseStream();
        StreamReader srReadData = new StreamReader(stReadData, Encoding.Default);

        // 응답 Stream -> 응답 String 변환
        string strResult = srReadData.ReadToEnd();
        JObject jsonResult = JObject.Parse(strResult);
        Debug.Log(strResult.ToString());
        */
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame



    void Update()
    {

    }
}
