using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public float Hardness = 1;
    public float Cooldown = 2f;
    public float ResetTime = 4f;
    private float curHardness;

    private float _nextDigPossibleTimeStamp;
    private float _resetTimeStamp;


    // Start is called before the first frame update
    void Start()
    {
        curHardness = Hardness;
        _nextDigPossibleTimeStamp = float.MinValue;
    }

    public void OnDig(float digDamage, float digSpeed)
    {
        float curTime = Time.time;
        if(curTime >= _nextDigPossibleTimeStamp)
        {
            //TODO: Coole Effekte!

            //can dig
            curHardness -= digDamage;
            if(curHardness <= 0)
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
        if(Time.deltaTime >= _resetTimeStamp)
        {
            curHardness = Hardness;
        }
    }

    public void DestroyTile()
    {
        //Gib Items!
        //TODO: Coole Effekte!
        Destroy(this.gameObject);
    }
}
