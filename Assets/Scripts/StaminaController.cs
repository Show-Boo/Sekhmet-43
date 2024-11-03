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

    private bool isCutsceneActive = false; // 컷씬 중 여부를 확인할 변수

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

    // 컷씬 중 스태미너 UI 비활성화
    public void DisableForCutscene()
    {
        isCutsceneActive = true;
        DisableStaminaUI();
    }

    // 컷씬 종료 후 스태미너 UI 활성화
    public void EnableAfterCutscene()
    {
        isCutsceneActive = false;
        EnableStaminaUI();
    }

    private void Update()
    {
        if (!weAreSprinting && !isCutsceneActive)
        {
            if (playerStamina < 100.0f)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStaminaUI(1);

                if (playerStamina >= 100.0f)
                {
                    playerStamina = 100.0f;
                    hasRegenerated = true;
                }
            }
        }
    }

    public void Sprinting()
    {
        if (hasRegenerated)
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStaminaUI(1);

            if (playerStamina <= 0)
            {
                playerStamina = 0;
                hasRegenerated = false;
                weAreSprinting = false;
                UpdateStaminaUI(0);
            }
        }
    }

    public void StopSprinting()
    {
        weAreSprinting = false;
    }

    private void UpdateStaminaUI(int value)
    {
        if (isCutsceneActive) return;

        staminaProgressUI.fillAmount = playerStamina / 100.0f;
        sliderCanvasGroup.alpha = value == 1 ? 1 : 0;
    }
}
