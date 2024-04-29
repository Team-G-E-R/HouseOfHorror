using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneScript : MonoBehaviour
{
    private PlayableDirector _playableDirector;
    public float TimeToJump;
    public SaveLoad GameData => SaveLoad.Instance;

    private void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
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

    public void SaveForDiary()
    {
        GameData.PlayerData.HasDiary = true;
        GameData.Save();
    }
}
