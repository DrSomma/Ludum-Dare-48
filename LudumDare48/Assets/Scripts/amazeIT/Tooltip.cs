using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [Serializable]
    public enum Upgrade {Tank,Sight,Speed};
    public Upgrade _thisUpgrade;
    public int level;

    [SerializeField] private Text EffectText;
    [SerializeField] private Text CostText;

    public String Effect;
    public int Cost;

    void Start()
    {
        switch (_thisUpgrade)
        {
            case Upgrade.Tank:
                EffectText.text = "Tank +1";
                CostText.text = $"{UpgradeManager.Instance.TankPrice * level}$";
                break;
            case Upgrade.Sight:
                EffectText.text = "Sight +30%";
                CostText.text = $"{UpgradeManager.Instance.SightPrice * level}$";
                break;
            case Upgrade.Speed:
                EffectText.text = "Seed +2";
                CostText.text = $"{UpgradeManager.Instance.SpeedPrice * level}$";
                break;
            default:
                break;
        }
    }
}
