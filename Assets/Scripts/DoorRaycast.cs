using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    private MyDoorController raycastedObj;

    [SerializeField] private KeyCode openDoorKey = KeyCode.E;

    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive = false;
    private bool doOnce = false;

    private const string interactableTag = "InteractiveObject";

    private void Start()
    {
        doOnce = false; // 추가: 초기화 로그
        Debug.Log("Start: doOnce initialized to " + doOnce);

        if (crosshair == null)
        {
            Debug.LogError("Crosshair Image is not assigned in the Inspector.");
        }
        else
        {
            Debug.Log("Crosshair Image is assigned and not null.");
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        //int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        int excludeLayer = 1 << LayerMask.NameToLayer(excludeLayerName);
        int mask = layerMaskInteract.value & ~excludeLayer;
        Debug.Log(doOnce);
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            Debug.Log("doOnce before condition: " + doOnce);// 충돌한 오브젝트 이름 출력


            if (hit.collider.CompareTag(interactableTag))
            {
                Debug.Log("Hit an interactive object with tag: " + hit.collider.tag); // 충돌한 오브젝트의 태그 출력
                //Debug.Log(doOnce);
                if (!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<MyDoorController>();
                    if (raycastedObj != null)
                    {
                        Debug.Log("MyDoorController component found on object: " + hit.collider.name);
                        Debug.Log("Calling CrosshairChange with true. on: true, doOnce: " + doOnce);
                        CrosshairChange(true);
                        doOnce = true; // 여기서 doOnce를 true로 설정
                        Debug.Log("doOnce set to true");
                    }
                    else
                    {
                        Debug.LogError("MyDoorController component not found on object: " + hit.collider.name);
                       
                    }
                }

                isCrosshairActive = true;

                if (Input.GetKeyDown(openDoorKey))
                {
                    Debug.Log("E key pressed, attempting to open door");
                    if (raycastedObj != null)
                    {
                        raycastedObj.PlayAnimation();
                    }
                }
            }
            else
            {
                Debug.Log("Hit object does not have the correct tag");
                ResetCrosshair();
            }
        }
        /*if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.CompareTag(interactableTag))
            {
                Debug.Log("Hit an interactive object");
                Debug.Log(doOnce);
                if (!doOnce)
                {
                    Debug.Log("....................");
                    raycastedObj = hit.collider.gameObject.GetComponent<MyDoorController>();
                    if (raycastedObj != null)
                    {
                        Debug.Log("MyDoorController component found on object: " + hit.collider.name);
                        Debug.Log("Calling CrosshairChange with true. on: true, doOnce: " + doOnce);
                        CrosshairChange(true);
                        doOnce = true;
                    }
                    else
                    {
                        Debug.LogError("MyDoorController component not found on object: " + hit.collider.name);
                        
                    }
                }

                isCrosshairActive = true;
                

                if (Input.GetKeyDown(openDoorKey))
                {
                    Debug.Log("E key pressed, attempting to open door");
                    raycastedObj.PlayAnimation();
                }

            }
        }*/

        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
                ResetCrosshair();
            }
        }

    }

    /*void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }*/
    void CrosshairChange(bool on)
    {
        Debug.Log("CrosshairChange called with parameter: " + on + ", doOnce: " + doOnce);
        if (on && !doOnce)
        {
            Debug.Log("Inside if (on && !doOnce)");
            if (crosshair != null)
            {
                Debug.Log("Crosshair is not null, attempting to change color to red");
                crosshair.color = Color.red;
                Debug.Log("Crosshair changed to red");
            }
            else
            {
                Debug.LogError("Crosshair is null, cannot change color to red");
            }
        }
        else
        {
            Debug.Log("Inside else");
            crosshair.color = Color.white;
            isCrosshairActive = false;
            Debug.Log("Crosshair changed to white");
        }
    }

    void ResetCrosshair()
    {
        Debug.Log("Resetting crosshair and doOnce");
        CrosshairChange(false);
        doOnce = false;
        raycastedObj = null;
    }
}

