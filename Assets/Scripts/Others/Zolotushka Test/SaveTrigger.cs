using UnityEngine;
using UnityEngine.SceneManagement;

public enum DiarySave
{
    DiarySave1,
    DiarySave2,
    DiarySave3,
    DiarySave4,
    DiarySave5
}

public class SaveTrigger : MonoBehaviour
{
    public DiarySave saveSlot;
    public SaveLoad GameData => SaveLoad.Instance;
    private GameObject _menu;
    private Diary _diary;

    private void Start()
    {
        _menu = GameObject.FindWithTag("Menu");
        _diary = _menu.GetComponentInChildren<Diary>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (GameData.PlayerDict.KeysDict[saveSlot.ToString()] == false)
        {
            FindPlayerSettingsData();
            _diary.EditOpen();
        }
    }

    public void FindPlayerSettingsData()
    {
        GameData.PlayerData.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameData.PlayerData.PlayerScenePos = GameObject.FindWithTag("Player").transform.position;
        GameData.PlayerData.CameraPos = GameObject.FindWithTag("MainCamera").transform.position;
        GameData.PlayerDict.KeysDict[saveSlot.ToString()] = true;
        GameData.Save();
    }
}
