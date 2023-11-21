using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] private int _nextLevelIndex;
    
    public void NextScene()
    {
        SceneManager.LoadScene(_nextLevelIndex, LoadSceneMode.Single);
    }
}
