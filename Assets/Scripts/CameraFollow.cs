using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // ����Ф�
    public Vector3 offset;     // ������ҧ���ͧ
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
