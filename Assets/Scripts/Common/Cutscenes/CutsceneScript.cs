using UnityEngine;
using UnityEngine.Playables;

public class CutsceneScript : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    private void Start()
    {
        _playableDirector = GameObject.FindWithTag("Cutscene").GetComponent<PlayableDirector>();
    }

    public void Pause()
    {
        _playableDirector.Pause();
    }

    public void Continue()
    {
        _playableDirector.Play();
    }

    public void CutsceneOff()
    {
        Destroy(gameObject);
    }
}
