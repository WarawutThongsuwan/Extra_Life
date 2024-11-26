using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI soulsText; // อ้างอิง Text UI ที่ใช้แสดงคะแนน Souls
    public TextMeshProUGUI scoreText; // อ้างอิง Text UI ที่ใช้แสดงคะแนน Score

    private void Update()
    {
        if (ScoreManager.Instance == null)
        {
            Debug.Log("ScoreManager.Instance is null!");
            return;
        }

        if (soulsText == null)
        {
            Debug.Log("soulsText is not assigned!");
            return;
        }

        if (scoreText == null)
        {
            Debug.Log("scoreText is not assigned!");
            return;
        }

        // อัปเดตข้อความ
        soulsText.text = "Souls: 7/" + ScoreManager.Instance.soulPoints.ToString();
        scoreText.text = "Score: " + ScoreManager.Instance.currentScore.ToString();
    }

}
