using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TagGame : MonoBehaviour
{
    [SerializeField] private UnityEvent _onWin;

    [SerializeField] private RectTransform _container;
    [SerializeField] private TagUnit _unitPrefab;
    [SerializeField] private Vector2Int _gridSize = new(3, 3);
    [SerializeField] private TagUnit[,] _grid;

    private const int INITIAL_OFFSET = -1;
    private const int RESULT_OFFSET = 1;

    private void OnEnable()
    {
        InitializeField();
        GenerateField();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void InitializeField()
    {
        _grid = new TagUnit[_gridSize.x, _gridSize.y];

        for (int x = 0; x < _gridSize.x; x++)
            for (int y = 0; y < _gridSize.y; y++)
            {
                TagUnit unit = Instantiate(_unitPrefab, _container);
                _grid[x, y] = unit;
                unit.Initialize(this, x, y);
            }
    }

    public void GenerateField()
    {
        List<int> range = Enumerable.Range(0, _gridSize.x * _gridSize.y).ToList();

        for (int x = 0; x < _gridSize.x; x++)
            for (int y = 0; y < _gridSize.y; y++)
            {
                TagUnit current = _grid[x, y];

                int rangeIndex = Random.Range(0, range.Count);
                current.SetValue(range[rangeIndex]);
                range.RemoveAt(rangeIndex);
            }


        if (IsWin() || !SolutionExists())
            GenerateField();
    }

    public void Move(TagUnit unit)
    {
        Vector2Int coordinates = unit.Coordinates;
        TagUnit[] relatives = GetCloseUnits(coordinates);
        print(relatives.Length);

        foreach (TagUnit relative in relatives)
            if (relative.Value == 0)
            {
                relative.SetValue(unit.Value);
                unit.SetValue(0);
            }

        if (IsWin())
            _onWin.Invoke();
    }

    private bool SolutionExists()
    {
        TagUnit[] convertedGrid = _grid.Cast<TagUnit>().ToArray();

        int inversion = 0;
        for (int i = 0; i < _gridSize.x * _gridSize.y; ++i)
            for (int j = 0; j < i; ++j)
                    if (convertedGrid[j].Value > convertedGrid[i].Value)
                        inversion += 1;

        return inversion % 2 == 0;
    }

    private TagUnit[] GetCloseUnits(Vector2Int coordinates)
    {
        List<TagUnit> result = new List<TagUnit>();

        if (IsInGrid(coordinates.x + INITIAL_OFFSET, 0))
            result.Add(_grid[coordinates.x + INITIAL_OFFSET, coordinates.y]);
        if (IsInGrid(coordinates.x + RESULT_OFFSET, 0))
            result.Add(_grid[coordinates.x + RESULT_OFFSET, coordinates.y]);

        if (IsInGrid(0, coordinates.y + INITIAL_OFFSET))
            result.Add(_grid[coordinates.x, coordinates.y + INITIAL_OFFSET]);
        if (IsInGrid(0, coordinates.y + RESULT_OFFSET))
            result.Add(_grid[coordinates.x, coordinates.y + RESULT_OFFSET]);

        return result.ToArray();
    }

    private bool IsInGrid(int x, int y) => IsInGrid(new Vector2Int(x, y));
    private bool IsInGrid(Vector2Int coordinates) => coordinates.x >= 0 && coordinates.x < _gridSize.x && coordinates.y >= 0 && coordinates.y < _gridSize.y;

    private bool IsWin()
    {
        int current = 1;
        for (int x = 0; x < _gridSize.x; x++)
            for (int y = 0; y < _gridSize.y; y++)
            {
                if (_grid[x, y].Value != current)
                    return false;
                else if (current == 8)
                    current = 0;
                else
                    current += 1;
            }

        return true;
    }
}
