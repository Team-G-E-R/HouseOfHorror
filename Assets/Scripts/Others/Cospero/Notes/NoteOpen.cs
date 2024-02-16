using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteOpen : MonoBehaviour
{
    [SerializeField] Sprite noteSprite;
    [TextArea (3,15)] [SerializeField] string noteText;
     [SerializeField] GameObject NoteManager;
    
    public void OpenNote()
    {
        NoteManager.GetComponent<NoteManager>().FirstStageOpen(noteText, noteSprite);
    }


}
