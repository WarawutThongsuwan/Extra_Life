using System.Collections;
using UnityEngine;

public class NpcGhost : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog; // ข้อความที่ NPC จะพูด
    [SerializeField] int soulPoints = 10; // จำนวนคะแนนวิญญาณที่จะเพิ่มเมื่อคุยกับ NPC นี้

    private bool hasInteracted = false; // ตรวจสอบว่าคุยแล้วหรือยัง

    public void Interact()
    {
        if (!hasInteracted) // หากยังไม่เคยคุย
        {
            hasInteracted = true; // ตั้งค่าว่าคุยแล้ว
            StartCoroutine(InteractCoroutine());
        }
    }

    private IEnumerator InteractCoroutine()
    {
        // แสดง Dialog
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

        // เพิ่มคะแนนวิญญาณ
        ScoreManager.Instance.AddSoulPoints(soulPoints);

        // เอฟเฟคการหายไป (ค่อยๆ หายไป)
        float fadeDuration = 5f;
        float timeElapsed = 0f;
        Renderer npcRenderer = GetComponent<Renderer>(); // ใช้ Renderer ในการทำให้ NPC ค่อยๆ หายไป

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = 1 - (timeElapsed / fadeDuration);
            Color color = npcRenderer.material.color;
            color.a = alpha;
            npcRenderer.material.color = color; // ค่อยๆ ลดความโปร่งใส
            yield return null;
        }

        // หลังจากหายไปแล้ว ทำการทำลาย NPC
        Destroy(gameObject);
    }
}