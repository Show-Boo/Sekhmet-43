using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_trigger : MonoBehaviour
{
    public LightController l_controller;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            l_controller.TurnOffAllColor();
        }
    }
}
