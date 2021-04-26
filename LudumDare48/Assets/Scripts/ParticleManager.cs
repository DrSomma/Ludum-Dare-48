using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    public ParticleSystem DiggingParticle;
    private ParticleSystem.MainModule _diggingMain;

    void Awake()
    {
        Instance = this;
        _diggingMain = DiggingParticle.main;
        DiggingParticle.gameObject.SetActive(false);
    }

    public void SpawnDiggingParticels(Vector3 where,Color color,Sprite sprite)
    {
        DiggingParticle.transform.position = where;
        //DiggingParticle.transform.LookAt(player);
        _diggingMain.startColor = color;
        DiggingParticle.gameObject.SetActive(true);
    }

    internal void StopDiggingParticels()
    {
        DiggingParticle.gameObject.SetActive(false);
    }
}
