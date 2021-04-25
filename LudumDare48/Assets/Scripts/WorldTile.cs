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
    private Color _imgColor;
    private Sprite _imgSprite; 

    private static WorldTile _curDiggingTile;

    // Start is called before the first frame update
    void Start()
    {
        curHardness = Hardness;
        _nextDigPossibleTimeStamp = float.MinValue;

        var render = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _imgColor = render.color;
        _imgSprite = render.sprite;

        //Tween
        _wait = new WaitForSeconds(waitTime);
        Transform _imgTrans = transform.GetChild(0);
        _effect = new EffectBuilder(this)
            //.AddEffect(new ScaleEffect(_imgTrans, maxScaleSize, scaleSpeed, _wait))
            .AddEffect(new ShakeEffect(_imgTrans, maxRotation, rotSpeed));
    }

    public void DigMe(float digDamage, float digSpeed)
    {
        float curTime = Time.time;
        if(_curDiggingTile == null || _curDiggingTile != this)
        {
            StartDigging();
        }

        if (curTime >= _nextDigPossibleTimeStamp)
        {
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

    private void StartDigging()
    {
        //reset last
        if(_curDiggingTile != null)
        {
            _curDiggingTile.StopDigging();
        }

        _curDiggingTile = this;

        //effect
        _effect.ExecuteEffects();

        //Particle
        ParticleManager.Instance.SpawnDiggingParticels(transform.position, _imgColor, _imgSprite);

    }

    public static void OnStopDigging()
    {
        if (_curDiggingTile != null)
        {
            _curDiggingTile.StopDigging();
        }
    }

    private void StopDigging()
    {
        _curDiggingTile = null;
        curHardness = Hardness;
        _effect.StopAllEffects();
        ParticleManager.Instance.StopDiggingParticels();

       
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

        ParticleManager.Instance.StopDiggingParticels();

        //TODO: Coole Effekte!
        Destroy(this.gameObject);
    }
}
