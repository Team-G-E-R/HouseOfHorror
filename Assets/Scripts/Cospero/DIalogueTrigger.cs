using Common.Scripts;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueWindow dialogue;
    private bool _diaPlaying;

    public void ActivateDialogue()
    {
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Activator>().enabled = false;
        _diaPlaying = FindObjectOfType<DialogueManager>()._dialogueIsPlaying;
        
        if(_diaPlaying == false) FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
