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
        message.SetActive(true); // �ؽ�Ʈ ���̰� �ϱ�
        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���
        Debug.Log("message set active");
        message.SetActive(false); // �ؽ�Ʈ �����
    }
}
