using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    [SerializeField] private float startOrthographicSize = 5f;

    [Header("Zoom")]
    [SerializeField] private float zoomDelaySeconds = 10f;
    [SerializeField] private float targetSize = 4f;
    [SerializeField] private float zoomSpeed = 1f;

    private Camera cam;
    private Vector3 followVelocity = Vector3.zero;
    private bool zoomStarted;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;
    }

    private void Start()
    {
        if (cam != null) cam.orthographicSize = startOrthographicSize;
    }

    private void LateUpdate()
    {
        if (cam == null) return;

        if (player != null)
        {
            float halfH = cam.orthographicSize;
            float halfW = halfH * cam.aspect;

            float minX = minBounds.x + halfW;
            float maxX = maxBounds.x - halfW;
            float minY = minBounds.y + halfH;
            float maxY = maxBounds.y - halfH;

            Vector3 target = new Vector3(player.position.x, player.position.y, transform.position.z);

            if (minX > maxX) target.x = (minBounds.x + maxBounds.x) * 0.5f;
            else target.x = Mathf.Clamp(target.x, minX, maxX);

            if (minY > maxY) target.y = (minBounds.y + maxBounds.y) * 0.5f;
            else target.y = Mathf.Clamp(target.y, minY, maxY);

            transform.position = Vector3.SmoothDamp(transform.position, target, ref followVelocity, smoothTime);
        }

        if (!zoomStarted && Time.timeSinceLevelLoad >= zoomDelaySeconds)
        {
            zoomStarted = true;
        }

        if (zoomStarted)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
        }
    }
}
