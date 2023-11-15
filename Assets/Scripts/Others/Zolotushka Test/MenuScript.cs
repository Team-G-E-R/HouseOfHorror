using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
   [SerializeField] private int _nextSceneIndex;

   private Slider _volumeSlider;
   private AudioSource _audioSource;
   private SavedDataScript _dataScript;

   private void Start()
   {
      FoundObjs();
      _volumeSlider.value = _dataScript._data.Volume;
   }

   private void FoundObjs()
   {
      _dataScript = GameObject.FindWithTag("Data").GetComponent<SavedDataScript>();
      _volumeSlider = GetComponentInChildren<Slider>();
      _audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
   }

   private void SaveData()
   {
      _dataScript._data.Volume = _volumeSlider.value;
      _dataScript.Save();
   }
   
   public void SceneLoad()
   {
      SaveData();
      SceneManager.LoadScene(_nextSceneIndex, LoadSceneMode.Single);
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
}
