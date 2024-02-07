using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AllLight : MonoBehaviour
{
    [SerializeField] private Light[] _specialLights;
    [Tooltip("Recomended 1")]
    [SerializeField] private float _chromaticIntensity;
    [Tooltip("Recomended 60")]
    [SerializeField] private float _lensIntensity;
    [SerializeField] private bool _needToShowHidden;
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
            SwitchLight(state);
        }
    }

    private void SwitchLight(bool isState)
    {
        _lights = FindObjectsOfType<Light>();
        foreach (var light in _lights)
        {
            light.enabled = !isState;
        }
        foreach (var lightS in _specialLights)
        {
            lightS.enabled = isState;
        }
        if (isState == true)
        {
            _postProcess.profile.GetSetting<ChromaticAberration>().intensity.value = _chromaticIntensity;
            _postProcess.profile.GetSetting<LensDistortion>().intensity.value = _lensIntensity;
        }
        else
        {
            _postProcess.profile.GetSetting<ChromaticAberration>().intensity.value = _standartChromatic;
            _postProcess.profile.GetSetting<LensDistortion>().intensity.value = _standartLens;
        }
        if (_hiddenObjs.Length != 0)
        {
            foreach (var obj in _hiddenObjs)
            {
                obj.SetActive(_hidden);
            }   
        }
    }
}
