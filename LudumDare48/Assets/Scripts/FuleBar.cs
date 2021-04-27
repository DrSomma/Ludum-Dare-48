using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FuleBar : MonoBehaviour
{
    public GameObject FuleBarPrefab; 
    public PlayerMovement player;
    private List<Image> _bars;



    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            Debug.LogError("Player nicht gesetzt");
        }

        _bars = new List<Image>();
        for (int i = 0; i < UpgradeManager.Instance.TankUpgrade+1; i++)
        {
            AddBar();
        }
        //_bars = new Image[transform.childCount];
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    _bars[i] = transform.GetChild(i).GetComponent<Image>();
        //}
        GameEvents.Instance.onUpgrade += OnUpgrade;
    }

    private void OnUpgrade()
    {
        if((UpgradeManager.Instance.TankUpgrade + 1) > _bars.Count)
            AddBar();
    }

    private void AddBar()
    {
        var newBar = Instantiate(FuleBarPrefab);
        newBar.transform.SetParent(this.transform);
        _bars.Add(newBar.GetComponentsInChildren<Image>().Last());
        player.Refuel();
    }

    void UpdateUi()
    {
        //25 / 20 = 1.2
        float barI = player.Fuel / UpgradeManager.Instance.TankPerLevel;
        for (int i = 0; i < _bars.Count; i++)
        {
            _bars[i].fillAmount = barI;
            barI -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUi();
    }
}
