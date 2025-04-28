using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuMusic : MonoBehaviour 
{ 
    [SerializeField] AudioSource musicSource;
    void Update()
    {
        if (Time.timeSinceLevelLoad > 3.5f)
        {
            musicSource.Play();
            musicSource.loop = true;
            Destroy(gameObject);
        }
    }
}
