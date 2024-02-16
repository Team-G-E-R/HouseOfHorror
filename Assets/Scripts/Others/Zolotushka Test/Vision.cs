using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Vision : MonoBehaviour
{
    [SerializeField] private Light[] _specialLights;
    [Tooltip("Recommend 1")]
    [SerializeField] private float _chromaticIntensity;
    [Tooltip("Recommend 60")]
    [SerializeField] private float _lensIntensity;
    [SerializeField] private GameObject[] _hiddenObjs;
    private Light[] _lights;
    private PostProcessVolume _postProcess;
    private float _standartChromatic;
    private float _standartLens;
    private bool state;
    private bool _hidden;

    private void OnEnable()
    {
        _postProcess = GameObject.
            FindGameObjectWithTag("PostProcessing").GetComponent<PostProcessVolume>();
        _standartChromatic = _postProcess.profile.GetSetting<ChromaticAberration>().intensity.value;
        _standartLens = _postProcess.profile.GetSetting<LensDistortion>().intensity.value;

        state = false;
        _hidden = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            state = !state;
            _hidden = !_hidden;
            SwitchLight();
        }
    }

    private void SwitchLight()
    {
        _lights = FindObjectsOfType<Light>();
        foreach (var light in _lights)
        {
            light.enabled = !state;
        }
        foreach (var lightS in _specialLights)
        {
            lightS.enabled = state;
        }
        
        float CA = _postProcess.profile.GetSetting<ChromaticAberration>().intensity.value;
        float LD = _postProcess.profile.GetSetting<LensDistortion>().intensity.value;
        StartCoroutine(ChangeCA(CA));
        StartCoroutine(ChangeLD(LD));

        if (_hiddenObjs.Length != 0)
        {
            foreach (var obj in _hiddenObjs)
            {
                obj.SetActive(_hidden);
            }   
        }
    }

    private IEnumerator ChangeCA (float CAValue)
    {
        float lerpCA = 0;
        while (CAValue <= _chromaticIntensity)
        {
            if (state == true)
            {
                lerpCA = Mathf.Lerp(CAValue, _chromaticIntensity, 0.1f);   
            }
            else
            {
                lerpCA = Mathf.Lerp(CAValue, _standartChromatic, 0.1f); 
            }
            _postProcess.profile.GetSetting<ChromaticAberration>().intensity.value = lerpCA;
            CAValue = lerpCA;
            yield return null;
        }
    }
    
    private IEnumerator ChangeLD (float LDValue)
    {
        float lerpLD = 0;
        while (LDValue <= _lensIntensity)
        {
            if (state == true)
            {
                lerpLD = Mathf.Lerp(LDValue, _lensIntensity, 0.1f); 
            }
            else
            {
                lerpLD = Mathf.Lerp(LDValue, _standartLens, 0.1f);
            }
            _postProcess.profile.GetSetting<LensDistortion>().intensity.value = lerpLD;
            LDValue = lerpLD;
            yield return null;
        }
    }
}
