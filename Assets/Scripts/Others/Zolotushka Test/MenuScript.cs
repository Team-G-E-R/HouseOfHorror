using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
   [SerializeField] private int _nextSceneIndex;

   private Slider _volumeSlider;
   private AudioSource _audioSource;
   private Data _data;

   private void Start()
   {
      FoundObjs();
      _volumeSlider.value = _data.GameData.Volume;
   }

   private void FoundObjs()
   {
      _data = GameObject.FindWithTag("Data").GetComponent<Data>();
      _audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
      _volumeSlider = GetComponentInChildren<Slider>();
   }

   private void SaveData()
   {
      _data.GameData.Volume = _volumeSlider.value;
      _data.Save();
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
