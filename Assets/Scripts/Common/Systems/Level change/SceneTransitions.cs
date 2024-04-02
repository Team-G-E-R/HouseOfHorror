using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] private int _nextLevelIndex;
    
    public void NextScene()
    {
        if (GameObject.FindGameObjectWithTag("Menu") == null)
        {
            Instantiate(Resources.Load("Pause Menu/Pause Menu"));
        }
        SceneManager.LoadScene(_nextLevelIndex);
    }
}
