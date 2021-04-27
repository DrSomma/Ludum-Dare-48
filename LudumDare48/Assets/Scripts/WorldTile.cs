using Manager;
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
    private float curHardness;
    #endregion

    private Color _imgColor;
    private Sprite _imgSprite; 

    private static WorldTile _curDiggingTile;

    // Start is called before the first frame update
    void Start()
    {
        curHardness = GetBaseHarndess();

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

    private float GetBaseHarndess()
    {
        return Hardness /** 1.5f*/;
    }

    public void DigMe(float digMulti)
    {
        if(_curDiggingTile == null || _curDiggingTile != this)
        {
            StartDigging();
        }
     
        var damage = (digMulti / GetBaseHarndess()) * Time.deltaTime;
        curHardness -= damage;
        if (curHardness <= 0)
        {
            DestroyTile();
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

        SoundManager.Instance.PlayDrillSound();
    }

    public static void OnStopDigging()
    {
        if (_curDiggingTile != null)
        {
            _curDiggingTile.StopDigging();
            SoundManager.Instance.StopDrillSound();
        }
    }

    private void StopDigging()
    {
        _curDiggingTile = null;
        curHardness = GetBaseHarndess();
        _effect.StopAllEffects();
        ParticleManager.Instance.StopDiggingParticels();       
    }

    public void DestroyTile()
    {
        //get items
        GameEvents.Instance.TileIsMined(Treasure, transform.position);

        ParticleManager.Instance.StopDiggingParticels();

        SoundManager.Instance.StopDrillSound();
        SoundManager.Instance.PlayBreakSound();

        //TODO: Coole Effekte!
        Destroy(this.gameObject);
    }
}
