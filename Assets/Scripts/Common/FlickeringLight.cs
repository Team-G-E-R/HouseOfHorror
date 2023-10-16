using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private AudioSource electro_sound;
    [SerializeField] private Light _light;
    [SerializeField] private float start_intensity;
    [SerializeField] private float min_intensity = 0.3f;
    [SerializeField] private float max_intensity = 1.5f;
    [SerializeField] private float noise_speed = 0.15f;
    [SerializeField] private bool flicker_ON;
    [SerializeField] private bool random_timer;
    [SerializeField] private float random_timer_value_MIN = 5f;
    [SerializeField] private float random_timer_value_MAX = 20f;
    [SerializeField] private float start_timer_value = 0.1f;
    private float random_timer_value;

    IEnumerator Start()
    {
        _light = GetComponent<Light>();
        start_intensity = _light.intensity;
        yield return new WaitForSeconds(start_timer_value);
        if(flicker_ON) electro_sound.Play();

        while(random_timer)
        {
            random_timer_value = Random.Range(random_timer_value_MIN, random_timer_value_MAX);
            yield return new WaitForSeconds(random_timer_value);
            if(flicker_ON)
            {
                _light.intensity = start_intensity;
                flicker_ON = false;
                if (electro_sound != null)
                {
                    electro_sound.Pause();   
                }
            }
            else
            {
                flicker_ON = true;
                if (electro_sound != null)
                {
                    electro_sound.Play();   
                }
            }
        }
    }
    void Update()
    {
        if (flicker_ON) _light.intensity = Mathf.Lerp(min_intensity, max_intensity, Mathf.PerlinNoise(10, Time.time / noise_speed));   
    }
}