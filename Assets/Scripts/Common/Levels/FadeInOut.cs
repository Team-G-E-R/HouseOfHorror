using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [Header("Fade settings")]
    [SerializeField] public float duration;
    [SerializeField] Image imageForFade;
    [Header("Fade on start settings")]
    [SerializeField] private bool _needToFadeOnStart;
    [SerializeField] private FadeChoose fade;
    public enum FadeChoose{
        NeedFadeIn,
        NeedFadeOut
    }
    private float currentTime;

    private void Start()
    {
        if (_needToFadeOnStart && fade == FadeChoose.NeedFadeIn) FadeIn();
        else if (_needToFadeOnStart && fade == FadeChoose.NeedFadeOut) FadeOut();
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
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            imageForFade.color = new Color(imageForFade.color.r, imageForFade.color.g, imageForFade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        currentTime = 0;
        yield break;
    }

    private IEnumerator FadeOutCrt()
    {
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            imageForFade.color = new Color(imageForFade.color.r, imageForFade.color.g, imageForFade.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        currentTime = 0;
        yield break;
    }
}