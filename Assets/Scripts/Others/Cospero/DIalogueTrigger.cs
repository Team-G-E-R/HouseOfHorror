using UnityEngine;

public class DialogueTrigger : Interactable
{
    [SerializeField] private DialogueWindow _dialogue;
    private bool _diaPlaying;

    public void ActivateDialogue()
    {
        var manager = FindObjectOfType<DialogueManager>();
        _diaPlaying = manager.DialogueIsPlaying;
        manager._dialogueTrigger = this;

        if (GameObject.FindWithTag("Player") != null)
        {
            Activator player = GameObject.FindWithTag("Player").GetComponent<Activator>();
            player.OneUsage = _oneUsage;   
        }

        if(_diaPlaying == false)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(_dialogue);
        } 
    }
}
