using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ใช้สำหรับ UI

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;
    public LayerMask nextSceneLayer;
    public LayerMask stage2Leyer;
    public LayerMask stage3Leyer;
    public LayerMask stage4Leyer;
    public LayerMask stage5Leyer;
    public LayerMask stage6Leyer;
    public LayerMask stage7Leyer;  // เพิ่ม Layer สำหรับ Stage7
    public LayerMask stageWinLeyer;

    public LayerMask stageWinbut;
    public LayerMask stageLossLeyer;
    public LayerMask fateDoorLayer;  // เพิ่ม Layer สำหรับ FateDoor

    [SerializeField] private AudioSource audioSource;

        private bool isPaused = false; // สถานะหยุดเกม
        public GameObject pauseMenu; // อ้างอิง UI ของเมนูหยุดเกม

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log("Animator set up in Awake");
    }

    private void Update()
    {
        // ตรวจสอบการกดปุ่ม P เพื่อหยุดเกม
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (!isPaused) // ดำเนินเกมถ้าไม่ได้หยุด
        {
            HandleUpdate();
        }
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    public void TogglePause()
    {
        isPaused = !isPaused; // สลับสถานะหยุดเกม

        if (isPaused)
        {
            Time.timeScale = 0f; // หยุดเวลาในเกม
            pauseMenu.SetActive(true); // แสดงเมนูหยุดเกม
            Cursor.visible = true; // แสดงเมาส์
            // ปลดล็อคเมาส์
        }
        else
        {
            Time.timeScale = 1f; // คืนค่าเวลาในเกม
            pauseMenu.SetActive(false); // ซ่อนเมนูหยุดเกม
             // ซ่อนเมาส์
             // ล็อคเมาส์
        }
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactablesLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null || 
            Physics2D.OverlapCircle(targetPos, 0.2f, interactablesLayer) != null)
        {
            return false;
        }
        return true;
    }

    private void CheckForEncounters()
    {
        // ตรวจสอบว่าได้ชนกับ object ที่จะเปลี่ยนฉาก
        if (Physics2D.OverlapCircle(transform.position, 0.2f, nextSceneLayer) != null)
        {
            StartCoroutine(PlaySoundAndLoadScene("Stage1"));
        }

        if (Physics2D.OverlapCircle(transform.position, 0.2f, stage2Leyer) != null)
        {
            StartCoroutine(PlaySoundAndLoadScene("Stage2"));
        }

        if (Physics2D.OverlapCircle(transform.position, 0.2f, stage3Leyer) != null)
        {
            StartCoroutine(PlaySoundAndLoadScene("Stage3"));
        }

        if (Physics2D.OverlapCircle(transform.position, 0.2f, stage4Leyer) != null)
        {
            StartCoroutine(PlaySoundAndLoadScene("Stage4"));
        }

        if (Physics2D.OverlapCircle(transform.position, 0.2f, stage5Leyer) != null)
        {
            StartCoroutine(PlaySoundAndLoadScene("Stage5"));
        }

        if (Physics2D.OverlapCircle(transform.position, 0.2f, stage6Leyer) != null)
        {
            StartCoroutine(PlaySoundAndLoadScene("Stage6"));
        }

        if (Physics2D.OverlapCircle(transform.position, 0.2f, stage7Leyer) != null)
        {
            {
                // ถ้า soulPoints >= 7 ให้พาไป StageWin
                StartCoroutine(PlaySoundAndLoadScene("Stage7"));
            } 
        }

        // ตรวจสอบการชนกับ FateDoor
        if (Physics2D.OverlapCircle(transform.position, 0.2f, fateDoorLayer) != null)
        {
            if (ScoreManager.Instance.soulPoints >= 7)
            {
                StartCoroutine(PlaySoundAndLoadScene("StageWin"));
            }
            else if (ScoreManager.Instance.soulPoints >= 4)
            {
                StartCoroutine(PlaySoundAndLoadScene("StageWinbut"));
            }
            else
            {
                StartCoroutine(PlaySoundAndLoadScene("StageLoss"));
            }
        }
    }

    private IEnumerator PlaySoundAndLoadScene(string sceneName)
    {
        // หยุดเวลาเกม
        Time.timeScale = 0f;

        if (audioSource != null && audioSource.clip != null)
        {
            // เล่นเสียง
            audioSource.Play();

            // รอจนกว่าเสียงจะเล่นเสร็จ
            yield return new WaitForSecondsRealtime(audioSource.clip.length);
        }

        // คืนค่าเวลาเกม
        Time.timeScale = 1f;

        // โหลดซีนใหม่
        SceneManager.LoadScene(sceneName);
    }
}
