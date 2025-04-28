using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int price = 100;
    [Header("References")]
    [SerializeField] GameObject placedObject;
    [SerializeField] Collider islandCollider;
    [SerializeField] AudioSource audioManager;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip fail;
    [SerializeField] AudioClip placed;
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
        //bool temp = MoneyManager.SpendMoney(price);

        building = true;

        /*
        if (temp)
        {
            audioManager.clip = success;
            audioManager.Play();
            building = true;
        }
        else
        {
            audioManager.clip = fail;
            audioManager.Play();
            return;
        }
        */
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

            if (Input.GetMouseButtonDown(0) && transform.position.y > 0.2)
            {
                bool temp = MoneyManager.SpendMoney(price);

                if (temp)
                {
                    audioManager.clip = placed;
                    audioManager.Play();
                    Instantiate(placedObject, transform.position, transform.rotation);
                    building = false;
                    transform.position = new Vector3(0, 0, 0);
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        AttemptToBuild();
                    }
                }
                else
                {
                    audioManager.clip = fail;
                    audioManager.Play();
                    building = false;
                    transform.position = new Vector3(0, 0, 0);
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        AttemptToBuild();
                    }
                    return;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                transform.position = new Vector3(0, 0, 0);
                building = false;
            }
        }
    }
}
