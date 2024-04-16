using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Finder
{
    [SerializeField] private GameObject menuUi;
    [HideInInspector]
    public bool _isMenuActive = false;
    public GameObject DiaryButton;
    public GameObject DiaryBackground;
    public GameObject DiaryUI;
    private Slider _soundVolume;
    
    private GameObject[] _allAudio;

    private void Start()
    {
        FindObjs();

        _allAudio = GameObject.FindGameObjectsWithTag("Audio");
        _soundVolume = gameObject.GetComponentInChildren<Slider>();
        _soundVolume.value = GameData.Volume;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) MenuActive();
    }

    public void VolumeSet()
    {
        foreach (var obj in _allAudio)
        {
            obj.GetComponent<AudioSource>().volume = _soundVolume.value;
        }
    }

    public void MenuActive()
    {
        _isMenuActive = !_isMenuActive;

        if (DiaryBackground.activeSelf)
        {
            DiaryUI.GetComponent<Diary>().Close();
            _isMenuActive = !_isMenuActive;
            return;
        }

        if (_isMenuActive)
        {
            _allAudio = GameObject.FindGameObjectsWithTag("Audio");
            CursorOn();
            Time.timeScale = 0f;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            CursorOff();
            Time.timeScale = 1f;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ContinueButton()
    {
        SaveSettingsData();
        MenuActive();
    }
    
    public void MainMenuButton()
    {
        CursorOff();
        SaveSettingsData();
        
        foreach (var go in _allAudio)
        {
            Destroy(go);
        }

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void MenuActiveInactive()
    {
        _isMenuActive = !_isMenuActive;
    }

    private void SaveSettingsData()
    {
        GameData.Volume = _soundVolume.value;
        SaveLoad.Instance.Save();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameData.HasDiary == false)
        {
            var colors = DiaryButton.GetComponent<Button>().colors;
            colors.normalColor = Color.gray;
            DiaryButton.GetComponent<Button> ().colors = colors;
            DiaryButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            DiaryButton.GetComponent<Button>().interactable = true;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
