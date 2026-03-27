using UnityEngine;

public class Patrol : MonoBehaviour
{
    [Header("จุดลาดตระเวน")]
    public Transform pointA; // จุดที่ 1 (Empty Object)
    public Transform pointB; // จุดที่ 2 (Empty Object)

    [Header("การตั้งค่าความเร็ว")]
    public float speed = 2f; // ความเร็วในการเดิน

    private Transform currentTarget; // จุดเป้าหมายที่กำลังเดินไปหา

    void Start()
    {
        // เริ่มต้นให้เดินไปหาจุด A ก่อน
        currentTarget = pointA;
    }

    void Update()
    {
        // ป้องกัน Error ในกรณีที่ลืมใส่จุด A หรือ B ใน Inspector
        if (pointA == null || pointB == null) return;

        // 1. สั่งให้ศัตรูเดินไปยังเป้าหมายปัจจุบัน
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // 2. เช็คระยะห่างว่าเดินมาถึงจุดหมายหรือยัง (เช็คที่ระยะ 0.2f เพื่อป้องกันบั๊กเดินไม่ถึงจุดเป๊ะๆ)
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.2f)
        {
            // 3. สลับเป้าหมาย
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
            }
            else
            {
                currentTarget = pointA;
            }

            // 4. หันหน้าตัวละครกลับ
            Flip();
        }
    }

    // ฟังก์ชันสำหรับกลับด้านภาพตัวละคร (ซ้าย-ขวา)
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // กลับค่าแกน X จากบวกเป็นลบ / ลบเป็นบวก
        transform.localScale = localScale;
    }

    // วาดเส้นเชื่อมระหว่างจุด A และ B ในหน้า Scene เพื่อให้เห็นเส้นทางเดินชัดเจน
    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawWireSphere(pointA.position, 0.3f);
            Gizmos.DrawWireSphere(pointB.position, 0.3f);
        }
    }
}