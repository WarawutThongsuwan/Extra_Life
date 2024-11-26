using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Collider2D playerCollider;
    public bool isImmortal = false; // สถานะอัมตะ

    private void Start()
    {
        // อ้างอิง Collider ของตัว Player
        playerCollider = GetComponent<Collider2D>();
    }

    public void IgnoreCollisionWithTag(string tag, float duration)
    {
        // ค้นหา GameObject ทั้งหมดที่มี Tag นั้น
        GameObject[] objectsToIgnore = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsToIgnore)
        {
            Collider2D otherCollider = obj.GetComponent<Collider2D>();
            if (otherCollider != null)
            {
                // เพิกเฉยการชน
                Physics2D.IgnoreCollision(playerCollider, otherCollider, true);
                Debug.Log($"Ignoring collision with {obj.name} for {duration} seconds.");
            }
        }

        // เริ่ม Coroutine เพื่อคืนค่าการชนหลังเวลาที่กำหนด
        StartCoroutine(RestoreCollision(objectsToIgnore, duration));
    }

    private IEnumerator RestoreCollision(GameObject[] objectsToIgnore, float delay)
    {
        // รอเวลาที่กำหนด
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in objectsToIgnore)
        {
            if (obj != null) // ตรวจสอบว่า GameObject ยังอยู่ในฉาก
            {
                Collider2D otherCollider = obj.GetComponent<Collider2D>();
                if (otherCollider != null)
                {
                    // คืนค่าการชน
                    Physics2D.IgnoreCollision(playerCollider, otherCollider, false);
                    Debug.Log($"Restored collision with {obj.name}.");
                }
            }
        }
    }
    public PlayerColorChanger colorChanger;

    public void ActivateImmortality(float duration)
    {
        StartCoroutine(ImmortalityTimer(duration));
    }

    

    private IEnumerator ImmortalityTimer(float duration)
    {
        isImmortal = true; // เปิดสถานะอัมตะ
    Debug.Log("Player is now immortal.");

    // เปิดเอฟเฟกต์สีสายรุ้ง
    if (colorChanger != null)
    {
        colorChanger.ActivateRainbow(duration);
    }

    // รอเวลาที่กำหนด
    yield return new WaitForSeconds(duration);

    isImmortal = false; // ปิดสถานะอัมตะ
    Debug.Log("Player's immortality ended.");
    }
}
