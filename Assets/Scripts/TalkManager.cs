using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkManager : MonoBehaviour
{
    public AudioSource characterAudioSource;
    public TextMeshProUGUI talkText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TalkInteract()
    {
        CharacterManager characterManager = GetComponent<CharacterManager>();
        CharacterData characterData = characterManager.character.GetComponent<CharacterData>();
        characterManager.PlayInteractAnimation();

        int index = Random.Range(0, characterData.interact.Length);
        characterAudioSource.clip = characterData.interact[index].audioClip;
        characterAudioSource.Play();
        talkText.text = characterData.interact[index].text;
    }
}
