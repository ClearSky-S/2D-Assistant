using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class MSGraphManager : MonoBehaviour
{
    void getToken(String authorizationCode)
    {
        // ��û�� ������ URI
        string strUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

        // POST, GET ���� ������ �Է�
        StringBuilder dataParams = new StringBuilder();
        dataParams.Append("client_id=1be14ecd-3e3b-4ede-a0a8-a91d459df1c0");
        dataParams.Append("&scope=offline_access%20user.read%20mail.read");
        dataParams.Append("&code="+ authorizationCode);
        dataParams.Append("&redirect_uri=https://login.microsoftonline.com/common/oauth2/nativeclient");
        dataParams.Append("&grant_type=authorization_code");

        // ��û String -> ��û Byte ��ȯ
        byte[] byteDataParams = UTF8Encoding.UTF8.GetBytes(dataParams.ToString());

        /////////////////////////////////////////////////////////////////////////////////////
        /* POST */
        // HttpWebRequest ��ü ����, ����
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri);
        request.Method = "POST";    // �⺻�� "GET"
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteDataParams.Length;

        /* GET */
        // GET ����� Uri �ڿ� ���� �����͸� �Է��Ͻø� �˴ϴ�.
        /*
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri + "?" + dataParams);
        request.Method = "GET";
        */
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

        Debug.Log(strResult);
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
