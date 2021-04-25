using System;
using System.Collections;
using System.Collections.Generic;
using Tween_Library.Scripts;
using Tween_Library.Scripts.Effects;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    [Header("Block Settings")]
    public float Hardness = 1;
    public int Treasure = 1;      //money, item id or ...

    [Header("Tween Settings")]
    public Vector3 maxScaleSize;
    public float scaleSpeed;
    public float rotSpeed;
    public float maxRotation;
    public float waitTime;
    private EffectBuilder _effect;
    private YieldInstruction _wait;

    #region static/config
    [Header("Static Settings")]
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

        //Tween
        _wait = new WaitForSeconds(waitTime);
        Transform _img = transform.GetChild(0);
        _effect = new EffectBuilder(this)
            //.AddEffect(new ScaleEffect(_img, maxScaleSize, scaleSpeed, _wait))
            .AddEffect(new ShakeEffect(_img, maxRotation, rotSpeed));
    }

    public void DigMe(float digDamage, float digSpeed)
    {
        float curTime = Time.time;
        if (curTime >= _nextDigPossibleTimeStamp)
        {
            //effect
            _effect.ExecuteEffects();

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
