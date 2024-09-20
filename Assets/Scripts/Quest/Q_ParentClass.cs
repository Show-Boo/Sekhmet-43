using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


[System.Serializable]
public class Q_ParentClass: MonoBehaviour
{
    //parent class

    public string questName;  // ����Ʈ �̸�

    [SerializeField] public bool true_cutscene = true;
    [SerializeField] public bool false_cutscene = false;
    public virtual bool cutscene { get => true_cutscene; set => true_cutscene = value; }  // ����Ʈ �Ϸ� �� ����� �ƾ��� �ִ��� ����. �������̵� ����

    public bool isActive = false;  // ����Ʈ Ȱ��ȭ ����
    public bool isCompleted = false;  // ����Ʈ �Ϸ� ����
    
    public CutSceneController cutsceneController;
    public QuestManager QuestManager;

    public virtual void UpdateQuest()
    {
        // �� �Լ��� Ư�� ����Ʈ�� Ȱ��ȭ�� ���� �� �����Ӹ��� ȣ��˴ϴ�.
        // �ʿ��� ��� �� ����Ʈ���� �� �޼��带 �������̵��Ͽ� ����� �� �ֽ��ϴ�.

    }
    

    public void CompleteQuest()//�ƾ��� �ϳ��� ����Ʈ�� ������ ��쿡 Ʋ����
    {
        if (cutscene ==true)//�ƾ��� �ִ� ��츸
        {
            Debug.Log($"{questName} cutscene has been activated.");
            cutsceneController.PlayCutscene();
            //cutsceneController.nowIndex++;//�ƾ��� �ִ� ��쿡�� index++
        }

        isCompleted = true;
        isActive = false;

        NextQuest();//���� ����Ʈ�� ���� ������Ʈ Ȱ��ȭ
        Debug.Log($"{questName} completed!");
    }

    public virtual void NextQuest()//���� ����Ʈ�� ���� ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ�ϴ� �ڵ�!! �������̵� ����
    {
        
    }
}
