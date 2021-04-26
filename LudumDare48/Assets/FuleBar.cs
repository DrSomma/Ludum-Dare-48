using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuleBar : MonoBehaviour
{
    [Range(0,1)]
    public float Fule;
    private Image _barFill;

    // Start is called before the first frame update
    void Start()
    {
        _barFill = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _barFill.fillAmount = Fule;
    }
}
