using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _textLoading;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
        
    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 1;
    }

    public void Hide() => 
        FadeInAsync();

    private async void FadeInAsync()
    {
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= 0.03f;
            await Task.Delay(3);
        }

        gameObject.SetActive(false);
    }

    public void UpdateProgress(float progress)
    {
        _textLoading.text = Mathf.RoundToInt(progress * 100) + "%";
        _slider.value = progress;
    }
}
