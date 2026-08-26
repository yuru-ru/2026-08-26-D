using UnityEngine;

/// <summary>
/// プレイヤーにカメラを追従させる。ステージ端でのクランプ機能付き。
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, -10f);

    [Header("移動範囲の制限（ステージの端で使用）")]
    [SerializeField] private bool useClamp = false;
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime));

        if (useClamp)
        {
            smoothed.x = Mathf.Clamp(smoothed.x, minPosition.x, maxPosition.x);
            smoothed.y = Mathf.Clamp(smoothed.y, minPosition.y, maxPosition.y);
        }

        transform.position = smoothed;
    }
}
