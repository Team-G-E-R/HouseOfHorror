using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] private int _nextLevelIndex;

    public void NextScene()
    {
        GameObject.FindGameObjectWithTag("Menu").GetComponent<PauseMenu>().enabled = true;
        SceneManager.LoadScene(_nextLevelIndex);
    }
}
