using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // ฟังก์ชันนี้จะทำงานเมื่อตัวละครเดินไปชนกับวัตถุอื่น (แบบเป็นก้อนแข็งๆ ชนกัน)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าสิ่งที่ชนด้วย มี Tag ชื่อ "Enemy" หรือไม่
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player ชนศัตรู! ตัวละครถูกทำลาย");

            // สั่งทำลายตัวเอง (Player)
            Destroy(gameObject);
        }
    }

    // ฟังก์ชันนี้เผื่อไว้ในกรณีที่ศัตรูของคุณติ๊กช่อง "Is Trigger" (ทะลุได้)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player ชนศัตรู (แบบ Trigger)! ตัวละครถูกทำลาย");

            // สั่งทำลายตัวเอง (Player)
            Destroy(gameObject);
        }
    }
}