using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropertyChoose : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        MenuUIManager.menuUI.propertyIndex = Convert.ToInt32(gameObject.name.Substring(gameObject.name.Length - 2, 1));
        JsonSave.json.data.index[MenuUIManager.menuUI.menuIndex] = MenuUIManager.menuUI.propertyIndex;
        if (MenuUIManager.menuUI.menuIndex >= 2)
        {
            for (int i = 0; i < JsonSave.json.player.transform.GetChild(MenuUIManager.menuUI.menuIndex).childCount; i++)
            {
                JsonSave.json.player.transform.GetChild(MenuUIManager.menuUI.menuIndex).GetChild(i).gameObject.SetActive(false);
            }
            JsonSave.json.player.transform.GetChild(MenuUIManager.menuUI.menuIndex).GetChild(MenuUIManager.menuUI.propertyIndex).gameObject.SetActive(true);
        }
        else if(MenuUIManager.menuUI.menuIndex == 0)
        {
            JsonSave.json.player.transform.GetChild(0).GetComponent<Renderer>().material = JsonSave.json.colors[MenuUIManager.menuUI.propertyIndex];
        }
        else if (MenuUIManager.menuUI.menuIndex == 1)
        {
            JsonSave.json.player.transform.GetChild(1).GetComponent<Renderer>().material = JsonSave.json.hairColors[MenuUIManager.menuUI.propertyIndex];
        }
        //SaveManager.Save(JsonSave.json.data);
    }
}
