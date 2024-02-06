using System.IO;
using UnityEngine;

public class SaveTrigger : SaveLoad
{
   public GameInfo PlayerInfo;

   private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Save();
        }
    }
}
