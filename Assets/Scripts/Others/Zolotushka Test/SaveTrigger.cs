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

    void OnTriggerEnter(Collider other)
    {
        FindPlayerSettingsData();
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
