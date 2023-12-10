using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : Finder
{
   [SerializeField] private int _nextSceneIndex;
   [SerializeField] private AudioClip _audioStartBtn;
   
   private AudioSource _audioSourceStartBtn;
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
      _audioSourceStartBtn = audioFile.AddComponent<AudioSource>();
      _audioSourceStartBtn.clip = _audioStartBtn;
      _audioSourceStartBtn.volume = SettingsDataobj.GameSettingsData.Volume;
   }

   public void SceneLoad()
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      SaveSettingsData();
      _audioSourceStartBtn.volume = SettingsDataobj.GameSettingsData.Volume;
      _audioSourceStartBtn.Play();
      AudioSourceObj.Stop();
      StartCoroutine(FadeInTransition());
   }
   
   private void SaveSettingsData()
   {
      SettingsDataobj.GameSettingsData.Volume = _volumeSlider.value;
      SettingsDataobj.Save();
   }

   public void VolumeSet()
   {
      AudioSourceObj.volume = _volumeSlider.value;
   }

   public void ExitGame()
   {
      SaveSettingsData();
      Application.Quit();
   }

   private IEnumerator FadeInTransition()
   {
      var fade = GetComponent<FadeInOut>();
      fade.duration = _audioStartBtn.length;
      fade.FadeIn();
      yield return new WaitForSeconds(fade.duration + 0.5f);
      SceneManager.LoadScene(_nextSceneIndex, LoadSceneMode.Single);
   }
}
