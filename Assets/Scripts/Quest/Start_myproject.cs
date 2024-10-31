using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine.UI;

public class Start_myproject : MonoBehaviour
{
    public GameObject Quest_6_Text;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public BoxCollider [] checkpoint;

    private Animator animator1;
    private Animator animator2;
    private Animator animator3;

    private NavMeshAgent navMeshAgent1;
    private NavMeshAgent navMeshAgent2;
    private NavMeshAgent navMeshAgent3;

    void Start()
    {
        //Text t = Quest_6_Text.GetComponent<Text>();

        animator1 = enemy1.GetComponent<Animator>();
        animator2 = enemy2.GetComponent<Animator>();
        animator3 = enemy3.GetComponent<Animator>();

        navMeshAgent1 = enemy1.GetComponent<NavMeshAgent>();
        navMeshAgent2 = enemy2.GetComponent<NavMeshAgent>();
        navMeshAgent3 = enemy3.GetComponent<NavMeshAgent>();



        Quest_6_Text.SetActive(false);
        enemy1.SetActive(false);
        enemy2.SetActive(false);    
        enemy3.SetActive(false);
        

        foreach (var cp in checkpoint)
        {
            cp.enabled = false;
        }//박스콜라이더 싹 다 꺼주기
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
