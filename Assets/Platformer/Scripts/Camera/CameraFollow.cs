using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Lookahead")]
    [SerializeField] private float lookaheadTime = 0.15f;
    [SerializeField] private float lookaheadSmoothing = 10f;

    [Header("Damping")]
    [SerializeField] private float dampingX = 1f;
    [SerializeField] private float dampingY = 1f;

    [Header("Dead Zone (in viewport %)")]
    [SerializeField] private float deadZoneWidth = 0.1f;
    [SerializeField] private float deadZoneHeight = 0.6f;

    [Header("Soft Zone (in viewport %)")]
    [SerializeField] private float softZoneWidth = 0.7f;
    [SerializeField] private float softZoneHeight = 0.7f;

    [Header("Camera Settings")]
    [SerializeField] private float cameraDistance = 10f;

    private Vector3 currentVelocity;
    private Vector3 lastTargetPosition;
    private Vector3 lookaheadOffset;

    private Camera cam;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraFollow: No target assigned!");
            enabled = false;
            return;
        }

        cam = GetComponent<Camera>();
        lastTargetPosition = target.position;
    }

    void LateUpdate()
    {
        Vector3 delta = target.position - lastTargetPosition;
        Vector3 lookahead = delta.normalized * lookaheadTime;
        lookaheadOffset = Vector3.Lerp(lookaheadOffset, lookahead, Time.deltaTime * lookaheadSmoothing);
        lastTargetPosition = target.position;

        Vector3 focusPosition = target.position + lookaheadOffset;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(
            Mathf.SmoothDamp(currentPosition.x, focusPosition.x, ref currentVelocity.x, dampingX),
            Mathf.SmoothDamp(currentPosition.y, focusPosition.y, ref currentVelocity.y, dampingY),
            -cameraDistance
        );

        // Convert target position to viewport space
        Vector3 viewportPoint = cam.WorldToViewportPoint(target.position);

        float deadZoneMinX = 0.5f - deadZoneWidth / 2f;
        float deadZoneMaxX = 0.5f + deadZoneWidth / 2f;
        float deadZoneMinY = 0.5f - deadZoneHeight / 2f;
        float deadZoneMaxY = 0.5f + deadZoneHeight / 2f;

        // Only move camera if target is outside dead zone
        if (viewportPoint.x < deadZoneMinX || viewportPoint.x > deadZoneMaxX)
        {
            currentPosition.x = targetPosition.x;
        }

        if (viewportPoint.y < deadZoneMinY || viewportPoint.y > deadZoneMaxY)
        {
            currentPosition.y = targetPosition.y;
        }

        currentPosition.z = -cameraDistance;
        transform.position = currentPosition;
    }

    void OnDrawGizmos()
    {
        if (!cam) cam = GetComponent<Camera>();
        if (!cam || !Application.isPlaying) return;

        Gizmos.color = Color.red;
        DrawZone(deadZoneWidth, deadZoneHeight);

        Gizmos.color = Color.yellow;
        DrawZone(softZoneWidth, softZoneHeight);
    }

    void DrawZone(float width, float height)
    {
        Vector3 center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraDistance));
        float camHeight = 2f * cameraDistance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float camWidth = camHeight * cam.aspect;

        Vector3 size = new Vector3(camWidth * width, camHeight * height, 0f);
        Gizmos.DrawWireCube(center, size);
    }
}
