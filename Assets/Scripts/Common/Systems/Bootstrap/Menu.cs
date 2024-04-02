using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : Finder
{
   public SaveLoad.GameInfo NewGameData => SaveLoad.Instance.PlayerData;
   public AudioSource AudioSourceStartBtn;
   [SerializeField] private int _nextSceneIndex;
   
   private Slider _volumeSlider;
   private const string _audioName = "AudioButton";
   private GameObject _pauseMenu;
   private PauseMenu _pauseScript;

   private void Start()
   {
      _pauseMenu = GameObject.FindGameObjectWithTag("Menu");
      _pauseScript = _pauseMenu.GetComponent<PauseMenu>();
      if (_pauseScript._isMenuActive)
      {
         _pauseScript.MenuActive();
      }
      DontDestroyOnLoad(_pauseMenu);
      _pauseScript.enabled = false;
      
      _volumeSlider = GetComponentInChildren<Slider>();
      _volumeSlider.value = GameData.Volume;
      CursorOn();
      FindObjs();
   }

   public void SceneLoad()
   {
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
      SaveSettingsData(true);
      StartBtnPlay();
      StartCoroutine(FadeInTransition());
   }

   public void StartBtnPlay()
   {
      foreach (var a in AudioSourceObj)
      {
         a.GetComponent<AudioSource>().Stop();  
      }
      AudioSourceStartBtn.volume = NewGameData.Volume;
      AudioSourceStartBtn.Play();
   }
   
   public void SaveSettingsData(bool needToResetData)
   {
      if (needToResetData)
      {
         SaveLoad.Instance.AllDataToZero();  
      }
      NewGameData.Volume = _volumeSlider.value;
      SaveLoad.Instance.Save();
   }

   public void VolumeSet()
   {
      foreach (var a in AudioSourceObj)
      {
         a.volume = _volumeSlider.value;  
      }
   }

   public void ExitGame()
   {
      SaveSettingsData(false);
      Application.Quit();
   }

   [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
   private static void PauseMenuCreating()
   {
      Instantiate(Resources.Load("Pause Menu/Pause Menu"));
   }

   private IEnumerator FadeInTransition()
   {
      var fade = GetComponent<FadeInOut>();
      fade.duration = AudioSourceStartBtn.clip.length;
      fade.FadeIn();
      yield return new WaitForSeconds(fade.duration + 0.5f);
      SceneManager.LoadScene(_nextSceneIndex, LoadSceneMode.Single);
      _pauseScript.enabled = true;
   }
}
