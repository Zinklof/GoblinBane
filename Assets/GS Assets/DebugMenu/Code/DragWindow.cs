using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform TopLevelRectTransform;
    [SerializeField] Canvas canvas;

    private void Start()
    {
        GameObject canvasObject = GameObject.Find("Canvas");
        canvas = canvasObject.GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        TopLevelRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
