using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDoorController : MonoBehaviour
{
    private Animator doorAnim;
    private bool doorOpen = false;
    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }


    public void PlayAnimation()
    {
        if (doorAnim == null)
        {
            Debug.LogError("Animator component not found on door object.");
            return;
        }
        if (!doorOpen)
        {
            doorAnim.Play("DoorOpen", 0, 0.0f);
            doorOpen = true;
        }
        else
        {
            doorAnim.Play("DoorClose", 0, 0.0f);
            doorOpen= false;
        }
    }

}
