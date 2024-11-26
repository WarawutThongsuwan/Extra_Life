using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string ignoreTag = "Enemy"; // Tag ที่ต้องการเพิกเฉย
    public float ignoreDuration = 5f; // ระยะเวลาเพิกเฉยการชน
    public float immortalityDuration = 5f; // ระยะเวลาสถานะอัมตะ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่า Player เก็บไอเทม
        if (collision.CompareTag("Player"))
        {
            // เรียกฟังก์ชันเพิกเฉยการชนใน Player
            PlayerCollisionController player = collision.GetComponent<PlayerCollisionController>();
            if (player != null)
            {
                player.IgnoreCollisionWithTag(ignoreTag, ignoreDuration);
                player.ActivateImmortality(immortalityDuration); // เปิดสถานะอัมตะ
            }

            // ลบไอเทมออกจากฉาก
            Destroy(gameObject);
        }
    } 
}
