using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    private const string Format = "Money: {0:000}$";
    public Text LblMoney;
    public Inventory Inventory;

    private void Start()
    {
        GameEvents.Instance.onInventoryUpdate += this.UpdateUi;
    }

    public void UpdateUi()
    {
        LblMoney.text = string.Format(Format, Inventory.money);
    }
}
