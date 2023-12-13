using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : Finder
{
   public AudioClip AudioStartBtn;
   public AudioSource AudioSourceStartBtn;
   [SerializeField] private int _nextSceneIndex;
   
   private Slider _volumeSlider;
   private AsyncOperation _level;
   private const string _audioName = "AudioButton";

   private void Awake()
   {
      _volumeSlider = GetComponentInChildren<Slider>();
      FindObjs();
      _volumeSlider.value = SettingsDataobj.GameSettingsData.Volume;
      CreateAudioBtn();
   }
   
   private void CreateAudioBtn()
   {
      GameObject audioFile = new GameObject();
      audioFile.name = _audioName;
      AudioSourceStartBtn = audioFile.AddComponent<AudioSource>();
      AudioSourceStartBtn.clip = AudioStartBtn;
      AudioSourceStartBtn.volume = SettingsDataobj.GameSettingsData.Volume;
   }

   public void SceneLoad()
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      SaveSettingsData();
      StartBtnPlay();
      StartCoroutine(FadeInTransition());
   }

   public void StartBtnPlay()
   {
      AudioSourceStartBtn.volume = SettingsDataobj.GameSettingsData.Volume;
      AudioSourceStartBtn.Play();
      foreach (var a in AudioSourceObj)
      {
         a.clip = null;  
      }
   }
   
   public void SaveSettingsData()
   {
      SettingsDataobj.GameSettingsData.Volume = _volumeSlider.value;
      SettingsDataobj.Save();
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
      SaveSettingsData();
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
