using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ใช้สำหรับจัดการซีน

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int currentScore = 0;
    public int soulPoints = 0;  // เพิ่มคะแนนวิญญาณ

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "DeadMap" || scene.name == "Menu" || scene.name == "StageLoss" || scene.name == "StageWinbut" || scene.name == "StageWin")
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public void AddSoulPoints(int amount) // ฟังก์ชันเพิ่มคะแนนวิญญาณ
    {
        soulPoints += amount;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
