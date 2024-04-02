using UnityEngine;

public class BootstrapSystem : Finder
{
    private float _soundVolume;

    private void Start()
    {
        CursorOn();
        FindObjs();
    }
}
