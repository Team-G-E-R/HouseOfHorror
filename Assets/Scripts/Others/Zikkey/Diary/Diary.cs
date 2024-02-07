using Common.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    public SaveLoad GameData => SaveLoad.Instance; 
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
        SwapPage(GameData.PlayerData.Turn);

        _close.onClick.AddListener(Close);
        _prevPage.onClick.AddListener(() => SwapPage(false));
        _nextPage.onClick.AddListener(() => SwapPage(true));

        _firstPage.lineLimit = _maxLines;
        _secondPage.lineLimit = _maxLines;
    }

    // private void Start()
    // {
    //     //UnlockPage(1);
    //     //UnlockPage(2);
    //     //UnlockPage(4);
    // }

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

        GameData.PlayerData.Turn = _turn;
        GameData.PlayerData.RequiredTurn = _turn;
        LoadData(_openedInEdit);
        GameData.Save();
    }

    private void LoadData(bool dynamically = false)
    {
        if (GameData.PlayerData.RequiredTurn != GameData.PlayerData.Turn)
        {
            GameData.PlayerData.Turn = GameData.PlayerData.RequiredTurn;
            _turn = GameData.PlayerData.Turn;
            GameData.Save();
            SwapPage(GameData.PlayerData.Turn);
            return;
        }

        int firstPage = firstPageIndex + pageOffset;
        int secondPage = secondPageIndex + pageOffset;

        if (GameData.PlayerDict.Diary.ContainsKey(firstPage) && GameData.PlayerData.UnlockedPages.Contains(firstPage))
        {
            string data = GameData.PlayerDict.Diary[firstPage];
            if (dynamically && GameData.PlayerData.AnimatedPages.Contains(firstPage) == false)
                StartCoroutine(LoadTextDynamicly(_firstPage, data, firstPage));
            else if (dynamically == false && GameData.PlayerData.AnimatedPages.Contains(firstPage) == false)
                _firstPage.text = string.Empty;
            else
                _firstPage.text = data;
        }
        else
            _firstPage.text = string.Empty;

        if (GameData.PlayerDict.Diary.ContainsKey(secondPage) && GameData.PlayerData.UnlockedPages.Contains(secondPage))
        {
            string data = GameData.PlayerDict.Diary[secondPage];

            if (dynamically && GameData.PlayerData.AnimatedPages.Contains(secondPage) == false)
                StartCoroutine(LoadTextDynamicly(_secondPage, data, secondPage));
            else if (dynamically == false && GameData.PlayerData.AnimatedPages.Contains(secondPage) == false)
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

        GameData.PlayerData.AnimatedPages.Add(page);
        GameData.Save();

        _lockClose = false;
    }

    public void UnlockPage(int page = firstPageIndex)
    {
        if (GameData.PlayerData.UnlockedPages.Contains(page))
            return;
        GameData.PlayerData.UnlockedPages.Add(page);
        GameData.PlayerData.RequiredTurn = Mathf.RoundToInt(page / 2f);
        GameData.Save();
    }

    public void LockPage(int page = firstPageIndex)
    {
        GameData.PlayerData.UnlockedPages.Remove(page);
        GameData.Save();
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
