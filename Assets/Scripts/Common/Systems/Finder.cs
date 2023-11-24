using UnityEngine;
using UnityEngine.SceneManagement;

public class Finder : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AudioSourceObj;
    [HideInInspector]
    public GameObject[] AllAudio;
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
            AudioSourceObj = audioFile.AddComponent<AudioSource>();
            DontDestroyOnLoad(AudioSourceObj);
        }
        else AudioSourceObj = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    public void FindPlayerSettingsData()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerScenePos = GameObject.FindWithTag("Player").transform.position;
        CameraPos = GameObject.FindWithTag("MainCamera").transform.position;
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
