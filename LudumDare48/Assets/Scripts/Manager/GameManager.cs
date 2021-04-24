using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        #region SINGLETON PATTERN

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject(name: "GameManager");
                        _instance = container.AddComponent<GameManager>();
                    }
                }

                return _instance;
            }
        }

        #endregion

        [Header(header: "Settings")]
        public int startMoney = 1000;
        public int Money { get; private set; }
        public GameObject moneyTextPrefab;
        public GameObject canvas;

        // public delegate void MoneyChanged(int money, int sumToAdd);

        // public event MoneyChanged OnMoneyChanged;

        // private int _nextObjectId;


        private void Awake()
        {
            // _gridByTile = new Dictionary<KeyValuePair<int, int>, WorldTileClass>();
            ChangeMoney(sumToAdd: startMoney, pos: Vector3.zero);

            // Grid to make visible the border of playing field
            // if (!drawDebugLine)
            // {
            //     // return;
            // }
            //
            // Debug.DrawLine(
            //     start: new Vector3(x: 0, y: 0),
            //     end: new Vector3(x: 0, y: height),
            //     color: Color.white,
            //     duration: 100f);
            // Debug.DrawLine(
            //     start: new Vector3(x: 0, y: 0),
            //     end: new Vector3(x: width, y: 0),
            //     color: Color.white,
            //     duration: 100f);
            // Debug.DrawLine(
            //     start: new Vector3(x: 0, y: height),
            //     end: new Vector3(x: width, y: height),
            //     color: Color.white,
            //     duration: 100f);
            // Debug.DrawLine(
            //     start: new Vector3(x: width, y: 0),
            //     end: new Vector3(x: width, y: height),
            //     color: Color.white,
            //     duration: 100f);
        }

        public void ChangeMoney(int sumToAdd, Vector3 pos)
        {
            GameObject temp = Instantiate(original: moneyTextPrefab, parent: canvas.transform, worldPositionStays: true);
            // temp.GetComponent<MoneyText>().Init(money: sumToAdd);
            // System.Diagnostics.Debug.Assert(condition: Camera.main != null, message: "Camera.main != null");
            // temp.transform.position = Camera.main.WorldToScreenPoint(position: pos);


            ChangeMoney(sumToAdd: sumToAdd);
        }

        private void ChangeMoney(int sumToAdd)
        {
            // if (sumToAdd > 0 || Money >= sumToAdd)
            // {
            //     Money += sumToAdd;
            //     OnMoneyChanged?.Invoke(money: Money, sumToAdd: sumToAdd);
            // }
        }

        // private bool IsValidField(int x, int y)
        // {
        // return x >= 0 && y >= 0 && x < width && y < height;
        // }
    }
}