using Common.Scripts;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public DialogueWindow dialogue;
    private bool _diaPlaying;

    public void ActivateDialogue()
    {
        var manager = FindObjectOfType<DialogueManager>();
        _diaPlaying = manager._dialogueIsPlaying;
        manager._dialogueTrigger = this;
        
        if(_diaPlaying == false) FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
