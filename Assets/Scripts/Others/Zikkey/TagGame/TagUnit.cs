using UnityEngine;
using UnityEngine.UI;

public class TagUnit : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMPro.TMP_Text _text;

    [SerializeField] private bool _syncText = false;
    [SerializeField] private bool _syncImage = true;

    private Button _button;

    private TagGame _game;
    private Vector2Int _coordinates = Vector2Int.zero;
    private int _value = 0;

    public Vector2Int Coordinates => _coordinates;
    public int Value => _value;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SendClicked);
        SetValue(0);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SendClicked);
    }

    private void SendClicked()
    {
        _game.Move(this);
    }

    public void Initialize(TagGame game, int x, int y) => Initialize(game, new Vector2Int(x, y));

    public void Initialize(TagGame game, Vector2Int coordinates)
    {
        _game = game;
        SetCoordinates(coordinates);
    }

    public void SetCoordinates(int x, int y)
    {
        _coordinates = new Vector2Int(x, y);
    }

    public void SetCoordinates(Vector2Int coordinates)
    {
        _coordinates = coordinates;
    }

    public void SetValue(int value)
    {
        _value = value;
        _text.text = value == 0 || _syncText == false ? string.Empty : _value.ToString();

        if (_syncImage && _game != null)
            _image.sprite = _game.GetTagIcon(value);
    }
}
