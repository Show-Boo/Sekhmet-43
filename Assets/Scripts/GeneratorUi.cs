using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorController : MonoBehaviour
{
    public Image progressBarBackground; // 배경 이미지
    public Image progressBarFill; // 빨간색으로 차오를 이미지
    public TextMeshProUGUI completeText;
    public Image crosshair;
    public Color crosshairNormalColor = Color.white;
    public Color crosshairHoverColor = Color.red;
    public float holdTime = 10f;
    public LayerMask generatorLayer;
    public PlayerHiding playerHiding; // PlayerHiding 스크립트를 참조하기 위한 변수

    private float currentHoldTime = 0f;
    private bool isHoveringGenerator = false;
    private bool isHolding = false;
    private bool progressComplete = false;

    void Start()
    {
        completeText.enabled = false;
        progressBarFill.fillAmount = 0f;
        progressBarBackground.gameObject.SetActive(false);
        progressBarFill.gameObject.SetActive(false);
        crosshair.color = crosshairNormalColor;

        if (playerHiding == null)
        {
            playerHiding = FindObjectOfType<PlayerHiding>(); // PlayerHiding 스크립트를 찾음
        }
    }

    void Update()
    {
        if (progressComplete)
        {
            return; // progress가 완료된 상태에서는 더 이상 업데이트 하지 않음
        }

        // PlayerHiding 스크립트에서 ray를 가져옴
        Ray ray = playerHiding.playerRay;

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, generatorLayer))
        {
            isHoveringGenerator = true;
            crosshair.color = crosshairHoverColor;

            if (Input.GetMouseButtonDown(0))
            {
                isHolding = true;
                progressBarBackground.gameObject.SetActive(true);
                progressBarFill.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            isHoveringGenerator = false;
            crosshair.color = crosshairNormalColor;
        }

        if (isHolding)
        {
            if (Input.GetMouseButton(0))
            {
                currentHoldTime += Time.deltaTime;
                progressBarFill.fillAmount = currentHoldTime / holdTime;

                if (currentHoldTime >= holdTime)
                {
                    CompleteProgress();
                }
            }
            else
            {
                isHolding = false;
                progressBarBackground.gameObject.SetActive(false);
                progressBarFill.gameObject.SetActive(false);
                currentHoldTime = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void CompleteProgress()
    {
        progressComplete = true;
        isHolding = false;
        progressBarBackground.gameObject.SetActive(false);
        progressBarFill.gameObject.SetActive(false);
        completeText.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Invoke("HideCompleteMessage", 3f);
    }

    private void HideCompleteMessage()
    {
        completeText.enabled = false;
        progressComplete = false;
        currentHoldTime = 0f;
        progressBarFill.fillAmount = 0f;
    }
}



