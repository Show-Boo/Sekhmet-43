using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorController : MonoBehaviour
{

    //1. 소리 추가
    //2. 카메라 고정
    //3. 다 고쳐지면 발전기 뱅글뱅글 돌기
    //4. 빛도 깜빡이다가 돌아오기 -> 일요일까지 done
    //5. 괴물에셋 사고 바꿔넣기

    public RectTransform redBar; // 빨간 막대 UI의 RectTransform
    public RectTransform whiteBar; // 하얀 막대 UI의 RectTransform

    //public Image progressBarBackground; // 배경 이미지
    //public Image progressBarFill; // 빨간색으로 차오를 이미지

    public TextMeshProUGUI completeText;
    public Image crosshair;

    public Color crosshairNormalColor = Color.white;
    public Color crosshairHoverColor = Color.red;

    public float duration = 10f;
    public LayerMask generatorLayer;
    public PlayerHiding playerHiding; // PlayerHiding 스크립트를 참조하기 위한 변수

    private float elapsedTime = 0f;
    private bool isHoveringGenerator = false;

    private bool isHolding = false;
    private bool progressComplete = false;
    private bool isProgressing = false;

    public bool AllGeneratorFixed = false;//발전기가 다 고쳐졌는지?

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

            if (Input.GetMouseButton(0))//ButtonDown은 눌리는 순간 true인듯
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
                    HideBars(); // 막대들 숨기기, 끝
                    CompleteProgress();
                }

                else
                {
                    isProgressing = true;//진행중?
                }

                // 빨간 막대 UI의 width를 조정
                float width = Mathf.Lerp(0, 700, elapsedTime / duration);
                redBar.sizeDelta = new Vector2(width, redBar.sizeDelta.y);

            }
            else
            {
                if (isHolding) // 마우스 버튼을 뗀 경우 초기화
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

            if (isHolding) // 마우스 버튼을 뗀 경우 초기화
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



