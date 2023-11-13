using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private LoadLevel _loadLevel;
    [SerializeField] private string _nextLevel;
    
    public void NextScene()
    {
        SceneManager.LoadScene(_nextLevel, LoadSceneMode.Single);
    }
}
