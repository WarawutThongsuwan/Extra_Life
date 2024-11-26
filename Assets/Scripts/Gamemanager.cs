using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // อ้างอิง AudioSource
    [SerializeField] private string targetSceneName = "DeadMap"; // ชื่อซีนที่จะโหลด

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCollisionController player = collision.gameObject.GetComponent<PlayerCollisionController>();
            if (player != null && !player.isImmortal)
            {
                StartCoroutine(PlaySoundAndLoadScene());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCollisionController player = other.GetComponent<PlayerCollisionController>();
            if (player != null && !player.isImmortal)
            {
                StartCoroutine(PlaySoundAndLoadScene());
            }
        }
    }

    private IEnumerator PlaySoundAndLoadScene()
    {
        // หยุดเกม
        Time.timeScale = 0f;

        if (audioSource != null && audioSource.clip != null)
        {
            // เล่นเสียง
            audioSource.Play();

            // รอจนกว่าเสียงจะเล่นเสร็จ
            yield return new WaitForSecondsRealtime(audioSource.clip.length);
        }

        // คืนเวลาเกม
        Time.timeScale = 1f;

        // โหลดซีนใหม่
        SceneManager.LoadScene(targetSceneName);
    }
}
