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

        repairingAudioSource = repairingAudio.GetComponent<AudioSource>();
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

                        if (!soundPlaying)
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

                            GeneratorManager.Instance.RepairGenerator();
                            repairingAudioSource.Stop();
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

                            repairingAudioSource.Stop();
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

                repairingAudioSource.Stop();
                soundPlaying = false;
            }
        }
    }

    private void CompleteProgress()
    {
        isFixed = true; // 개별 발전기 수리 완료 상태로 설정
        isHolding = false;
        completeText.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Invoke("HideCompleteMessage", 3f);
    }

    private void HideCompleteMessage()
    {
        completeText.enabled = false;
    }

    private void HideBars()
    {
        redBar.gameObject.SetActive(false);
        whiteBar.gameObject.SetActive(false);
    }
}