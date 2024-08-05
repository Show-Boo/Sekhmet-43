using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    public float threshold;


    void FixedUpdate()
    {
        if(transform.position.y < threshold)
        {
            transform.position = new Vector3(6.21f, -18f, 28f);
        }
    }

}
