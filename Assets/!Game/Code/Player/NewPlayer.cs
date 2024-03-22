using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public static class Player
{
    static bool hitBoxes = false;
    static bool bounds = false;

    public delegate void PlayerEventHandeler();
    public static event PlayerEventHandeler PlayerRenderValuesChanged;

    public static bool ToggleHitboxes()
    {
        if (hitBoxes)
        {
            hitBoxes = false;
            PlayerRenderValuesChanged();
            return false;
        }
        else
        {
            hitBoxes = true;
            PlayerRenderValuesChanged();
            return true;
        }
    }

    public static bool ToggleBounds()
    {
        if (bounds)
        {
            bounds = false;
            PlayerRenderValuesChanged();
            return false;
        }
        else
        {
            bounds = true;
            PlayerRenderValuesChanged();
            return true;
        }
    }

    public static bool GetBoundsValue()
    {
        if (bounds) 
        return true; 
        else
        return false;
    }

    public static bool GetHitboxesValue()
    {
        if (hitBoxes)
            return true;
        else
            return false;
    }
}


public class NewPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Camera cameraObject;
    [SerializeField] private CharacterController characterController;
    [Header("Movement variables")]
    [SerializeField] float baseSpeed;
    [SerializeField] float altMult;
    [SerializeField] float shiftMult;
    private float moveSpeed;


    private void Start()
    {
        Player.PlayerRenderValuesChanged += this.ChangeSettings;
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            moveSpeed = baseSpeed * altMult;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = baseSpeed * shiftMult;
        }
        else
        {
            moveSpeed = baseSpeed;
        }

        Vector3 x = Input.GetAxis("Horizontal") * moveSpeed * transform.right * Time.deltaTime;
        Vector3 z = Input.GetAxis("Vertical") * moveSpeed * transform.forward * Time.deltaTime;
        Vector3 w = Input.GetAxis("Mouse ScrollWheel") * moveSpeed * 200 * cameraObject.transform.forward * Time.deltaTime;
        Vector3 y = 0f * transform.up;

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space))
            y.y = 1f * moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.LeftControl))
            y.y = -1f * moveSpeed * Time.deltaTime;

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

    private void ChangeSettings()
    {
        if (Player.GetHitboxesValue())
        {
            cameraObject.cullingMask |= 1 << 9;
        }
        else
        {
            cameraObject.cullingMask &= ~(1 << 9);
        }
        if (Player.GetBoundsValue())
        {
            cameraObject.cullingMask |= 1 << 10;
        }
        else
        {
            cameraObject.cullingMask &= ~(1 << 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Bounds();
    }
}
