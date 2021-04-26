using UnityEngine;

namespace Manager
{
    public class UpgradeManager : MonoBehaviour
    {
        public static UpgradeManager Instance;

        [Header("Upgrades")]
        public int TankUpgrade;
        public int SpeedUpgrade;
        public int SightUpgrade;

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

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            //TankUpgrade = 0;
            //SpeedUpgrade = 0;
            //SightUpgrade = 0;
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
                // Geld abziehen und upgraden
                TankUpgradeBlocker[TankUpgrade].  SetActive(false);
                TankUpgrade++;

                GameEvents.Instance.Upgrade();
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
                // Geld abziehen und upgraden

                SpeedUpgradeBlocker[SpeedUpgrade].SetActive(false);
                SpeedUpgrade++;

                GameEvents.Instance.Upgrade();
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

                SightUpgradeBlocker[SightUpgrade].SetActive(false);
                SightUpgrade++;

                GameEvents.Instance.Upgrade();
            }
        }
    }
}
