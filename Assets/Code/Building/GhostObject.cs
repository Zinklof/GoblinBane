using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int price = 100;
    [Header("References")]
    [SerializeField] GameObject placedObject;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] Collider islandCollider;
    [Header("debug Variables")]
    [SerializeField] RaycastHit hit;
    [SerializeField] Vector3 position;
    [SerializeField] bool building = false;

    private int layer = 7;
    private int layermask;

    private void Start()
    {
        layermask = 1 << layer;

        GameObject temp = GameObject.FindGameObjectWithTag("Island");
        islandCollider = temp.GetComponent<Collider>();
    }

    public void AttemptToBuild()
    {
        bool temp = moneyManager.SpendMoney(price);

        if (temp)
        {
            building = true;
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
        if(building)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 500000f, layermask))
            {
                if (hit.collider == islandCollider)
                {
                    transform.position = hit.point;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(placedObject, transform.position, transform.rotation);
                building = false;
                transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
