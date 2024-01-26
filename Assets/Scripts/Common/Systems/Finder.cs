using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Finder : MonoBehaviour
{
    [HideInInspector]
    public List<AudioSource> AudioSourceObj = new List<AudioSource>();
    [HideInInspector]
    public SettingsData SettingsDataobj;
    
    private const string SettingsDataName = "GameSettingsData";
    private const string AudioName = "AudioService";

    [HideInInspector]
    public int SceneIndex;
    [HideInInspector]
    public Vector3 PlayerScenePos;
    [HideInInspector]
    public Vector3 CameraPos;

    public void FindObjs()
    {
        if (GameObject.FindWithTag("SettingsData") == null)
        {
            GameObject NewSettingsData = new GameObject();
            NewSettingsData.name = SettingsDataName;
            NewSettingsData.tag = "SettingsData";
            SettingsDataobj = NewSettingsData.AddComponent<SettingsData>();
            SettingsDataobj.Load();
            DontDestroyOnLoad(SettingsDataobj);
        }
        else SettingsDataobj = GameObject.FindWithTag("SettingsData").GetComponent<SettingsData>();

        if (GameObject.FindWithTag("Audio") == null)
        {
            GameObject audioFile = new GameObject();
            audioFile.name = AudioName;
            audioFile.tag = "Audio";
            audioFile.AddComponent<AudioSource>();
            var audio = audioFile.GetComponent<AudioSource>();
            audio.volume = SettingsDataobj.GameSettingsData.Volume;
            AudioSourceObj.Add(audio);
            DontDestroyOnLoad(audioFile);
        }
        else
        {
            GameObject[] allAudio = GameObject.FindGameObjectsWithTag("Audio");
            foreach (var a in allAudio)
            {
                AudioSourceObj.Add(a.GetComponent<AudioSource>());
            }
        }
    }

    public void FindPlayerSettingsData()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerScenePos = GameObject.FindWithTag("Player").transform.position;
        CameraPos = GameObject.FindWithTag("MainCamera").transform.position;
    }
    
    public void SaveMusicTime(float time)
    {
        SettingsDataobj.GameSettingsData.MusicTime = time;
        SettingsDataobj.Save();
    }

    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void CursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
