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
                    hasRegenerated = true;  // 스태미너가 완전히 회복되면 hasRegenerated를 true로 설정
                }
            }
        }
    }

    public void Sprinting()
    {
        if (hasRegenerated)  // 스태미너가 회복된 후에만 달리기 가능
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina <= 0)
            {
                playerStamina = 0;
                hasRegenerated = false;  // 스태미너가 바닥나면 달리기 중지
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
