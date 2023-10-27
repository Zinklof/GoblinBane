using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class SpellCode : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] GameObject target;
    [Header("Variables")]
    [SerializeField] bool AOE = true;
    [SerializeField] bool status = true;
    [Header("Stats")]
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] float AOERadious;
    [SerializeField] float animspeed;
    [SerializeField] List<GameObject> AOETargets = new List<GameObject>();

    float i = 0f;
    bool attacked = false;
    bool apexOfAnim = false;
    Vector3 animSize = Vector3.zero;

    public void SetTarget(GameObject tgt)
    {
        target = tgt;

        animSize = new Vector3(AOERadious, AOERadious, AOERadious);
    }

    private void Attack()
    {
        if (AOE)
        {
            AOETargets.Clear();
            AOETargets = GetAllEnemies();

            if (status)
            {
                foreach (GameObject t in AOETargets)
                {
                    Grunt temp = t.GetComponent<Grunt>();
                    if (temp == null)
                    {
                        return;
                    }
                    temp.freeze();
                }
            }
            foreach (GameObject t in AOETargets)
            {
                Grunt temp = t.GetComponent<Grunt>();
                if (temp == null)
                {
                    return;
                }
                temp.DamageGoblin(damage);
            }

            attacked = true;
        }
        else
        {
            Grunt temp = target.GetComponent<Grunt>();
            if (temp == null)
            {
                return;
            }
            temp.DamageGoblin(damage);
        }
    }

    private List<GameObject> GetAllEnemies()
    {
        List<GameObject > enemies = new List<GameObject>();
        foreach (Collider co in Physics.OverlapSphere(target.transform.position, AOERadious))
        {
            if (co.gameObject.tag == "Goblin")
            {
                GameObject temp3 = co.gameObject;
                enemies.Add(temp3);
            }
        }
        return enemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && !attacked)
        {
            Destroy(gameObject); return;
        }

        if (target != null && !attacked)
        {

            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed);
            transform.LookAt(target.transform.position);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.8f)
            {
                Attack();
            }
        }

        if(attacked && !apexOfAnim)
        {
            transform.localScale = animSize;
            if (transform.localScale.magnitude >= animSize.magnitude * .98f)
            {
                apexOfAnim = true;
            }
        }
        if (apexOfAnim)
        {
            transform.localScale = (Vector3.Lerp(transform.localScale, Vector3.zero, animspeed));
            if (transform.localScale.magnitude <= 0.05)
            {
                Destroy(gameObject);
            }
        }
    }
}
