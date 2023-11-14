using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private LoadLevel _loadLevel;
    [SerializeField] private int _nextLevelIndex;
    
    public void NextScene()
    {
        SceneManager.LoadScene(_nextLevelIndex, LoadSceneMode.Single);
    }
}
