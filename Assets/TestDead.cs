using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDead : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deadCutScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            deadCutScene.SetActive(true);
        }
    }


   
}
