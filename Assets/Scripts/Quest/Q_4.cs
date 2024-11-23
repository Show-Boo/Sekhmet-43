using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Q_4 : Q_ParentClass
{

    // Start is called before the first frame update
    public override bool cutscene { get => false_cutscene; set => false_cutscene = value; }
    
    public DoorOpenUI displayUIOnApproach; //{ get; set; }-> �̰� �� ����ϳ�? ui system��ũ��Ʈ
    //public QuestManager QuestManager;//�������ܼ� get set ����..
    public GameObject questUI; // ����Ʈ UI�� ��Ÿ���� GameObject �߰�

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    private Animator animator1;
    private Animator animator2;
    private Animator animator3;

    private NavMeshAgent navMeshAgent1;
    private NavMeshAgent navMeshAgent2;
    private NavMeshAgent navMeshAgent3;

    public GameObject eating_enemy;
    public GameObject coworker;

    public BoxCollider checkpoint1;
    public BoxCollider Deadpoint;

    private void Start()
    {
        animator1 = enemy1.GetComponent<Animator>();
        animator2 = enemy2.GetComponent<Animator>();
        animator3 = enemy3.GetComponent<Animator>();

        navMeshAgent1 = enemy1.GetComponent<NavMeshAgent>();
        navMeshAgent2 = enemy2.GetComponent <NavMeshAgent>();
        navMeshAgent3 = enemy3.GetComponent<NavMeshAgent>();

        Debug.Log(animator1);
    }

    public override void UpdateQuest()
    {
        if (displayUIOnApproach.isFirst)
        {
            QuestManager.CompleteObjective();
        }

    }

    public override void NextQuest()
    {
        StartCoroutine(ShowQuestUI());

        enemy1.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);
        checkpoint1.enabled = false;

        animator1.SetBool("IsWander",true);
        animator2.SetBool("IsWander", true);
        animator3.SetBool("IsWander", true);

        eating_enemy.SetActive(false);
        coworker.SetActive(false);
        Deadpoint.enabled = false;
    }
    
    private IEnumerator ShowQuestUI()
    {
        questUI.SetActive(true); // UI Ȱ��ȭ
        yield return new WaitForSeconds(3); // 3�� ���
        questUI.SetActive(false); // UI ��Ȱ��ȭ
    }

}
