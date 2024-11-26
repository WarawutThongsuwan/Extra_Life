using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // ตัว SpriteRenderer ของผู้เล่น
    public Gradient rainbowGradient; // Gradient สีสายรุ้ง
    public float colorChangeSpeed = 2f; // ความเร็วในการเปลี่ยนสี

    private bool isRainbowActive = false; // สถานะเปิด/ปิดสีสายรุ้ง

    [SerializeField] AudioClip collectSound; // เสียงเมื่อเก็บไอเท็ม
    AudioSource audioSource;

    private void Start()
    {
        // อ้างอิง SpriteRenderer อัตโนมัติถ้าไม่ได้ตั้งค่าใน Inspector
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // เพิ่ม AudioSource ให้กับ GameObject
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void ActivateRainbow(float duration)
    {
        if (!isRainbowActive)
        {
            PlayCollectSound(); // เล่นเสียง
            StartCoroutine(RainbowEffect(duration));
        }
    }

    private void PlayCollectSound()
    {
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }

    private IEnumerator RainbowEffect(float duration)
    {
        isRainbowActive = true;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // เปลี่ยนสีตาม Gradient
            float t = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);
            spriteRenderer.color = rainbowGradient.Evaluate(t);

            yield return null;
        }

        // กลับสีปกติหลังหมดเวลา
        spriteRenderer.color = Color.white;
        isRainbowActive = false;
    }
}
