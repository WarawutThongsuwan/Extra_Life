using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulsUI : MonoBehaviour
{
    public TextMeshProUGUI soulsText; // อ้างอิง Text UI ที่ใช้แสดงคะแนน

    private void Update()
    {
        if (ScoreManager.Instance != null)
        {
            soulsText.text = "Souls: 7/" + ScoreManager.Instance.soulPoints.ToString();
        }
    }
}
