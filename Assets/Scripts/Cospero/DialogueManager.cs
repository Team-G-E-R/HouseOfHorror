using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;


public class DialogueManager : MonoBehaviour
{
public bool _isRussian;
public TMP_Text _dialogueTextUI;
public TMP_Text _speakerNameUI;
public Queue<int>  _indexOfSpeaker; 
public Queue<string>  _sentence;
public bool _dialogueIsPlaying;
public GameObject _dialogueObjUI;
public Image _dialogueImageUI;
private string _curSentanceText;
private string _dialogueFileName;
private string[] _charName;
private Color[] _dialogueColours;
private float _dialogueTimerValue;
private string[] _jsonSentenses;
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
{   _dialogueFileName=dialogue._jsonAssetName;
    ThingModel _thingModel=new ThingModel();
    var jsonTextFile = Resources.Load<TextAsset>("Texts/"+_dialogueFileName);
    
    //JsonUtility.FromJsonOverwrite<ThingModel>(jsonTextFile.text);
    JsonUtility.FromJsonOverwrite(jsonTextFile.text, _thingModel);
    if (_isRussian)
    {
        _jsonSentenses=_thingModel._ruLines;
    }
    else
    {
         _jsonSentenses=_thingModel._euLines;
    }
    _charName=_thingModel._namesOfTheSpeakers;
    _dialogueColours=dialogue._charColor;   
    
    _sentence.Clear();
    _indexOfSpeaker.Clear();
    _dialogueIsPlaying=true;
    _dialogueObjUI.SetActive(true);
    foreach (string sentense in _jsonSentenses)
    {
        _sentence.Enqueue(sentense);
    }
    foreach (int whoIsSpeaking in _thingModel._indexesOfSpeakers)
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


public class ThingModel: MonoBehaviour
{
    public string[] _ruLines;
    public string[] _euLines; 
    public int[] _indexesOfSpeakers;
    public string[] _namesOfTheSpeakers;
}

/*  IEnumerator NextDialogueStage()
    {
       yield return new WaitForSeconds(_dialogueTimerValue);
       DisplayNextLine();
    } */

