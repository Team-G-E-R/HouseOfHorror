using UnityEngine;

public class Finder : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AudioSourceObj;
    [HideInInspector]
    public GameObject[] AllAudio;
    [HideInInspector]
    public Data Dataobj;
    
    private const string DataName = "GameData";
    private const string AudioName = "AudioService";
    
    public void FindObjs()
    {
        if (GameObject.FindWithTag("Data") == null)
        {
            GameObject NewData = new GameObject();
            NewData.name = DataName;
            NewData.tag = "Data";
            Dataobj = NewData.AddComponent<Data>();
            Dataobj.Load();
            DontDestroyOnLoad(Dataobj);
        }
        else Dataobj = GameObject.FindWithTag("Data").GetComponent<Data>();

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
