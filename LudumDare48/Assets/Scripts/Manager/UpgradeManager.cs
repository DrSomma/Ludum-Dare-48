using UnityEngine;

namespace Manager
{
    public class UpgradeManager : MonoBehaviour
    {
        [Header("Upgrades")]
        [SerializeField] private int TankUpgrade;
        [SerializeField] private int SpeedUpgrade;
        [SerializeField] private int SightUpgrade;

        [SerializeField] public GameObject[] TankUpgradeBlocker;
        [SerializeField] public GameObject[] SpeedUpgradeBlocker;
        [SerializeField] public GameObject[] SightUpgradeBlocker;

        private void Start()
        {
            TankUpgrade = 0;
            SpeedUpgrade = 0;
            SightUpgrade = 0;
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
            }
        }
    }
}
