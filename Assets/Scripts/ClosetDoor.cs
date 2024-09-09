using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClosetDoor : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;

    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive = false;
    private bool doOnce = false;

    private const string interactableTag = "InteractiveObject";
    */

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        //int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        int excludeLayer = 1 << LayerMask.NameToLayer(excludeLayerName);
        int mask = layerMaskInteract.value & ~excludeLayer;
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
        }

        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
                ResetCrosshair();
            }
        }
        */
    }
    
}

