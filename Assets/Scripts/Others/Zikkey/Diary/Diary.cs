using Common.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Diary : DiaryData
{
    [SerializeField] private int _maxLines = 24;
    [SerializeField] private int _maxTurns = 20;

    [SerializeField] private float _charChangeSpeed = 0.1f;

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


    private bool _openedInEdit = false;
    private bool _lockClose = false;

    private void Awake()
    {
        Load();
        SwapPage(KeysData.Turn);

        _close.onClick.AddListener(Close);
        _prevPage.onClick.AddListener(() => SwapPage(false));
        _nextPage.onClick.AddListener(() => SwapPage(true));

        _firstPage.lineLimit = _maxLines;
        _secondPage.lineLimit = _maxLines;
    }

    private void Start()
    {
        //UnlockPage(1);
        //UnlockPage(2);
        //UnlockPage(4);
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
        if (_lockClose)
            return;
        Lock();
        GameObject.FindWithTag("Player").GetComponent<movement>().SetWalk(false);
        _diary.SetActive(true);

        _openedInEdit = true;
        LoadData(_openedInEdit);
    }

    public void ReadToggle()
    {
        if (_lockClose)
            return;
        Lock();
        GameObject.FindWithTag("Player").GetComponent<movement>().SetWalk(_diary.activeSelf);
        _diary.SetActive(!_diary.activeSelf);
        
        if (_diary.activeSelf)
            _openedInEdit = false;
    }

    public void Close()
    {
        if (_lockClose)
            return;
        Lock();
        GameObject.FindWithTag("Player").GetComponent<movement>().SetWalk(true);
        _diary.SetActive(false);

        _openedInEdit = false;
    }

    private void SwapPage(bool forward)
    {
        SwapPage(forward ? _turn + 1 : _turn - 1);
    }

    private void SwapPage(int turn)
    {
        if (_lockClose)
            return;

        _turn = turn;
        _turn = Mathf.Clamp(_turn, firstPageIndex, _maxTurns);

        _firstPageNumber.text = (firstPageIndex + pageOffset).ToString();
        _secondPageNumber.text = (secondPageIndex + pageOffset).ToString();

        _firstPage.text = string.Empty;
        _secondPage.text = string.Empty;

        KeysData.Turn = _turn;
        KeysData.RequiredTurn = _turn;
        LoadData(_openedInEdit);
        Save();
    }

    private void LoadData(bool dynamically = false)
    {
        if (KeysData.RequiredTurn != KeysData.Turn)
        {
            KeysData.Turn = KeysData.RequiredTurn;
            _turn = KeysData.Turn;
            Save();
            SwapPage(KeysData.Turn);
            return;
        }

        int firstPage = firstPageIndex + pageOffset;
        int secondPage = secondPageIndex + pageOffset;

        if (KeysData.Diary.ContainsKey(firstPage) && KeysData.UnlockedPages.Contains(firstPage))
        {
            string data = KeysData.Diary[firstPage];
            if (dynamically && KeysData.AnimatedPages.Contains(firstPage) == false)
                StartCoroutine(LoadTextDynamicly(_firstPage, data, firstPage));
            else if (dynamically == false && KeysData.AnimatedPages.Contains(firstPage) == false)
                _firstPage.text = string.Empty;
            else
                _firstPage.text = data;
        }
        else
            _firstPage.text = string.Empty;

        if (KeysData.Diary.ContainsKey(secondPage) && KeysData.UnlockedPages.Contains(secondPage))
        {
            string data = KeysData.Diary[secondPage];

            if (dynamically && KeysData.AnimatedPages.Contains(secondPage) == false)
                StartCoroutine(LoadTextDynamicly(_secondPage, data, secondPage));
            else if (dynamically == false && KeysData.AnimatedPages.Contains(secondPage) == false)
                _secondPage.text = string.Empty;
            else
                _secondPage.text = data;
        }
        else
            _secondPage.text = string.Empty;
    }

    private IEnumerator LoadTextDynamicly(TMPro.TMP_InputField inputField, string text, int page)
    {
        while (_lockClose)
        {
            yield return new WaitForSeconds(_charChangeSpeed);
        }

        int current = 0;
        inputField.text = "";

        _lockClose = true;

        while (current != text.Length)
        {
            inputField.text += text[current];
            yield return new WaitForSeconds(_charChangeSpeed);
            current += 1;
        }

        KeysData.AnimatedPages.Add(page);
        Save();

        _lockClose = false;
    }

    public void UnlockPage(int page = firstPageIndex)
    {
        if (KeysData.UnlockedPages.Contains(page))
            return;
        KeysData.UnlockedPages.Add(page);
        KeysData.RequiredTurn = Mathf.RoundToInt(page / 2f);
        Save();
    }

    public void LockPage(int page = firstPageIndex)
    {
        KeysData.UnlockedPages.Remove(page);
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
