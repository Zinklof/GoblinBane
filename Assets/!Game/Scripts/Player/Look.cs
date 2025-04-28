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
    [SerializeField] private Vector3 cameraRotationEuler;

    private void XRotation()
    {
        float x = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        cameraRotationEuler += new Vector3(-x, 0, 0);
        if (cameraRotationEuler.x > 90 )
        {
            cameraRotationEuler.x = 90;
        }
        if (cameraRotationEuler.x < -90)
        {
            cameraRotationEuler.x = -90;
        }

        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationEuler);
    }

    private void YRotation()
    {
        float y = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        playerObject.transform.Rotate(0, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.F))
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
