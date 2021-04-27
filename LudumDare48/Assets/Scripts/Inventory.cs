using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int money; //items or whatever! eventsystem is there

    private void Start()
    {
        GameEvents.Instance.onTileIsMined += this.OnTileIsMined;
    }

    public void OnTileIsMined(int treasure, Vector3 pos) //money, item id or ...
    {
        //get the itmes 
        money += treasure;

        //update UI
        GameEvents.Instance.InventoryUpdate();
    }

    internal void RemoveMoney(int price)
    {
        //get the itmes 
        money -= price;

        //update UI
        GameEvents.Instance.InventoryUpdate();
    }
}
