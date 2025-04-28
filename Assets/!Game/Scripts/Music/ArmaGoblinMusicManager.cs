using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaGoblinMusicManager : MonoBehaviour
{

    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip m_Clip;
    [SerializeField] AudioClip m_ClipLooped;
    [SerializeField] AudioClip m_ArmaGoblin;
    [SerializeField] AudioClip m_ArmaGoblinLooped;
    [SerializeField] bool armaGoblin = false;

    private void Awake()
    {
        m_AudioSource.clip = m_Clip;
        m_AudioSource.Play();
        m_AudioSource.loop = false;
    }

    public void ChangeToArmaGoblin()
    {
        m_AudioSource.clip = m_ArmaGoblin;
        m_AudioSource.Play();
        m_AudioSource.loop = false;
        armaGoblin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AudioSource.isPlaying == false &&  armaGoblin == false)
        {
            m_AudioSource.clip = m_ClipLooped;
            m_AudioSource.Play();
            m_AudioSource.loop = true;
        }
        if (m_AudioSource.isPlaying == false && armaGoblin == true)
        {
            m_AudioSource.clip = m_ArmaGoblinLooped;
            m_AudioSource.Play();
            m_AudioSource.loop = true;
        }
    }
}
