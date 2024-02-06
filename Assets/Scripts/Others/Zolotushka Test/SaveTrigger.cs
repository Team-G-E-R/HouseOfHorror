using System.IO;
using UnityEngine;

public class SaveTrigger : SaveLoad
{
   public GameInfo PlayerInfo;
   //private string _filePath => Application.streamingAssetsPath + "/save.json";
   private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Save();
        }
    }
}
