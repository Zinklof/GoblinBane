using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerObject;
    [Header("Movement variables")]
    [SerializeField] private float moveSpeed;

    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;
        float y = 0f;

        if (Input.GetKey(KeyCode.Space))
            y = 1f * moveSpeed;
        else if (Input.GetKey(KeyCode.LeftControl))
            y = -1f * moveSpeed;

        //playerObject.transform.position += new Vector3(x, y, z) * Time.deltaTime;

        playerObject.transform.position += transform.forward * z * Time.deltaTime;
        playerObject.transform.position += transform.up * y * Time.deltaTime;
        playerObject.transform.position += transform.right * x * Time.deltaTime;
    }

    private void Bounds()
    {
        if (playerObject.transform.position.x > 25) 
        {
            playerObject.transform.position = new Vector3(25, playerObject.transform.position.y, playerObject.transform.position.z);
        }
        if (playerObject.transform.position.x < -25)
        {
            playerObject.transform.position = new Vector3(-25, playerObject.transform.position.y, playerObject.transform.position.z);
        }
        if (playerObject.transform.position.z > 25)
        {
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, 25);
        }
        if (playerObject.transform.position.z < -25)
        {
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -25);
        }
        if (playerObject.transform.position.y > 15)
        {
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, 15, playerObject.transform.position.z);
        }
        if (playerObject.transform.position.y < -15)
        {
            playerObject.transform.position = new Vector3(playerObject.transform.position.x, -15, playerObject.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        //Bounds();
    }
}
