using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterData : MonoBehaviour
{
    [Serializable]
    public struct TalkData
    {
        public string title;
        [TextArea]
        public string text;
        public AudioClip audioClip;
    };
    [SerializeField]
    public TalkData[] interact; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
