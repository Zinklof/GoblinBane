using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCode : MonoBehaviour
{
    [SerializeField] float attackSpeedSeconds;
    [SerializeField] GameObject target;
    [SerializeField] GameObject spell;
    [SerializeField] float attackDistance;
    [SerializeField] Transform spellRelease;
    [SerializeField] float attackDelay = 0f;

    private void Awake()
    {
        target = ObjectChecker.findClosestGoblin(transform);
    }

    private void Attack()
    {
        GameObject temp = Instantiate(spell, spellRelease.position, spellRelease.rotation);
        SpellCode spellCode = temp.GetComponent<SpellCode>();

        spellCode.SetTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay -= Time.deltaTime;

        if (attackDelay <= 0f)
        {
            target = ObjectChecker.findClosestGoblin(transform);

            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < attackDistance)
                {
                    Attack();
                }
            }
            attackDelay = attackSpeedSeconds;
        }
    }
}
