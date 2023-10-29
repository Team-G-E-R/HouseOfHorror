using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Table : MonoBehaviour
{
    private const int SIZE = 3;
    private const float DECREASE_SIZE = 3f;

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
                    if (x == SIZE - 1 && y == SIZE - 1)
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
        for (int i = 0; i < table.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (table[j] > table[i])
                    countInvarsions++;
            }
        }
        return countInvarsions % 2 == 0;
    }
    public void Generate()
    {
        Clear();

        int[,] table = GenerateTable();
        float xOffset = 2f;
        float yOffset = 1f; // »значальное смещение по оси Y
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                if (table[x, y] == 0)
                    break;
                var cell = Instantiate(cellPrefab);
                cell.transform.position = new Vector3(x / DECREASE_SIZE + xOffset, y / DECREASE_SIZE + yOffset, 0f); // »зменение позиции по оси Y дл€ каждой €чейки
                cell.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                cell.Number = table[x, y];
                this.table[x, y] = cell;
            }
            yOffset += 0.1f; // ”величиваем смещение по оси Y дл€ следующей строки €чеек
        }
    }

    public bool WinGame()
    {
        if (table[0, 0] != null)
        {
            return false;
        }
        int prev = 0;
        for (int y = SIZE - 1; y >= 0; y--)
        {
            for (int x = SIZE - 1; x >= 0; x--)
            {
                if (table[x, y] == null)
                {
                    if (x == 0 && y == 0)
                        return true;
                    break;
                }

                if (prev > table[x, y].Number)
                    return false;

                prev = table[x, y].Number;
            }
        }

        return true;
    }

    private Vector2Int FindCellCoordinates(Cell cell)
    {
        for (int y = 0; y < SIZE; y++)
            for (int x = 0; x < SIZE; x++)
                if (cell == table[x, y])
                    return new Vector2Int(x, y);

        return Vector2Int.one * -1;
    }

    public bool TryMove(Cell cell)
    {
        Vector2Int coordinates = FindCellCoordinates(cell);

        List<Vector2Int> dxdy = new List<Vector2Int>()
        {
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
        };

        for (int i = 0; i < dxdy.Count; i++)
        {
            int xx = coordinates.x + dxdy[i].x;
            int yy = coordinates.y + dxdy[i].y;
            if (xx >= 0 && xx < SIZE && yy >= 0 && yy < SIZE)
            {
                if (table[xx, yy] == null)
                {
                    Vector3 position = cell.transform.position;
                    float offsetX = dxdy[i].x / DECREASE_SIZE;
                    float offsetY = dxdy[i].y / DECREASE_SIZE + (dxdy[i].y * 0.1f);
                    position.x += offsetX;
                    position.y += offsetY;

                    cell.Move(coordinates, new Vector2Int(xx, yy), position.x, position.y);
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
        table[cell.PrevIndex.x, cell.PrevIndex.y] = null;
        table[cell.NewIndex.x, cell.NewIndex.y] = cell;
        OnMoveComplete?.Invoke();
    }
}
