using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    public GameObject MoneyTextPrefab;
    public Text LblMoney;
    public Inventory Inventory;
    private GameObject canvas;

    private void Start()
    {
        canvas = this.gameObject;
        GameEvents.Instance.onInventoryUpdate += this.UpdateUi;
        GameEvents.Instance.onTileIsMined += this.ChangeMoney;
    }

    public void UpdateUi()
    {
        LblMoney.text = $"Money {Inventory.money}$";
    }

    public void ChangeMoney(int sumToAdd, Vector3 pos)
    {
        if (pos != null)
        {
            GameObject temp = Instantiate(MoneyTextPrefab);
            temp.transform.SetParent(canvas.transform);
            temp.GetComponent<MoneyText>().Init(sumToAdd);
            temp.transform.position = Camera.main.WorldToScreenPoint(pos);
        }
    }
}
