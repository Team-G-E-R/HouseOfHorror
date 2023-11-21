using System.Collections;
using System.Collections.Generic;
using Common.Scripts;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue settings")]
    [Space(5)]
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
    [HideInInspector]
    public DialogueTrigger _dialogueTrigger;
    private bool _moveEnable = true;

    private void Start()
    {
        _dialogueTrigger = GameObject.FindWithTag("DialogueTrigger").GetComponent<DialogueTrigger>();
        _sentence = new Queue<string>();
        _indexOfSpeaker = new Queue<int>();
        _dialogueIsPlaying = false;
        _dialogueObjUI.SetActive(false);
    }
    
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E) == false) return;
        StopAllCoroutines();
        if (_dialogueTextUI.text != _curSentanceText) _dialogueTextUI.text = _curSentanceText;
        else if (_dialogueIsPlaying) DisplayNextLine();
    }
    
    public void StartDialogue(DialogueWindow dialogue)
    {
        MovementOffOn();
        ThingModel _thingModel = new ThingModel();
        var jsonTextFile = dialogue._jsonFile.text;
        
        JsonUtility.FromJsonOverwrite(jsonTextFile, _thingModel);
        
        if (_isRussian) _jsonSentenses = _thingModel._ruLines;
        else _jsonSentenses = _thingModel._euLines;
        
        _charName = _thingModel._namesOfTheSpeakers;
        _dialogueColours = dialogue._charColor;
        
        _sentence.Clear();
        _indexOfSpeaker.Clear();
        _dialogueIsPlaying = true;
        _dialogueObjUI.SetActive(true);
        
        foreach (string sentense in _jsonSentenses) _sentence.Enqueue(sentense);
        foreach (int whoIsSpeaking in _thingModel._indexesOfSpeakers) _indexOfSpeaker.Enqueue(whoIsSpeaking);
        
        DisplayNextLine();
    }
    
    private void DisplayNextLine()
        {
            StopAllCoroutines();
            
            if (_sentence.Count == 0)
            {
                _dialogueTrigger.Interact();
                EndDialogue();
                return;
            }
            
            string sentense = _sentence.Dequeue();
            int indexOfSpeaker = _indexOfSpeaker.Dequeue();
            _curSentanceText = sentense;
            StartCoroutine(TypeLines(sentense, indexOfSpeaker));
        }
    
    IEnumerator TypeLines(string sentense, int indexOfSpeaker)
        {
            _dialogueTimerValue = (float)sentense.Length/20f+1.5f;
            _speakerNameUI.text = _charName[indexOfSpeaker];
            _dialogueImageUI.color = _dialogueColours[indexOfSpeaker];
           _dialogueTextUI.text = "";
           
            foreach (char letter in sentense.ToCharArray())
            {
                _dialogueTextUI.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
    
    private void EndDialogue()
        {
            _dialogueIsPlaying = false;
            _dialogueObjUI.SetActive(false);
            MovementOffOn();
        }

    private void MovementOffOn()
    {
        _moveEnable = !_moveEnable;
        var player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Animator>().enabled = _moveEnable;
        player.GetComponent<movement>().enabled = _moveEnable;
        player.GetComponent<Activator>().enabled = _moveEnable;
    }
    }

public class ThingModel: MonoBehaviour
    {
        public string[] _ruLines;
        public string[] _euLines; 
        public int[] _indexesOfSpeakers;
        public string[] _namesOfTheSpeakers;
    }
