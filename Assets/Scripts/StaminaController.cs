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

    // 스태미너 UI 비활성화
    public void DisableStaminaUI()
    {
        sliderCanvasGroup.alpha = 0;
    }

    // 스태미너 UI 활성화
    public void EnableStaminaUI()
    {
        sliderCanvasGroup.alpha = 1;
    }

    private void Update()
    {
        if (!weAreSprinting)
        {
            // 스태미너가 최대치에 도달하지 않은 경우 회복 진행
            if (playerStamina < 100.0f)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStaminaUI(1);

                // 스태미너가 완전히 회복된 경우
                if (playerStamina >= 100.0f)
                {
                    playerStamina = 100.0f;
                    hasRegenerated = true;  // 스태미너가 완전히 회복되면 hasRegenerated를 true로 설정
                }
            }
        }
    }

    public void Sprinting()
    {
        // 스태미너가 충분히 회복된 경우에만 실행
        if (hasRegenerated)
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStaminaUI(1);

            // 스태미너가 바닥나면 달리기를 중지하고 회복 상태 업데이트
            if (playerStamina <= 0)
            {
                playerStamina = 0;
                hasRegenerated = false;  // 스태미너가 바닥나면 hasRegenerated를 false로 설정
                weAreSprinting = false;
                UpdateStaminaUI(0);      // UI를 숨기도록 설정
            }
        }
    }

    public void StopSprinting()
    {
        weAreSprinting = false;
    }

    private void UpdateStaminaUI(int value)
    {
        staminaProgressUI.fillAmount = playerStamina / 100.0f;

        // 스태미너 UI를 필요에 따라 숨기거나 보이도록 설정
        sliderCanvasGroup.alpha = value == 1 ? 1 : 0;
    }
}
