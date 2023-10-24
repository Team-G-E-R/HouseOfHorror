using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class DialogueTrigger : MonoBehaviour
{
    public DialogueWindow dialogue;
    private bool DiaPlaying;
   
    public void ActivateDialogue()
    {   
        DiaPlaying=FindObjectOfType<DialogueManager>()._dialogueIsPlaying;
        if(DiaPlaying==false)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
          
    }

    private void Update() 
    {
       if(Input.GetKeyDown(KeyCode.H))
        {
            ActivateDialogue();
        } 
    }
    
   
}
