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
    public bool IsRussian;
    public TMP_Text DialogueTextUI;
    public TMP_Text SpeakerNameUI;
    public Queue<int> IndexOfSpeaker; 
    public Queue<string> Sentence;
    public bool DialogueIsPlaying;
    public GameObject DialogueObjUI;
    public Image DialogueImageUI;
    
    private string _curSentanceText;
    private string _dialogueFileName;
    private string[] _charName;
    private Color[] _dialogueColours;
    private string[] _jsonSentenses;
    [HideInInspector]
    public DialogueTrigger _dialogueTrigger;
    private bool _moveEnable = true;

    private void Start()
    {
        _dialogueTrigger = GameObject.FindWithTag("DialogueTrigger").GetComponent<DialogueTrigger>();
        Sentence = new Queue<string>();
        IndexOfSpeaker = new Queue<int>();
        DialogueIsPlaying = false;
        DialogueObjUI.SetActive(false);
    }
    
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E) == false) return;
        StopAllCoroutines();
        if (DialogueTextUI.text != _curSentanceText) DialogueTextUI.text = _curSentanceText;
        else if (DialogueIsPlaying) DisplayNextLine();
    }
    
    public void StartDialogue(DialogueWindow dialogue)
    {
        MovementOffOn();
        ThingModel _thingModel = new ThingModel();
        var jsonTextFile = dialogue._jsonFile.text;
        
        JsonUtility.FromJsonOverwrite(jsonTextFile, _thingModel);
        
        if (IsRussian) _jsonSentenses = _thingModel._ruLines;
        else _jsonSentenses = _thingModel._euLines;
        
        _charName = _thingModel._namesOfTheSpeakers;
        _dialogueColours = dialogue._charColor;
        
        Sentence.Clear();
        IndexOfSpeaker.Clear();
        DialogueIsPlaying = true;
        DialogueObjUI.SetActive(true);
        
        foreach (string sentense in _jsonSentenses) Sentence.Enqueue(sentense);
        foreach (int whoIsSpeaking in _thingModel._indexesOfSpeakers) IndexOfSpeaker.Enqueue(whoIsSpeaking);
        
        DisplayNextLine();
    }
    
    private void DisplayNextLine()
        {
            StopAllCoroutines();
            
            if (Sentence.Count == 0)
            {
                _dialogueTrigger.Interact();
                EndDialogue();
                return;
            }
            
            string sentense = Sentence.Dequeue();
            int indexOfSpeaker = IndexOfSpeaker.Dequeue();
            _curSentanceText = sentense;
            StartCoroutine(TypeLines(sentense, indexOfSpeaker));
        }
    
    IEnumerator TypeLines(string sentense, int indexOfSpeaker)
        {
            SpeakerNameUI.text = _charName[indexOfSpeaker]; 
            DialogueImageUI.color = _dialogueColours[indexOfSpeaker];
            DialogueTextUI.text = "";
           
            foreach (char letter in sentense.ToCharArray())
            {
                DialogueTextUI.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
    
    private void EndDialogue()
        {
            DialogueIsPlaying = false;
            DialogueObjUI.SetActive(false);
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

public class ThingModel
    {
        public string[] _ruLines;
        public string[] _euLines; 
        public int[] _indexesOfSpeakers;
        public string[] _namesOfTheSpeakers;
    }
