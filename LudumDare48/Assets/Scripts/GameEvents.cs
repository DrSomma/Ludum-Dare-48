using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action<int,Vector3> onTileIsMined; //int = money / item id ... can be changed
    public void TileIsMined(int i,Vector3 pos) //money, item id, or ...
    {
        if(onTileIsMined != null)
        {
            onTileIsMined(i,pos);
        }
    }

    public event Action onInventoryUpdate;
    public void InventoryUpdate() //money, item id, or ...
    {
        if (onInventoryUpdate != null)
        {
            onInventoryUpdate();
        }
    }

    public event Action onUpgrade;
    public void Upgrade()
    {
        if (onUpgrade != null)
        {
            onUpgrade();
        }
    }
}
