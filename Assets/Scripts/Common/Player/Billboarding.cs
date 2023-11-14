using UnityEngine;

public class Billboarding : MonoBehaviour
{
    #region Camera
    Vector3 cameraDir;
    #endregion

    #region face_towards_camera
    void Update()
    {
        cameraDir = Camera.main.transform.forward;
        cameraDir.y = 0;
        transform.rotation = Quaternion.LookRotation(cameraDir);
    }
    #endregion
}