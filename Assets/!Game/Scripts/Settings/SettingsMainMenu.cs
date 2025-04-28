using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SettingsMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private Volume postProcessing;
    [SerializeField] private bool is3D;
    public bool open;
    public Vector3 hoverScale;
    private Vector3 nonHoverScale = Vector3.one;
    private DepthOfField blur;

    private void Start()
    {
        nonHoverScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        if (is3D && !open)
        {
            gameObject.transform.localScale = hoverScale;
        }
    }

    private void OnMouseExit()
    {
        if (is3D && !open)
        {
            gameObject.transform.localScale = nonHoverScale;
        }
    }

    private void OnMouseDown()
    {
        if (is3D && !open)
        {
            ChangeStatus(true);
        }
    }

    public void ChangeStatus(bool status)
    {
        open = status;
        settings.SetActive(status);
        postProcessing.profile.TryGet(out blur);

        blur.active = status;
    }
}
