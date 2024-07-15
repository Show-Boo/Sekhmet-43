using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
