using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaGoblinMusicManager : MonoBehaviour
{

    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip m_Clip;
    [SerializeField] AudioClip m_ClipLooped;

    private void Awake()
    {
        m_AudioSource.clip = m_Clip;
        m_AudioSource.Play();
        m_AudioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AudioSource.isPlaying == false)
        {
            m_AudioSource.clip = m_ClipLooped;
            m_AudioSource.Play();
            m_AudioSource.loop = true;
        }
    }
}
