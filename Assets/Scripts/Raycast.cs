using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayCast : MonoBehaviour
{
    [Header("ตั้งค่า Raycast")]
    public float rayDistance = 30f;
    public LayerMask targetLayer; // **อย่าลืมตั้งให้ครอบคลุมเลเยอร์ของ Player ด้วยนะครับ**

    [Header("ตั้งค่าการสลับทิศทาง")]
    public float switchInterval = 0.5f; // เวลาในการสลับทิศทาง (วินาที)

    private LineRenderer lineRenderer;
    private float timer = 0f; // ตัวจับเวลา
    private bool isShootingLeft = true; // สถานะปัจจุบันว่ากำลังยิงไปทางซ้ายอยู่หรือไม่

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void Update()
    {
        // 1. ระบบจับเวลาเพื่อสลับทิศทาง
        timer += Time.deltaTime; // บวกเวลาที่ผ่านไปในแต่ละเฟรม
        if (timer >= switchInterval)
        {
            isShootingLeft = !isShootingLeft; // สลับสถานะ (ถ้าเป็นซ้ายให้เปลี่ยนเป็นขวา, ขวาเป็นซ้าย)
            timer = 0f; // รีเซ็ตตัวจับเวลาใหม่
        }

        // 2. กำหนดทิศทางตามสถานะปัจจุบัน
        Vector2 direction = isShootingLeft ? Vector2.left : Vector2.right;
        Vector2 startPosition = transform.position;

        // ยิง Raycast 
        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, rayDistance, targetLayer);

        lineRenderer.SetPosition(0, startPosition);

        if (hit.collider != null)
        {
            // วาดเส้นสีแดงไปหยุดตรงจุดที่ชน
            lineRenderer.SetPosition(1, hit.point);
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;

            // ตรวจสอบว่าสิ่งที่ชนคือ Player หรือไม่
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("สังหาร Player สำเร็จ!");
                Destroy(hit.collider.gameObject); // ทำลาย Player ทิ้ง
            }
        }
        else
        {
            // ถ้าไม่ชนอะไรเลย วาดเส้นสีเขียวยาวเต็มระยะ
            lineRenderer.SetPosition(1, startPosition + (direction * rayDistance));
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
    }
}