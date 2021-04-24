using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public float Hardness = 1;
    private float curHardness;


    // Start is called before the first frame update
    void Start()
    {
        curHardness = Hardness;
    }

    public void OnDig(float damage)
    {
        curHardness -= damage;
        if(curHardness <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("On Destroy");
        }
    }
}
