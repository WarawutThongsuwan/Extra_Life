using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;

    // เมื่อผู้เล่นเข้าสู่โซน จะย้ายไปยังฉากอื่น
    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger Entered");
        
        // ตรวจสอบว่าผู้เล่นเข้าสู่โซนหรือไม่
        if (other.CompareTag("Player")) {
            // ตรวจสอบว่า sceneBuildIndex มีค่าอยู่ในช่วงที่ถูกต้อง
            if (sceneBuildIndex >= 0 && sceneBuildIndex < SceneManager.sceneCountInBuildSettings) {
                print("Switching Scene to " + sceneBuildIndex);
                SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            } else {
                Debug.LogWarning("Invalid sceneBuildIndex: " + sceneBuildIndex);
            }
        }
    }
}
