using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControler : MonoBehaviour
{
    [SerializeField] 
    private Table table;

    [SerializeField]
    private Raycaster raycaster;
    public DoorController DC;

    void Start()
    {
        raycaster.OnCellHit += Raycaster_OnCellHit;
        table.OnMoveComplete += Table_OnMoveComplete;
        table.Generate();
        
        

    }

    private void Table_OnMoveComplete()
    {
        
        raycaster.Locked = false;

        
        Debug.Log("1");
        if (table.WinGame())
        {
            Debug.Log("win");
            table.Generate();
            DC.winGame = true;
        }
        



    }
   

    private void Raycaster_OnCellHit(Cell cell)
    {
        raycaster.Locked = table.TryMove(cell); //работает
    }
}
