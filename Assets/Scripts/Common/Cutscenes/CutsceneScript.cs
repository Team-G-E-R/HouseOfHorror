using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneScript : MonoBehaviour
{
    private PlayableDirector _playableDirector;
    public float TimeToJump;

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

    public void CutsceneToStart()
    {
        _playableDirector.time = 0;
    }

    public void JumpToTime()
    {
        _playableDirector.time = TimeToJump;
    }
}
