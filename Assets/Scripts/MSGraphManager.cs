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
        // 요청을 보내는 URI
        string strUri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

        // POST, GET 보낼 데이터 입력
        StringBuilder dataParams = new StringBuilder();
        dataParams.Append("client_id=1be14ecd-3e3b-4ede-a0a8-a91d459df1c0");
        dataParams.Append("&scope=offline_access%20user.read%20mail.read");
        dataParams.Append("&code="+ authorizationCode);
        dataParams.Append("&redirect_uri=https://login.microsoftonline.com/common/oauth2/nativeclient");
        dataParams.Append("&grant_type=authorization_code");

        // 요청 String -> 요청 Byte 변환
        byte[] byteDataParams = UTF8Encoding.UTF8.GetBytes(dataParams.ToString());

        /////////////////////////////////////////////////////////////////////////////////////
        /* POST */
        // HttpWebRequest 객체 생성, 설정
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri);
        request.Method = "POST";    // 기본값 "GET"
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteDataParams.Length;

        /* GET */
        // GET 방식은 Uri 뒤에 보낼 데이터를 입력하시면 됩니다.
        /*
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri + "?" + dataParams);
        request.Method = "GET";
        */
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
