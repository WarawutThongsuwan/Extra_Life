using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    public int scoreValue = 10; // คะแนนที่ได้เมื่อเก็บไอเทม

    [SerializeField] AudioClip collectSound; // เสียงเมื่อเก็บเหรียญ
    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // เพิ่มคะแนน
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }

            // เล่นเสียงเก็บไอเทม
            PlayCollectSound();

            // ลบไอเทมออกจากฉากหลังเสียงเล่นเสร็จ
            Destroy(gameObject, collectSound.length);
        }
    }

    private void PlayCollectSound()
    {
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }
}
