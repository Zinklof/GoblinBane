using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;

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

    Vector3 lastKnownLocation = Vector3.zero;
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
        GetComponent<AudioSource>().Play();
        if (AOE && target != null)
        {
            AOETargets.Clear();
            AOETargets = GetAllEnemies(target.transform.position);

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
        else if (AOE)
        {
            AOETargets.Clear();
            AOETargets = GetAllEnemies(lastKnownLocation);

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
        else if (target != null)
        {
            Grunt temp = target.GetComponent<Grunt>();
            if (temp == null)
            {
                return;
            }
            temp.DamageGoblin(damage);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private List<GameObject> GetAllEnemies(Vector3 position)
    {
        List<GameObject > enemies = new List<GameObject>();
        foreach (Collider co in Physics.OverlapSphere(position, AOERadious))
        {
            if (co.gameObject.tag == "Goblin")
            {
                GameObject temp3 = co.gameObject;
                enemies.Add(temp3);
            }
        }
        return enemies;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Goblin")
        Attack();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && !attacked)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastKnownLocation, speed * Time.deltaTime);
            transform.LookAt(lastKnownLocation);
            if (Vector3.Distance(transform.position, lastKnownLocation) < 0.1f)
            {
                Attack();
            }
        }

        if (target != null && !attacked)
        {
            lastKnownLocation = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.LookAt(target.transform.position);
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
            transform.localScale = new Vector3(transform.localScale.x - (animspeed * Time.deltaTime), transform.localScale.y - (animspeed * Time.deltaTime), transform.localScale.z - (animspeed * Time.deltaTime));
            if (transform.localScale.x <= 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }
}
