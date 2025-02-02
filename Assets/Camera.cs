using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public Vector2 minBounds; 
    public Vector2 maxBounds; 
    public float orthographicSize = 5f; 
    private Camera cam;

    private float elapsedTime = 0f; 
    public float zoomSpeed = 1f; 
    public float targetSize = 4f; 
    private bool zoomStarted = false; 

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = orthographicSize; 
    }

    void LateUpdate()
    {
        if (player != null)
        {
            float cameraHalfWidth = cam.orthographicSize * cam.aspect; 
            float cameraHalfHeight = cam.orthographicSize; 

            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
            float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

            Vector3 smoothPosition = new Vector3(clampedX, clampedY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, smoothPosition, smoothSpeed * Time.deltaTime);
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 10f && !zoomStarted)
        {
            zoomStarted = true;
        }

        if (zoomStarted)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
        }
    }
}
