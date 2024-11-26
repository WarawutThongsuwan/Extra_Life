using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        // คืนค่าเวลาเกมก่อนเปลี่ยนฉาก
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void MainGame()
    {
        // คืนค่าเวลาเกมก่อนเปลี่ยนฉาก
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        // ออกเกม
        Application.Quit();
    }
}
