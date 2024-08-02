using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorController : MonoBehaviour
{
    public Image progressBarBackground; // ��� �̹���
    public Image progressBarFill; // ���������� ������ �̹���
    public TextMeshProUGUI completeText;
    public Image crosshair;
    public Color crosshairNormalColor = Color.white;
    public Color crosshairHoverColor = Color.red;
    public float holdTime = 10f;
    public LayerMask generatorLayer;
    public PlayerHiding playerHiding; // PlayerHiding ��ũ��Ʈ�� �����ϱ� ���� ����

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
            playerHiding = FindObjectOfType<PlayerHiding>(); // PlayerHiding ��ũ��Ʈ�� ã��
        }
    }

    void Update()
    {
        if (progressComplete)
        {
            return; // progress�� �Ϸ�� ���¿����� �� �̻� ������Ʈ ���� ����
        }

        // PlayerHiding ��ũ��Ʈ���� ray�� ������
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



