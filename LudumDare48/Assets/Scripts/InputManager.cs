using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeMenu;
    [SerializeField] private GameObject EscapeMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (EscapeMenu.activeSelf)
            {
                return;
            }
            else
            {
                UpgradeMenu.SetActive(!UpgradeMenu.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UpgradeMenu.activeSelf)
            {
                UpgradeMenu.SetActive(false);
            }
            else
            {
                EscapeMenu.SetActive(!EscapeMenu.activeSelf);
            }
        }
    }
}
