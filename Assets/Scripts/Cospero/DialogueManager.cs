using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
public TMP_Text _dialogueTextUI;
public TMP_Text _speakerNameUI;
public Queue<int>  _indexOfSpeaker; 
public Queue<string>  _sentence;
public bool _dialogueIsPlaying;
public GameObject _dialogueObjUI;
public Image _dialogueImageUI;
private string _curSentanceText;

private string[] _charName;
private Color[] _dialogueColours;
private float _dialogueTimerValue;

private void Start() 
{
    _sentence=new Queue<string>();
    _indexOfSpeaker = new Queue<int>();
    _dialogueIsPlaying=false;
    _dialogueObjUI.SetActive(false);
    
}
private void Update() 
{
     if (Input.GetKeyDown(KeyCode.E) == false)
     {
        return;
     }
        StopAllCoroutines();
     if (_dialogueTextUI.text!=_curSentanceText)
     {
        _dialogueTextUI.text=_curSentanceText;
        /* StartCoroutine(NextDialogueStage()); */
     }
     else
     {
         DisplayNextLine();
     }
}
public void StartDialogue(DialogueWindow  dialogue)
{   
    _dialogueColours=dialogue._charColor;
    _charName=dialogue._characterNames;
    _sentence.Clear();
    _indexOfSpeaker.Clear();
    _dialogueIsPlaying=true;
    _dialogueObjUI.SetActive(true);
    foreach (string sentense in dialogue._sentenses)
    {
        _sentence.Enqueue(sentense);
    }
    foreach (int whoIsSpeaking in dialogue._indexOfSpeakers)
    {
        _indexOfSpeaker.Enqueue(whoIsSpeaking);
    }
    DisplayNextLine();
}
private void DisplayNextLine()
    {
        StopAllCoroutines();
        if (_sentence.Count==0)
        {
            EndDialogue();
            return;
        }
        string sentense = _sentence.Dequeue();
        int indexOfSpeaker= _indexOfSpeaker.Dequeue();
        _curSentanceText=sentense;
        StartCoroutine((TypeLines(sentense, indexOfSpeaker )));
    }
IEnumerator TypeLines(string sentense,  int indexOfSpeaker)
    {
        _dialogueTimerValue=((float)sentense.Length/20f+1.5f);
        _speakerNameUI.text=_charName[indexOfSpeaker];
        _dialogueImageUI.color=_dialogueColours[indexOfSpeaker];
       _dialogueTextUI.text="";
        foreach (char letter in sentense.ToCharArray())
        {
            _dialogueTextUI.text +=letter;
            yield return new WaitForSeconds(0.05f);
        }
        /* StartCoroutine(NextDialogueStage()); */
     }
private void EndDialogue()
    {
    _dialogueIsPlaying=false;
    _dialogueObjUI.SetActive(false);
    
    }
}

/*  IEnumerator NextDialogueStage()
    {
       yield return new WaitForSeconds(_dialogueTimerValue);
       DisplayNextLine();
    } */

