using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public float Hardness = 1;
    public int Treasure = 1;      //money, item id or ...

    #region static/config
    public float Cooldown = 2f;
    public float ResetTime = 4f;
    private float curHardness;
    #endregion

    private float _nextDigPossibleTimeStamp;
    private float _resetTimeStamp;


    // Start is called before the first frame update
    void Start()
    {
        curHardness = Hardness;
        _nextDigPossibleTimeStamp = float.MinValue;
    }

    public void DigMe(float digDamage, float digSpeed)
    {
        float curTime = Time.time;
        if (curTime >= _nextDigPossibleTimeStamp)
        {
            //TODO: Coole Effekte!

            //can dig
            curHardness -= digDamage;
            if (curHardness <= 0)
            {
                DestroyTile();
            }
            _nextDigPossibleTimeStamp = curTime + (Cooldown - digSpeed);
            _resetTimeStamp = curTime + ResetTime;
        }
        else
        {
            //On Cooldown
        }
    }

    private void Update()
    {
        if (Time.deltaTime >= _resetTimeStamp)
        {
            curHardness = Hardness;
        }
    }

    public void DestroyTile()
    {
        //get items
        GameEvents.Instance.TileIsMined(Treasure);

        //TODO: Coole Effekte!
        Destroy(this.gameObject);
    }
}
