using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ISavedProgress
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadProgress(PlayerProgress playerProgress)
    {
    }

    public void UpdateProgress(PlayerProgress playerProgress)
    {
        Debug.Log("UpdateProgress");
        playerProgress.WorldData = new WorldData("Level 2");
        Debug.Log(playerProgress.WorldData.PositionOnLevel.Level);
    }
}
