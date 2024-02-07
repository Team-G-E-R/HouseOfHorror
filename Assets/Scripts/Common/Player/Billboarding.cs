using UnityEngine;

public class Billboarding : MonoBehaviour
{
    #region Camera
    Vector3 cameraDir;
    #endregion

    #region face_towards_camera
    void Update()
    {
        if (CameraSwitch.CurrentCamera == null) 
            return;

        cameraDir = CameraSwitch.CurrentCamera.transform.forward;
        cameraDir.y = 0;
        transform.rotation = Quaternion.LookRotation(cameraDir);
    }
    #endregion
}