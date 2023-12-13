using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [Header("Fade settings")]
    public float duration;
    [SerializeField] private Image _imageForFade;
    [Space(10)]
    [Header("Will work after fade end working")]
    [Space(5)]
    [SerializeField] private UnityEvent _afterFade;
    [Header("Fade on start settings")]
    [SerializeField] private bool _needToFadeOnStart;
    [SerializeField] private FadeChoose _fade;
    
    public enum FadeChoose
    {
        NeedFadeIn,
        NeedFadeOut
    }
    
    private float _currentTime;

    private void Start()
    {
        if (_needToFadeOnStart && _fade == FadeChoose.NeedFadeIn) FadeIn();
        else if (_needToFadeOnStart && _fade == FadeChoose.NeedFadeOut) FadeOut();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCrt());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCrt());
    }

    private IEnumerator FadeInCrt()
    {
        while (_currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, _currentTime / duration);
            _imageForFade.color = new Color(_imageForFade.color.r, _imageForFade.color.g, _imageForFade.color.b, alpha);
            _currentTime += Time.deltaTime;
            yield return null;
        }
        _currentTime = 0;
        _afterFade.Invoke();
    }

    private IEnumerator FadeOutCrt()
    {
        while (_currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, _currentTime / duration);
            _imageForFade.color = new Color(_imageForFade.color.r, _imageForFade.color.g, _imageForFade.color.b, alpha);
            _currentTime += Time.deltaTime;
            yield return null;
        }
        _currentTime = 0;
        _afterFade.Invoke();
    }
}