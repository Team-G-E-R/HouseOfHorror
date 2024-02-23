using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : Finder
{
   public SaveLoad.GameInfo NewGameData => SaveLoad.Instance.PlayerData;
   public AudioClip AudioStartBtn;
   public AudioSource AudioSourceStartBtn;
   [SerializeField] private int _nextSceneIndex;
   
   private Slider _volumeSlider;
   private const string _audioName = "AudioButton";

   private void Start()
   {
      _volumeSlider = GetComponentInChildren<Slider>();
      FindObjs();
      _volumeSlider.value = GameData.Volume;
      CreateAudioBtn();
   }
   
   private void CreateAudioBtn()
   {
      GameObject audioFile = new GameObject();
      audioFile.name = _audioName;
      AudioSourceStartBtn = audioFile.AddComponent<AudioSource>();
      AudioSourceStartBtn.clip = AudioStartBtn;
      AudioSourceStartBtn.volume = GameData.Volume;
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
         a.clip = null;  
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

   private IEnumerator FadeInTransition()
   {
      var fade = GetComponent<FadeInOut>();
      fade.duration = AudioStartBtn.length;
      fade.FadeIn();
      yield return new WaitForSeconds(fade.duration + 0.5f);
      SceneManager.LoadScene(_nextSceneIndex, LoadSceneMode.Single);
   }
}
