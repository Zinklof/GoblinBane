using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private CharacterController characterController;
    [Header("Movement variables")]
    [SerializeField] private float moveSpeed;

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            moveSpeed = 0.05f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 0.2f;
        }
        else
        {
            moveSpeed = 0.1f;
        }

            Vector3 x = Input.GetAxis("Horizontal") * moveSpeed * transform.right;
        Vector3 z = Input.GetAxis("Vertical") * moveSpeed * transform.forward;
        Vector3 w = Input.GetAxis("Mouse ScrollWheel") * moveSpeed * 200 * cameraObject.transform.forward;
        Vector3 y = 0f * transform.up;

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space))
            y.y = 1f * moveSpeed;
        else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.LeftControl))
            y.y = -1f * moveSpeed;

        Vector3 normMovement = x + y + z + w;
        
        characterController.Move(normMovement);
    }

    private void Bounds()
    {
        if (playerObject.transform.position.x > 45)
        {
            playerObject.transform.position = new Vector3(45, playerObject.transform.position.y, playerObject.transform.position.z);
        }
        if (playerObject.transform.position.x < -50)
        {
            playerObject.transform.position = new Vector3(-50, playerObject.transform.position.y, playerObject.transform.position.z);
        }
        if (playerObject.transform.position.z > 45)
        {
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, 45);
        }
        if (playerObject.transform.position.z < -50)
        {
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -50);
        }
        if (playerObject.transform.position.y > 25)
        {
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, 25, playerObject.transform.position.z);
        }
        if (playerObject.transform.position.y < 0.5f)
        {
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, 0.5f, playerObject.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Bounds();
    }
}
