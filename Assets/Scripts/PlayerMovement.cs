using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f; // เปลี่ยนชื่อตัวแปรให้ตรงความหมาย (อาจจะต้องปรับค่าใน Inspector ให้เยอะขึ้นนิดหน่อย)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = 0f;

        // A / D ซ้ายขวา
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        // เคลื่อนที่แนวนอน
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        // W กระโดด (กด 1 ครั้ง กระโดด 1 ที และกดกลางอากาศได้เรื่อยๆ)
        if (Input.GetKeyDown(KeyCode.W))
        {
            // เซ็ตความเร็วแกน Y ใหม่โดยตรง จะทำให้สลัดแรงโน้มถ่วงที่กำลังตกลงมาทิ้งไป และกระโดดเด้งขึ้นทันที
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}