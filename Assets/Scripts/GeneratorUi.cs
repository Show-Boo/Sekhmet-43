using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorController : MonoBehaviour
{

    //1. �Ҹ� �߰�
    //2. ī�޶� ����
    //3. �� �������� ������ ��۹�� ����
    //4. ���� �����̴ٰ� ���ƿ��� -> �Ͽ��ϱ��� done
    //5. �������� ��� �ٲ�ֱ�

    public RectTransform redBar; // ���� ���� UI�� RectTransform
    public RectTransform whiteBar; // �Ͼ� ���� UI�� RectTransform

    //public Image progressBarBackground; // ��� �̹���
    //public Image progressBarFill; // ���������� ������ �̹���

    public TextMeshProUGUI completeText;
    public Image crosshair;

    public Color crosshairNormalColor = Color.white;
    public Color crosshairHoverColor = Color.red;

    public float duration = 10f;
    public LayerMask generatorLayer;
    public PlayerHiding playerHiding; // PlayerHiding ��ũ��Ʈ�� �����ϱ� ���� ����

    private float elapsedTime = 0f;
    private bool isHoveringGenerator = false;

    private bool isHolding = false;
    private bool progressComplete = false;
    private bool isProgressing = false;

    public bool AllGeneratorFixed = false;//�����Ⱑ �� ����������?

    void Start()
    {
        completeText.enabled = false;
        //progressBarFill.fillAmount = 0f;
        redBar.gameObject.SetActive(false);
        whiteBar.gameObject.SetActive(false);
        //progressBarFill.gameObject.SetActive(false);
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

            if (Input.GetMouseButton(0))//ButtonDown�� ������ ���� true�ε�
            {
                isHolding = true;
                //progressBarBackground.gameObject.SetActive(true);
                //progressBarFill.gameObject.SetActive(true);
                redBar.gameObject.SetActive(true);
                whiteBar.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                elapsedTime += Time.deltaTime;

                if (elapsedTime >= duration)
                {
                    elapsedTime = duration;
                    isProgressing = false;
                    HideBars(); // ����� �����, ��
                    CompleteProgress();
                }

                else
                {
                    isProgressing = true;//������?
                }

                // ���� ���� UI�� width�� ����
                float width = Mathf.Lerp(0, 700, elapsedTime / duration);
                redBar.sizeDelta = new Vector2(width, redBar.sizeDelta.y);

            }
            else
            {
                if (isHolding) // ���콺 ��ư�� �� ��� �ʱ�ȭ
                {
                    HideBars();
                    elapsedTime = 0;
                    redBar.sizeDelta = new Vector2(0, redBar.sizeDelta.y);
                    isHolding = false;
                }
            }
        }
        else
        {
            isHoveringGenerator = false;
            crosshair.color = crosshairNormalColor;

            if (isHolding) // ���콺 ��ư�� �� ��� �ʱ�ȭ
            {
                HideBars();
                elapsedTime = 0;
                redBar.sizeDelta = new Vector2(0, redBar.sizeDelta.y);
                isHolding = false;
            }
        }

        /*
        if (isHolding)
        {
            if (Input.GetMouseButton(0))
            {
                elapsedTime += Time.deltaTime;
                //progressBarFill.fillAmount = currentHoldTime / holdTime;

                if (currentHoldTime >= holdTime)
                {
                    CompleteProgress();
                }
            }

            else
            {
                isHolding = false;
                //progressBarBackground.gameObject.SetActive(false);
                //progressBarFill.gameObject.SetActive(false);
                currentHoldTime = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        */

    }

    private void CompleteProgress()
    {
        progressComplete = true;
        isHolding = false;
        //progressBarBackground.gameObject.SetActive(false);
        //progressBarFill.gameObject.SetActive(false);
        completeText.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Invoke("HideCompleteMessage", 3f);
    }

    private void HideCompleteMessage()
    {
        completeText.enabled = false;
        progressComplete = false;
        //currentHoldTime = 0f;
        //progressBarFill.fillAmount = 0f;
    }

    private void HideBars()
    {
        redBar.gameObject.SetActive(false);
        whiteBar.gameObject.SetActive(false);
    }

}



