using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
   [SerializeField] private int _nextSceneIndex;
   [SerializeField] private AudioClip _audioStartBtn;

   private Data _data;
   private AudioSource _audioSource;
   private AudioSource _audioSourceStartBtn;
   private Slider _volumeSlider;
   private AsyncOperation _level;
   private const string AudioName = "AudioButton";

   private void Start()
   {
      FoundObjs();
      CreateAudioBtn();
      _volumeSlider.value = _data.GameData.Volume;
   }

   private void FoundObjs()
   {
      _data = GameObject.FindWithTag("Data").GetComponent<Data>();
      _audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
      _volumeSlider = GetComponentInChildren<Slider>();
   }

   public void SceneLoad()
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      SaveData();
      _audioSourceStartBtn.Play();
      _audioSource.Stop();
      StartCoroutine(FadeInTransition());
   }

   public void VolumeSet()
   {
      _audioSource.volume = _volumeSlider.value;
   }

   public void ExitGame()
   {
      SaveData();
      Application.Quit();
   }
   
   private void SaveData()
   {
      _data.GameData.Volume = _volumeSlider.value;
      _data.Save();
   }

   private void CreateAudioBtn()
   {
      GameObject audioFile = new GameObject();
      audioFile.name = AudioName;
      _audioSourceStartBtn = audioFile.AddComponent<AudioSource>();
      _audioSourceStartBtn.clip = _audioStartBtn;
      _audioSourceStartBtn.volume = _data.GameData.Volume;
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
