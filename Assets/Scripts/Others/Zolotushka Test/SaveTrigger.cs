using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveTrigger : MonoBehaviour
{
   public SaveLoad GameData => SaveLoad.Instance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindPlayerSettingsData();
            GameData.Save();
        }
    }

    public void FindPlayerSettingsData()
    {
        GameData.PlayerData.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameData.PlayerData.PlayerScenePos = GameObject.FindWithTag("Player").transform.position;
        GameData.PlayerData.CameraPos = GameObject.FindWithTag("MainCamera").transform.position;
    }
}
