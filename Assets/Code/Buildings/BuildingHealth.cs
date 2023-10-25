using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    [Header("stats")]
    [SerializeField] float health;
    [Header("references")]
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject playerObject;
    [Header("Math")]
    [SerializeField] float hpBarDivider = 1000;
    [SerializeField] float hpBarHeight = 0.3f;

    public void Damage(float dmg)
    {
        health -= dmg;
    }

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }

        float temp = health / hpBarDivider;

        HealthBar.transform.localScale = new Vector3(temp, hpBarHeight, 1);

        HealthBar.transform.LookAt(playerObject.transform);
    }
}
