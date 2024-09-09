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
       
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        //int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        int excludeLayer = 1 << LayerMask.NameToLayer(excludeLayerName);
        int mask = layerMaskInteract.value & ~excludeLayer;
    
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {


            if (hit.collider.CompareTag(interactableTag))
            {
                
                if (!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<MyDoorController>();
                    if (raycastedObj != null)
                    {
                        CrosshairChange(true);
                        doOnce = true; // 여기서 doOnce를 true로 설정
                     
                    }
                    
                }

                isCrosshairActive = true;

                if (Input.GetKeyDown(openDoorKey))
                {
               
                    if (raycastedObj != null)
                    {
                        raycastedObj.PlayAnimation();
                    }
                }
            }
            else
            {
       
                ResetCrosshair();
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

    }

    void CrosshairChange(bool on)
    {
        
        if (on && !doOnce)
        {

            if (crosshair != null)
            {

                crosshair.color = Color.red;
            }
              
            
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
           
        }
    }

    void ResetCrosshair()
    {
       
        CrosshairChange(false);
        doOnce = false;
        raycastedObj = null;
    }
}

