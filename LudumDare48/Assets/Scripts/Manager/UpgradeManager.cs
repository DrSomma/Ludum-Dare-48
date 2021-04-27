using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager Instance;

        [Header("Upgrades")]
        public int TankUpgrade;
        public int SpeedUpgrade;
        public int SightUpgrade;

        [Header("Price")]
        public int SpeedPrice = 150;
        public int TankPrice = 250;
        public int SightPrice = 100;

        [Header("Settings")]
        public float TankPerLevel = 20f; 
        public float SpeedPerLevel = 2f;
        public float StartSpeed = 1.5f;
        [Range(0, 1)]
        public float StartSight = 1;
        public float SightPerLevel = 0.3f;

        [SerializeField] public GameObject[] TankUpgradeBlocker;
        [SerializeField] public GameObject[] SpeedUpgradeBlocker;
        [SerializeField] public GameObject[] SightUpgradeBlocker;

        public float Sight => StartSight - (SightPerLevel * (SightUpgrade));
        public float DrillSpeedMultiplier => StartSpeed + (SpeedPerLevel*SpeedUpgrade);
        public float MaxFuel => TankPerLevel + (TankPerLevel * TankUpgrade);


        public Inventory _playerInventory;

        private void Awake()
        {
            Instance = this;
        }

        public void UpgradeTank(int level)
        {
            if (level != TankUpgrade + 1)
            {
                return;
            }
            else
            {
                // Geld überprüfen
                var price = TankPrice * (TankUpgrade+1);
                if (_playerInventory.money >= price)
                {
                    _playerInventory.RemoveMoney(price);
                    // Geld abziehen und upgraden
                    TankUpgradeBlocker[TankUpgrade].GetComponent<Button>().enabled = false;
                    TankUpgradeBlocker[TankUpgrade].GetComponent<Image>().color = new Color(0,0,0,0);
                    TankUpgrade++;

                    GameEvents.Instance.Upgrade();
                }
            }
        }

        public void UpgradeSpeed(int level)
        {
            if (level != SpeedUpgrade + 1)
            {
                return;
            }
            else
            {
                // Geld überprüfen
                var price = SpeedPrice * (SpeedUpgrade+1);
                if (_playerInventory.money >= price)
                {
                    _playerInventory.RemoveMoney(price);
                    // Geld abziehen und upgraden
                    SpeedUpgradeBlocker[SpeedUpgrade].GetComponent<Button>().enabled = false;
                    SpeedUpgradeBlocker[SpeedUpgrade].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    SpeedUpgrade++;

                    GameEvents.Instance.Upgrade();
                }
            }
        }

        public void UpgradeSight(int level)
        {
            if (level != SightUpgrade + 1)
            {
                return;
            }
            else
            {
                // Geld überprüfen
                // Geld abziehen und upgraden
                var price = SightPrice * (SightUpgrade+1);
                if (_playerInventory.money >= price)
                {
                    _playerInventory.RemoveMoney(price);
                    SightUpgradeBlocker[SightUpgrade].GetComponent<Button>().enabled = false;
                    SightUpgradeBlocker[SightUpgrade].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    SightUpgrade++;

                    GameEvents.Instance.Upgrade();
                }
            }
        }
    }
}
