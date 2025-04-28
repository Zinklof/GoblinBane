using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatRotation : MonoBehaviour
{
    [SerializeField] float maxChange;
    [SerializeField] float maxChangePerTick;

    [SerializeField] Vector2 currentRotation;
    [SerializeField] Vector2 rotation;

    [SerializeField] float tickrate;

    [SerializeField] bool DoThisScript;

    private void Start()
    {
        StartCoroutine(Rotator());
    }

    private IEnumerator Rotator()
    {
        while (DoThisScript = true)
        {
            rotation.x += Random.Range(-maxChangePerTick, maxChangePerTick);
            rotation.y += Random.Range(-maxChangePerTick, maxChangePerTick);

            rotation.x = Mathf.Clamp(rotation.x, -maxChange, maxChange);
            rotation.y = Mathf.Clamp(rotation.y, -maxChange, maxChange);

            yield return new WaitForSeconds(tickrate);
        }
    }

    private void Update()
    {
        currentRotation.x = Mathf.Lerp(currentRotation.x, rotation.x, 0.0025f);

        currentRotation.y = Mathf.Lerp(currentRotation.y, rotation.y, 0.0025f);

        Vector3 finalRoation = new Vector3(currentRotation.x, transform.rotation.y, currentRotation.y);

        transform.rotation = Quaternion.Euler(finalRoation);
    }
}
