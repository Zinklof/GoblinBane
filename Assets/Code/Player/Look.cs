using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Look : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject playerObject;
    [Header("Variables")]
    [SerializeField] private float sensitivity;

    private void XRotation()
    {
        float x = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        playerCamera.transform.Rotate(-x, 0, 0);
    }

    private void YRotation()
    {
        float y = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        playerObject.transform.Rotate(0, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) 
        {
            XRotation();
            YRotation();
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
