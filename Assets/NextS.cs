using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // กำหนดชื่อ Scene ที่ต้องการข้ามไป

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called"); // ตรวจสอบว่าเข้ามาในฟังก์ชันนี้แล้วหรือยัง

        if (other.gameObject.CompareTag("Player")) // ตรวจสอบว่าเป็น Object ที่มี Tag Player หรือไม่
        {
            Debug.Log("Player collided with the object"); // ตรวจสอบว่าการชนกับ Player ทำงานถูกต้อง

            if (!string.IsNullOrEmpty(targetSceneName) && Application.CanStreamedLevelBeLoaded(targetSceneName))
            {
                Debug.Log("Loading scene: " + targetSceneName); // ตรวจสอบว่า scene จะถูกโหลด
                SceneManager.LoadScene(targetSceneName); // ข้ามไปยัง Scene ที่กำหนดไว้
            }
            else
            {
                Debug.LogError("Scene name is invalid or not added to Build Settings");
            }
        }
    }
}
