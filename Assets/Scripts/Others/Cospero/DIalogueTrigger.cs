using UnityEngine;

public class DialogueTrigger : Interactable
{
    [SerializeField] private DialogueWindow _dialogue;
    private bool _diaPlaying;

    public void ActivateDialogue()
    {
        var manager = FindObjectOfType<DialogueManager>();
        _diaPlaying = manager._dialogueIsPlaying;
        manager._dialogueTrigger = this;
        
        if(_diaPlaying == false) FindObjectOfType<DialogueManager>().StartDialogue(_dialogue);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.H)) ActivateDialogue();
    }
}
