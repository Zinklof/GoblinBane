using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGoblin : MonoBehaviour
{
    [SerializeField] GameObject Head;
    [SerializeField] Transform Player;

    private void Update()
    {
        Head.transform.LookAt(Player.position);
    }
}
