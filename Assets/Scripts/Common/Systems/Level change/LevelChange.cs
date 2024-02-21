using System.Collections;
using Common.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLevel : MonoBehaviour
{
    [SerializeField] private LevelConnection _levelConnection;
    [SerializeField] private string _targetSceneName;
    [SerializeField] private Transform _spawnPointPlayer;
    [SerializeField] private Transform _spawnPointCamera;
    [SerializeField] private GameObject playerDebug;
    
    FadeInOut fade;

    private void Awake()
    {
        if (_levelConnection == LevelConnection.ActiveConnection)
        {
            StartCoroutine(PlayerOffOn());
        }
    }

    public void SetPlayer()
    {
        var player = GameObject.FindWithTag("Player").transform;
        if (player != null || playerDebug != null)
        {
            LevelConnection.ActiveConnection = _levelConnection;
            fade = FindObjectOfType<FadeInOut>();
            StartCoroutine(LoadLevel());   
        }
    }
    public IEnumerator LoadLevel()
    {
        Debug.Log("Tries to Load " + _targetSceneName);
        GameObject.FindWithTag("Player").GetComponent<movement>().enabled = false;
        fade.FadeIn();
        yield return new WaitForSeconds(fade.duration + 1);
        SceneManager.LoadScene(_targetSceneName);
    }

    public IEnumerator PlayerOffOn()
    {
        GameObject.FindWithTag("Player").GetComponent<movement>().TurnOffMovement();
        yield return new WaitForFixedUpdate();
        GameObject.FindWithTag("Player").transform.position = _spawnPointPlayer.position;
        GameObject.FindWithTag("MainCamera").transform.position = _spawnPointCamera.position;
        yield return new WaitForFixedUpdate();
        GameObject.FindWithTag("Player").GetComponent<movement>().TurnOnMovement();
    }
}