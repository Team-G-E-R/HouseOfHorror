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

    void Start()
    {
        raycaster.OnCellHit += Raycaster_OnCellHit;
        table.OnMoveComplete += Table_OnMoveComplete;

    }

    private void Table_OnMoveComplete()
    {
        raycaster.Locked = false;
    }

    private void Raycaster_OnCellHit(Cell cell)
    {
        raycaster.Locked = table.TryMove(cell); //работает
    }
}
