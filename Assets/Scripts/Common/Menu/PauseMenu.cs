using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Finder
{
    private bool _isMenuActive = false;
    private Slider _soundVolume;
    
    private GameObject[] _allAudio;

    private void Start()
    {
        FindObjs();

        _allAudio = GameObject.FindGameObjectsWithTag("Audio");
        _soundVolume = gameObject.GetComponentInChildren<Slider>();
        _soundVolume.value = GameData.Volume;
        transform.GetChild(0).gameObject.SetActive(false);
        
        DontDestroyOnLoad(gameObject);
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

    private void MenuActive()
    {
        _isMenuActive = !_isMenuActive;

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
        
        Destroy(gameObject);
    }

    private void SaveSettingsData()
    {
        GameData.Volume = _soundVolume.value;
        SaveLoad.Instance.Save();
    }
}
