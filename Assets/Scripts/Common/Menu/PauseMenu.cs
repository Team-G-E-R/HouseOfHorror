using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Data _data;
    private bool isMenuActive = false;
    private Slider _soundVolume;

    private AudioSource _audioSource;
    private GameObject[] _allAudio;

    private void Awake()
    {
        FoundsObj();
    }

    private void Start()
    {
        //FoundsObj();
        
        _soundVolume.value = _data.GameData.Volume;
        transform.GetChild(0).gameObject.SetActive(false);
        
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) MenuActive();
    }

    private void FoundsObj()
    {
        if (GameObject.FindWithTag("Data") == null)
        {
            GameObject NewData = new GameObject();
            NewData.name = "Data";
            NewData.tag = "Data";
            _data = NewData.AddComponent<Data>();
            _data.Load();
            DontDestroyOnLoad(_data);
        }
        else _data = GameObject.FindWithTag("Data").GetComponent<Data>();
        _allAudio = GameObject.FindGameObjectsWithTag("Audio");
        _soundVolume = GetComponentInChildren<Slider>();
    }

    public void VolumeSet()
    {
        foreach (var obj in _allAudio)
        {
            _audioSource = obj.GetComponent<AudioSource>();
            _audioSource.volume = _soundVolume.value;
        }
    }

    private void MenuActive()
    {
        isMenuActive = !isMenuActive;

        if (isMenuActive)
        {
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
        SaveData();
        MenuActive();
    }
    
    public void MainMenuButton()
    {
        CursorOff();
        SaveData();
        
        Destroy(_data.gameObject);
        foreach (var go in _allAudio)
        {
            Destroy(go);
        }

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        
        Destroy(gameObject);
    }

    private void SaveData()
    {
        _data.GameData.Volume = _soundVolume.value;
        _data.Save();
    }
    
    private void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    private void CursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
