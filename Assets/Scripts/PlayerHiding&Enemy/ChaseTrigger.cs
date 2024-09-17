using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ChaseTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public StartToChase StartToChase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartToChase.isNearBy = true;
        }
    }
}
