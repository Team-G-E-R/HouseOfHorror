using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DialogueWindow 
{
     
   /*  public float[] dialogueTime; 
    public Sprite[] characterSprite;  
    public string[] name;*/
    [TextArea(3,10)]
    public string[] _sentenses;
    public int[] _indexOfSpeakers;
    /* public string _mainCharName;
    public string _oftherCharName; */
    public string[] _characterNames;
    public Color[] _charColor;
   
}
