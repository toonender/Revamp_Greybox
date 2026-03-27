using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("ตั้งค่าการเกิดใหม่ (Respawn)")]
    public GameObject playerPrefab; // ลาก Prefab ของ Player มาใส่ช่องนี้
    public Transform spawnPoint;    // ลากจุดเกิด (Empty Object) มาใส่ช่องนี้

    private GameObject currentPlayer; // ตัวแปรเก็บ Player ตัวปัจจุบันที่อยู่ในฉาก

    void Start()
    {
        // เริ่มเกมมา ให้สร้าง Player ทันที
        SpawnPlayer();
    }

    void Update()
    {
        // เช็คการกดปุ่ม R (กดได้ตลอดเวลา ทั้งตอนที่มีชีวิตอยู่ และตอนตายไปแล้ว)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            // สร้าง Player ขึ้นมาใหม่ที่ตำแหน่งของจุด Spawn
            currentPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    void Respawn()
    {
        // 1. ถ้ายังมี Player ตัวเก่าอยู่ในฉาก (กด R ตอนที่ยังไม่ตาย) ให้ลบตัวเก่าทิ้งก่อน
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        // 2. เรียกฟังก์ชันสร้าง Player ตัวใหม่
        SpawnPlayer();

        Debug.Log("เกิดใหม่เรียบร้อย!");
    }
}