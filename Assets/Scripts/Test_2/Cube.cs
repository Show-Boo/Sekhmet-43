using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update

    public Q2 q2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            q2.tagPlayer = true;
            Debug.Log("cube gone");
            transform.gameObject.SetActive(false); // 두번째 퀘스트 완료
            
        }
    }
}
