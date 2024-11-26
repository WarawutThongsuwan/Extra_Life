using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
