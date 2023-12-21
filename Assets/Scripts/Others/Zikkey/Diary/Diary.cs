using Common.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Diary : DiaryData
{
    [SerializeField] private int _maxLines = 24;
    [SerializeField] private int _maxPages = 500;

    [SerializeField] private Button _close;
    [SerializeField] private Button _prevPage;
    [SerializeField] private Button _nextPage;

    [SerializeField] private TMPro.TMP_InputField _firstPage;
    [SerializeField] private TMPro.TMP_InputField _secondPage;
    [SerializeField] private TMPro.TMP_Text _firstPageNumber;
    [SerializeField] private TMPro.TMP_Text _secondPageNumber;

    [SerializeField] private GameObject _diary;
    [SerializeField] private KeyCode _interactKey = KeyCode.Escape;

    private int _turn = 1;

    private const int firstPageIndex = 1;
    private const int secondPageIndex = 2;
    private int pageOffset => (_turn - 1) * 2;

    private void Awake()
    {
        Load();
        SwapPage(KeysData.Turn);

        _close.onClick.AddListener(Close);
        _prevPage.onClick.AddListener(() => SwapPage(false));
        _nextPage.onClick.AddListener(() => SwapPage(true));

        _firstPage.onEndEdit.AddListener((val) => SaveData(1));
        _secondPage.onEndEdit.AddListener((val) => SaveData(2));
        _firstPage.lineLimit = _maxLines;
        _secondPage.lineLimit = _maxLines;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            ReadToggle();
        }
    }

    public void EditOpen()
    {
        Unlock();
        GameObject.FindWithTag("Player").GetComponent<movement>().SetWalk(false);
        _diary.SetActive(true);
    }

    public void ReadToggle()
    {
        Lock();
        GameObject.FindWithTag("Player").GetComponent<movement>().SetWalk(_diary.activeSelf);
        _diary.SetActive(!_diary.activeSelf);
    }

    public void Close()
    {
        Lock();
        GameObject.FindWithTag("Player").GetComponent<movement>().SetWalk(true);
        _diary.SetActive(false);
    }

    private void SwapPage(bool forward)
    {
        SwapPage(forward ? _turn + 1 : _turn - 1);
    }

    private void SwapPage(int turn)
    {
        _turn = turn;
        _turn = Mathf.Clamp(_turn, firstPageIndex, _maxPages);

        _firstPageNumber.text = (firstPageIndex + pageOffset).ToString();
        _secondPageNumber.text = (secondPageIndex + pageOffset).ToString();

        KeysData.Turn = _turn;
        LoadData();
        Save();
    }

    private void LoadData()
    {
        if (KeysData.Diary.ContainsKey(firstPageIndex + pageOffset))
            _firstPage.text = KeysData.Diary[firstPageIndex + pageOffset];
        else
            _firstPage.text = string.Empty;

        if (KeysData.Diary.ContainsKey(secondPageIndex + pageOffset))
            _secondPage.text = KeysData.Diary[secondPageIndex + pageOffset];
        else
            _secondPage.text = string.Empty;
    }

    private void SaveData(int page = firstPageIndex)
    {
        KeysData.Diary[page + pageOffset] = page == firstPageIndex ? _firstPage.text : _secondPage.text;
        Save();
    }

    private void Lock()
    {
        _firstPage.readOnly = true;
        _secondPage.readOnly = true;
    }

    private void Unlock()
    {
        _firstPage.readOnly = false;
        _secondPage.readOnly = false;
    }  
}
