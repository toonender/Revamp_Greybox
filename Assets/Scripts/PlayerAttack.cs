using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("การตั้งค่าโจมตี")]
    public Transform attackPoint; // จุดศูนย์กลางการโจมตี 1
    public Transform attackPoint2; // จุดศูนย์กลางการโจมตี 2
    public float attackRange = 1.0f; // รัศมีการโจมตี
    public LayerMask enemyLayer; // กำหนดว่า Layer ไหนคือศัตรู

    void Update()
    {
        // เมื่อกด F จะทำการโจมตี
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    void Attack()
    {
        // 1. สร้างวงกลมล่องหนเพื่อเช็คว่ามีศัตรูอยู่ในระยะของทั้ง 2 จุดหรือไม่
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange, enemyLayer);

        // 2. สั่งทำลายศัตรูที่อยู่ในวงกลมจุดที่ 1
        foreach (Collider2D enemy in hitEnemies)
        {
            Destroy(enemy.gameObject);
        }

        // 3. สั่งทำลายศัตรูที่อยู่ในวงกลมจุดที่ 2
        foreach (Collider2D enemy in hitEnemies2)
        {
            // เช็คกันเหนียว เผื่อมีศัตรูตัวใหญ่อยู่ตรงกลางและโดนวงกลมทั้ง 2 วงทับซ้อนกัน (โดนจุดที่ 1 ทำลายไปแล้ว)
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    // ฟังก์ชันนี้จะวาดเส้นวงกลมสีแดงในหน้า Scene ให้คุณเห็นระยะโจมตี
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // แยกเช็คแต่ละจุด เพื่อป้องกัน Error ในกรณีที่ลืมตั้งค่าใน Inspector
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        if (attackPoint2 != null)
        {
            Gizmos.DrawWireSphere(attackPoint2.position, attackRange);
        }
    }
}