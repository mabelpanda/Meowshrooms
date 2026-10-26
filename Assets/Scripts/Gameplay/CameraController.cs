using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private BoxCollider2D _cameraBounds;

    [SerializeField] private float _horizontalThreshold = 2f;
    [SerializeField] private float _verticalThreshold = 1.5f;

    [SerializeField] private float _cameraSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 targetPosition = transform.position;
        Vector3 offset = _target.position - transform.position;

        if (Mathf.Abs(offset.x) > _horizontalThreshold)
        {
            targetPosition.x = _target.position.x - Mathf.Sign(offset.x) * _horizontalThreshold;
        }

        if (Mathf.Abs(offset.y) > _verticalThreshold)
        {
            targetPosition.y = _target.position.y - Mathf.Sign(offset.y) * _verticalThreshold;
        }

        targetPosition = ClampCameraPosition(targetPosition);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            _cameraSpeed * Time.deltaTime
        );
    }

    private Vector3 ClampCameraPosition(Vector3 position)
    {
        Bounds bounds = _cameraBounds.bounds;

        Camera camera = GetComponent<Camera>();

        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        float minX = bounds.min.x + cameraWidth;
        float maxX = bounds.max.x - cameraWidth;

        float minY = bounds.min.y + cameraHeight;
        float maxY = bounds.max.y - cameraHeight;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        return position;
    }
}
