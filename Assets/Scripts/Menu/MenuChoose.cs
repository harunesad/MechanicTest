using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuChoose : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        MenuUIManager.menuUI.panels[MenuUIManager.menuUI.menuIndex].SetActive(false);
        MenuUIManager.menuUI.menuIndex = Convert.ToInt32(gameObject.name.Substring(gameObject.name.Length - 1));
        MenuUIManager.menuUI.panels[MenuUIManager.menuUI.menuIndex].SetActive(true);
    }
}
