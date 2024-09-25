using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Start_myproject : MonoBehaviour
{
    public GameObject Quest_6_Text;
    void Start()
    {
        //Text t = Quest_6_Text.GetComponent<Text>();
        Quest_6_Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowMessage(GameObject message, float delay)
    {
        message.SetActive(true); // 텍스트 보이게 하기
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기
        Debug.Log("message set active");
        message.SetActive(false); // 텍스트 숨기기
    }
}
