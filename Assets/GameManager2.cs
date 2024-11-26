using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    public class SceneLoader : MonoBehaviour
    {
        public TextMeshProUGUI newScoreText; // Text UI ใน Scene ใหม่

        private void Start()
        {
            if (GameManager2.Instance != null)
            {
            GameManager2.Instance.SetScoreUI(newScoreText); // อัปเดต UI เมื่อเข้า Scene นี้
            }
        }
    }
    public static GameManager2 Instance; // ใช้ Singleton เพื่อให้ GameManager มีตัวเดียว

    public int score = 0; // ตัวแปรเก็บคะแนน
    public TextMeshProUGUI scoreText; // Text UI สำหรับแสดงคะแนน

    private void Awake()
    {
        // ทำให้ GameManager คงอยู่ในทุก Scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ไม่ทำลาย GameObject นี้เมื่อเปลี่ยน Scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI(); // อัปเดตคะแนนตอนเริ่มเกม
    }

    public void AddScore(int amount)
    {
        score += amount; // เพิ่มคะแนน
        UpdateScoreUI(); // อัปเดต UI
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // ฟังก์ชันสำหรับตั้งค่า UI ใหม่เมื่อเปลี่ยน Scene
    public void SetScoreUI(TextMeshProUGUI newScoreText)
    {
        scoreText = newScoreText;
        UpdateScoreUI();
    }

    
}
