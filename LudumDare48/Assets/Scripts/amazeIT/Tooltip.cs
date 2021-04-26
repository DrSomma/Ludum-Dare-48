using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text EffectText;
    [SerializeField] private Text CostText;

    public String Effect;
    public int Cost;

    void Start()
    {
        EffectText.text = Effect;
        CostText.text = $"{Cost}$";
    }
}
