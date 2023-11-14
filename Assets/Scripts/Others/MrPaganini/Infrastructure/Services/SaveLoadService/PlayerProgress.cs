using System;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    public WorldData WorldData;
    public InventoryData InventoryData; 

    public PlayerProgress(string initialLevel)
    {
        Debug.Log("Constructor PlayerProgress");
        WorldData = new WorldData(initialLevel);
        InventoryData = new InventoryData();
    }
    
}