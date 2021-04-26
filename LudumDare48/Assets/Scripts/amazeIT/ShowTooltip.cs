using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject ToolTip;

    private bool mouseOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
            if (!mouseOver && ToolTip != null)
            {
                mouseOver = true;
                ToolTip.SetActive(true);
            }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseOver && ToolTip != null)
        {
            ToolTip.SetActive(false);
            mouseOver = false;
        }
    }
}
