using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using ZinklofDev.Utils;
using UnityEngine;

public class TowerCode : MonoBehaviour
{
    [SerializeField] float attackSpeedSeconds;
    [SerializeField] GameObject target;
    [SerializeField] GameObject spell;
    [SerializeField] GameObject rotater;
    [SerializeField] float attackDistance;
    [SerializeField] float minAttackDistance;
    [SerializeField] Transform spellRelease;
    [SerializeField] float attackDelay = 0f;
    [SerializeField] ParticleSystem[] particleSystems = new ParticleSystem[0];

    private void Awake()
    {
        target = ObjectChecker.findClosestGoblin(transform);
        attackDistance = (float)MathZ.Square(attackDistance);
        minAttackDistance = (float)MathZ.Square(minAttackDistance);
    }

    private void Attack()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }

        if (rotater != null)
        {
            Vector3 rotation = Quaternion.LookRotation(target.transform.position).eulerAngles;
            rotation.x = 0f;
            rotation.z = 0f;

            rotater.transform.rotation = Quaternion.Euler(rotation);
        }

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
                if (MathZ.VectorDistanceSquared(transform.position, target.transform.position) < attackDistance && MathZ.VectorDistanceSquared(transform.position, target.transform.position) > minAttackDistance)
                {
                    Attack();
                    attackDelay = attackSpeedSeconds;
                }
            }
        }
    }
}
