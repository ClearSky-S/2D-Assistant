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
        // ��û�� ������ URI
        string strUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

        // POST, GET ���� ������ �Է�
        StringBuilder dataParams = new StringBuilder();
        dataParams.Append("client_id="+clientId);
        dataParams.Append("&scope=user.read offline_access");
        dataParams.Append("&code="+ authorizationCode);
        dataParams.Append("&redirect_uri=https://login.microsoftonline.com/common/oauth2/nativeclient");
        dataParams.Append("&grant_type=authorization_code");

        // ��û String -> ��û Byte ��ȯ
        byte[] byteDataParams = UTF8Encoding.UTF8.GetBytes(dataParams.ToString());

        /////////////////////////////////////////////////////////////////////////////////////
        // POST
        // HttpWebRequest ��ü ����, ����
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri);
        request.Method = "POST";    // �⺻�� "GET"
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteDataParams.Length;

        // GET 
        // GET ����� Uri �ڿ� ���� �����͸� �Է��Ͻø� �˴ϴ�.
        
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri + "?" + dataParams);
        //request.Method = "GET";
        
        //////////////////////////////////////////////////////////////////////////////////////


        // ��û Byte -> ��û Stream ��ȯ
        Stream stDataParams = request.GetRequestStream();
        stDataParams.Write(byteDataParams, 0, byteDataParams.Length);
        stDataParams.Close();

        // ��û, ���� �ޱ�
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        // ���� Stream �б�
        Stream stReadData = response.GetResponseStream();
        StreamReader srReadData = new StreamReader(stReadData, Encoding.Default);

        // ���� Stream -> ���� String ��ȯ
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
