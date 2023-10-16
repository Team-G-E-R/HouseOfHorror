using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] public float duration;
    [SerializeField] Image imageForFade;
    private float currentTime;

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