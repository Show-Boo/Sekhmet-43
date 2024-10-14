using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] public float jumpCost = 20f;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool weAreSprinting = false;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)][SerializeField] private float staminaDrain = 0.3f;
    [Range(0, 50)][SerializeField] private float staminaRegen = 0.5f;

    [Header("Stamina UI Elements")]
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;


    // ���¹̳� UI ��Ȱ��ȭ
    public void DisableStaminaUI()
    {
        sliderCanvasGroup.alpha = 0; // UI�� ����ϴ�.
    }

    // ���¹̳� UI Ȱ��ȭ
    public void EnableStaminaUI()
    {
        sliderCanvasGroup.alpha = 1; // UI�� �ٽ� ���̰� �մϴ�.
    }


    private void Update()
    {
        if (!weAreSprinting)
        {
            if (playerStamina <= 100.0f - 0.01f)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStamina(1);

                if (playerStamina >= 100.0f)
                {
                    playerStamina = 100.0f;
                    sliderCanvasGroup.alpha = 0;
                    hasRegenerated = true;  // ���¹̳ʰ� ������ ȸ���Ǹ� hasRegenerated�� true�� ����
                }
            }
        }
    }

    public void Sprinting()
    {
        if (hasRegenerated)  // ���¹̳ʰ� ȸ���� �Ŀ��� �޸��� ����
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina <= 0)
            {
                playerStamina = 0;
                hasRegenerated = false;  // ���¹̳ʰ� �ٴڳ��� �޸��� ����
                sliderCanvasGroup.alpha = 0;
                weAreSprinting = false;
            }
        }
    }

    public void StopSprinting()
    {
        weAreSprinting = false;
    }



    void UpdateStamina(int value)
    {
        staminaProgressUI.fillAmount = playerStamina / 100.0f;

        if (value == 0)
        {
            sliderCanvasGroup.alpha = 0;
        }
        else
        {
            sliderCanvasGroup.alpha = 1;
        }
    }
}
