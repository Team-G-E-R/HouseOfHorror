using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Table : MonoBehaviour
{
    private const int SIZE = 3;

    public event System.Action OnMoveComplete;
                
    [SerializeField]
    private Cell cellPrefab;

    private Cell[,] table;

    private void Clear()
    {
        var cells = FindObjectsOfType<Cell>();
        foreach (var cell in cells) 
            Destroy(cell.gameObject);

        table = new Cell[SIZE, SIZE];
    }
    private int[,] GenerateTable()
    {
        int[,] table = new int[SIZE, SIZE];

        do
        {
                List<int> numbers = Enumerable.Range(1, 8).ToList();

                for (int y = 0; y < SIZE; y++)
                {
                    for (int x = 0; x < SIZE; x++)
                    {
                        if (x == SIZE - 1 && y  == SIZE - 1)
                            continue;

                        int index = Random.Range(0, numbers.Count);
                        table[x, y] = numbers[index];
                        numbers.RemoveAt(index);
                    }
                }
        }
        while (IsSolvable(table.Cast<int>().ToArray()));
        return table;

    }
    private bool IsSolvable(int[] table)
    {
        int countInvarsions = 0;    
        for (int i = 0;i < table.Length; i++)
        {
            for(int j = 0; j < i; j++)
            {
                if (table[j] > table[i])    
                    countInvarsions++;
            }
        }
        return countInvarsions % 2 == 0;
    }
    private void Generate()
    {
        Clear();

        int[,] table = GenerateTable();
        float yOffset = 0f; // »значальное смещение по оси Y
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                if (table[x, y] == 0)
                    break;
                var cell = Instantiate(cellPrefab);
                cell.transform.position = new Vector3(x / 3f + 2f, y / 3f + 1f + yOffset, 0f); // »зменение позиции по оси Y дл€ каждой €чейки
                cell.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                cell.Number = table[x, y];
                this.table[x, y] = cell;
            }
            yOffset += 0.1f; // ”величиваем смещение по оси Y дл€ следующей строки €чеек
        }
    }
    public bool TryMove(Cell cell)
    {
        int x = -Mathf.RoundToInt(cell.transform.position.x);
        int y = Mathf.RoundToInt(cell.transform.position.y);

        List<Vector2Int> dxdy = new List<Vector2Int>()
    {
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0),

    };


        for (int i = 0; i < dxdy.Count; i++)
        {
            int xx = x + dxdy[i].x;
            int yy = y + dxdy[i].y;
            if (xx >= 0 && xx < SIZE && yy >= 0 && yy < SIZE)
            {
                Debug.Log("8");
                if (table[xx, yy] == null)
                {
                    Debug.Log("9");
                    cell.Move(-xx, yy);
                    cell.OnPositionChanged += Cell_OnPositionChanged;
                    return true;

                }
            }
        }
        return false;


    }

    private void Cell_OnPositionChanged(Cell cell, Vector3 prev, Vector3 curr)
    {
        cell.OnPositionChanged -= Cell_OnPositionChanged;
        Debug.Log("2");
        int x = -Mathf.RoundToInt(prev.x);
        int y = Mathf.RoundToInt(prev.y);
        table[x, y] = null;
        x = -Mathf.RoundToInt(curr.x );
        Debug.Log("1");
        y = Mathf.RoundToInt(curr.y);
        table[x, y] = cell;
        OnMoveComplete?.Invoke();
        Debug.Log("0");

    }

    void Start()
    {
        Generate();

    }

    
}
