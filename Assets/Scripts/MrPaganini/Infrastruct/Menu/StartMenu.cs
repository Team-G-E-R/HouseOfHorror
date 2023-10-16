using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _startMenuSound;

    private const string NextLevel = "Level 1";
    
    private LoadLevel _loadLevel;
    private AudioSource _audioSource;

    public void Construct(GameFactory gameFactory,
        IAudioService audioService)
    {
        _loadLevel = new LoadLevel(gameFactory);

        StartMusic(audioService);
    }

    public void StartGame()
    {
        _loadLevel.EnterLevel(NextLevel);
    }

    public void ExitGame() => 
        Application.Quit();

    private void StartMusic(IAudioService audioService)
    {
        _audioSource = audioService.AudioSource;
        _audioSource.clip = _startMenuSound;
        _audioSource.Play();
        _audioSource.volume = 0.2f;
    }
}
