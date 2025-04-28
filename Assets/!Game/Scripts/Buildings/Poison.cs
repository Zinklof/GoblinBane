using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Poison : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float AOERadious;
    [SerializeField] float attackspeedsecconds;

    [SerializeField] float attackdelay;
    List<GameObject> AOETargets = new List<GameObject>();

    private List<GameObject> GetAllEnemies(Vector3 position)
    {
        List<GameObject> enemies = new List<GameObject>();
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

    private void Attack()
    {
        AOETargets.Clear();
        AOETargets = GetAllEnemies(gameObject.transform.position);

        foreach (GameObject t in AOETargets)
        {
            Grunt temp = t.GetComponent<Grunt>();
            if (temp == null)
            {
                return;
            }
            temp.DamageGoblin(damage);
        }
    }

    private void Update()
    {
        attackdelay += Time.deltaTime;

        if (attackdelay > attackspeedsecconds)
        { 
            Attack();
            attackdelay = 0;
        }
    }
}
