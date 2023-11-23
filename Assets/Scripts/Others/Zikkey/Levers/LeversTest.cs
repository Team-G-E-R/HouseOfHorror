using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LeversTest : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        HideRenderer();
    }

    public void HideRenderer()
    {
        _renderer.enabled = false;
    }

    public void ShowRenderer()
    {
        _renderer.enabled = true;
    }
}
