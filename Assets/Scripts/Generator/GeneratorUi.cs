using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorController : MonoBehaviour
{
    public RectTransform redBar;
    public RectTransform whiteBar;
    public TextMeshProUGUI completeText;
    public Image crosshair;

    public Color crosshairNormalColor = Color.white;
    public Color crosshairHoverColor = Color.red;

    public float duration = 10f;
    public LayerMask generatorLayer;
    public PlayerHiding playerHiding;

    private float elapsedTime = 0f;
    private bool isHoveringGenerator = false;
    private bool isHolding = false;
    private bool isProgressing = false;
    private bool isFixed = false; // 개별 발전기 수리 완료 상태

    public GameObject repairingAudio;
    private AudioSource repairingAudioSource;
    private bool soundPlaying = false;

    public int id; // 각 발전기에 고유 ID를 부여
    public bool isComplete = false; // 발전기 완료 상태 추가
    private bool progressComplete = false;

    void Start()
    {
        completeText.enabled = false;
        redBar.gameObject.SetActive(false);
        whiteBar.gameObject.SetActive(false);
        crosshair.color = crosshairNormalColor;

        if (playerHiding == null)
        {
            playerHiding = FindObjectOfType<PlayerHiding>();
        }

        if (repairingAudio != null)
        {
            repairingAudioSource = repairingAudio.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Repairing audio object is not assigned!");
        }
    }

    void Update()
    {
        Ray ray = playerHiding.playerRay;
        RaycastHit hit;

        // Raycast가 발전기를 감지하는지 확인
        if (Physics.Raycast(ray, out hit, playerHiding.checkDistance, generatorLayer))
        {
            // 발전기와 상호작용 중인지 확인
            if (hit.collider.gameObject == gameObject)
            {
                if (!isFixed)
                {
                    isHoveringGenerator = true;
                    crosshair.color = crosshairHoverColor;

                    if (Input.GetMouseButton(0))
                    {
                        isHolding = true;
                        redBar.gameObject.SetActive(true);
                        whiteBar.gameObject.SetActive(true);
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;

                        elapsedTime += Time.deltaTime;

                        if (!soundPlaying && repairingAudioSource != null)
                        {
                            repairingAudioSource.Play();
                            soundPlaying = true;
                        }

                        if (elapsedTime >= duration)
                        {
                            elapsedTime = duration;
                            isProgressing = false;
                            HideBars();
                            CompleteProgress();

                            // 수리 완료 시 메서드 호출
                            GeneratorManager.Instance.RepairGenerator(id);
                            if (repairingAudioSource != null)
                            {
                                repairingAudioSource.Stop();
                            }
                        }
                        else
                        {
                            isProgressing = true;
                        }

                        float width = Mathf.Lerp(0, 600, elapsedTime / duration);
                        redBar.sizeDelta = new Vector2(width, redBar.sizeDelta.y);
                    }
                    else
                    {
                        if (isHolding)
                        {
                            HideBars();
                            elapsedTime = 0;
                            redBar.sizeDelta = new Vector2(0, redBar.sizeDelta.y);
                            isHolding = false;

                            if (repairingAudioSource != null)
                            {
                                repairingAudioSource.Stop();
                            }
                            soundPlaying = false;
                        }
                    }
                }
            }
        }
        else
        {
            // 발전기를 벗어나면 커서를 기본 색상으로 변경
            isHoveringGenerator = false;
            crosshair.color = crosshairNormalColor;

            if (isHolding)
            {
                HideBars();
                elapsedTime = 0;
                redBar.sizeDelta = new Vector2(0, redBar.sizeDelta.y);
                isHolding = false;

                if (repairingAudioSource != null)
                {
                    repairingAudioSource.Stop();
                }
                soundPlaying = false;
            }
        }
    }

    private void CompleteProgress()
    {
        progressComplete = true;
        isHolding = false;

        if (completeText != null)
        {
            completeText.enabled = true;

            // 3초 후에 메시지를 비활성화
            Invoke("HideCompleteMessage", 3f);
        }
        else
        {
            Debug.LogError("CompleteText is not assigned!");
        }

        // 마우스 커서 다시 보이도록 설정
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // GeneratorManager에 발전기 수리 완료를 알림
        GeneratorManager.Instance.RepairGenerator(id);

        Debug.Log("발전기 " + id + "가 수리되었습니다.");
    }

    private void HideCompleteMessage()
    {
        if (completeText != null)
        {
            completeText.enabled = false;
        }
    }

    private void HideBars()
    {
        redBar.gameObject.SetActive(false);
        whiteBar.gameObject.SetActive(false);
    }
}
