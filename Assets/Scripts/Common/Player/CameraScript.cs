using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    private GameObject player;
    [Tooltip("Time that the camera will follow the player")]
    [SerializeField] float timeOffSet;
    [Header("Camera boundary settings")]
    [SerializeField] float leftCameraLimit;
    [SerializeField] float rightCameraLimit;
    [SerializeField] float lowerCameraLimit;
    [SerializeField] float upperCameraLimit;
    [SerializeField] private float _zStartPos;
    [SerializeField] private float _zEndPos;
    [Tooltip("How far the camera will be from the player on the Z Axis")]
    [SerializeField] private float _zPosOffsetPlayer;
    private Vector3 posOffSet;
    private Vector3 velocity;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = player.transform.position;
            endPos.x += posOffSet.x;
            endPos.y += posOffSet.y;
            endPos.z += posOffSet.z - _zPosOffsetPlayer;
            transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffSet);

            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, leftCameraLimit, rightCameraLimit),
                Mathf.Clamp(transform.position.y, lowerCameraLimit, upperCameraLimit),
                Mathf.Clamp(transform.position.z, _zStartPos, _zEndPos)
            );   
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftCameraLimit, upperCameraLimit), new Vector2(rightCameraLimit, upperCameraLimit));
        Gizmos.DrawLine(new Vector2(rightCameraLimit, upperCameraLimit), new Vector2(rightCameraLimit, lowerCameraLimit));
        Gizmos.DrawLine(new Vector2(rightCameraLimit, lowerCameraLimit), new Vector2(leftCameraLimit, lowerCameraLimit));
        Gizmos.DrawLine(new Vector2(leftCameraLimit, lowerCameraLimit), new Vector2(leftCameraLimit, upperCameraLimit));
        Gizmos.DrawLine(new Vector3(leftCameraLimit, lowerCameraLimit, _zStartPos), new Vector3(leftCameraLimit, lowerCameraLimit, _zEndPos));
    }
}
