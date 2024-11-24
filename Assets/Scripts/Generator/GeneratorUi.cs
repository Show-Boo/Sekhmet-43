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

                        // 경과 시간 증가
                        elapsedTime += Time.deltaTime;

                        // 사운드가 재생 중이 아니면 재생
                        if (!soundPlaying && repairingAudioSource != null)
                        {
                            repairingAudioSource.Play();
                            soundPlaying = true;
                        }

                        // 경과 시간이 지정된 지속 시간 이상일 경우 수리 완료 처리
                        if (elapsedTime >= duration)
                        {
                            elapsedTime = duration; // 경과 시간을 지속 시간으로 고정
                            isProgressing = false;  // 진행 상태 해제
                            HideBars();             // 진행 UI 숨기기
                            CompleteProgress();     // 수리 완료 처리
                        }
                        else
                        {
                            isProgressing = true; // 진행 상태 유지
                        }

                        // 진행 바의 크기 조정
                        float width = Mathf.Lerp(0, 600, elapsedTime / duration);
                        redBar.sizeDelta = new Vector2(width, redBar.sizeDelta.y);
                    }
                    else
                    {
                        // 수리 중단 시 초기화
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

            // 수리 중단 시 초기화
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

        // completeText가 할당되지 않았을 경우 에러 메시지 출력
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

        // GeneratorManager.Instance가 null인지 확인
        if (GeneratorManager.Instance != null)
        {
            // GeneratorManager에 발전기 수리 완료를 알림
            GeneratorManager.Instance.RepairGenerator(id);
            Debug.Log("발전기 " + id + "가 수리되었습니다.");
        }
        else
        {
            Debug.LogError("GeneratorManager.Instance is not assigned!");
        }


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
