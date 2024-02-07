using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLight : MonoBehaviour
{
    [SerializeField] private Light[] SpecialLights;
    private Light[] _lights;

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            SwitchLight();
        }
    }

    public void SwitchLight()
    {
        _lights = FindObjectsOfType<Light>();
        foreach (var light in _lights)
        {
            light.enabled = false;
        }
        foreach (var lightS in SpecialLights)
        {
            lightS.enabled = true;
        }
    }
}
