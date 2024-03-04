using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;

public class Finder : MonoBehaviour
{
    [HideInInspector]
    public List<AudioSource> AudioSourceObj = new List<AudioSource>();
    [HideInInspector]
    public SaveLoad.GameInfo GameData => SaveLoad.Instance.PlayerData;
    
    private AudioSource _audioObj;
    private const string AudioName = "AudioService";

    [HideInInspector]
    public int SceneIndex;
    [HideInInspector]
    public Vector3 PlayerScenePos;
    [HideInInspector]
    public Vector3 CameraPos;

    public void FindObjs()
    {
        if (GameObject.FindAnyObjectByType<AudioSource>() == null)
        {
            GameObject audioFile = new GameObject();
            audioFile.name = AudioName;
            audioFile.tag = "Audio";
            audioFile.AddComponent<AudioSource>();
            _audioObj = audioFile.GetComponent<AudioSource>();
            _audioObj.volume = GameData.Volume;
            AudioSourceObj.Add(_audioObj);
            DontDestroyOnLoad(audioFile);
        }
        else
        {
            AudioSource[] allAudio = Resources.FindObjectsOfTypeAll<AudioSource>();
            foreach (var a in allAudio)
            {
                a.volume = GameData.Volume;
                AudioSourceObj.Add(a);
            }
        }
    }
    
    public void SaveMusicTime(float time)
    {
        GameData.MusicTime = time;
        SaveLoad.Instance.Save();
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
