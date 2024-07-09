/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro UseText;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayers;

    public void OnUse()
    {
        if(Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            if (hit.collider.TryGetComponent<RotateDoor>(out RotateDoor door))
            {
                if (door.IsOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
        }
    }

    private void Update()
    {
        if(Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance,UseLayers)
            && hit.collider.TryGetComponent<RotateDoor>(out RotateDoor door))
        {
            if (door.IsOpen)
            {
                UseText.SetText("Close \"E\"");
            }
            else
            {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI UseText; // TextMeshProUGUI�� ����
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayers; // ���̾� ���� ����

    public void OnUse()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            if (hit.collider.TryGetComponent<RotateDoor>(out RotateDoor door))
            {
                if (door.IsOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed"); // ����� �α� �߰�
            OnUse();
        }

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)
            && hit.collider.TryGetComponent<RotateDoor>(out RotateDoor door))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (door.IsOpen)
            {
                UseText.SetText("Close \"E\"");
            }
            else
            {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}
