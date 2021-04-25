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

    public void OnTileIsMined(int treasure) //money, item id or ...
    {
        //get the itmes 
        money += treasure;
    }
}
