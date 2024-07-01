using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HidePlayerMovement : MonoBehaviour
{

    private float rotationX = 0;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public Camera playerCamera;
    public Transform target;

    public PlayerHiding playerController; // 스크립트 받아오기

    // Start is called before the first frame update
    void Start()
    {
        playerController = target.GetComponent<PlayerHiding>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isPlayer1Active)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        }
    }
}
