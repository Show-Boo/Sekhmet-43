using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouDied_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TryAgain;
    private bool first = true;

    public GameObject Youdied;
    private Button button;
    private bool buttonOn = false;

    public EnemyMove[] EnemyMove;

    void Start()
    {
        if (first)
        {
            first = false;
            gameObject.SetActive(false);
        }

        button = TryAgain.GetComponentInChildren<Button>();

    }

    void OnEnable()
    {
        // ������Ʈ�� Ȱ��ȭ�� �� ȣ���� �Լ�
        TryAgain.SetActive(false);
        buttonOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("InvokeTryagain", 2f);

        if (buttonOn)
        {
            button.onClick.AddListener(HideImage);

            if (Input.GetKeyDown(KeyCode.Return)) // ���� Ű�� KeyCode.Return���� ����. �ϴ� ����Ű�� ��ü
            {
                HideImage();
            }
        }
        
    }

    public void InvokeTryagain()
    {
        TryAgain.SetActive(true);

        buttonOn = true;

    }

    public void HideImage()
    {
        Debug.Log("button");

        foreach (var enemyMoveScript in EnemyMove)
        {
            enemyMoveScript.PlayerDead = false;//�ٽ� ���� �簳
            enemyMoveScript.retry = false;//��ġ���� ��
        }

        gameObject.SetActive(false);
    }
}
