using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerGenaration_Layer_0 : LayerGenaration
{
    public WorldTile Weed;

    public override void GenerateLayer(int startY, int seed = -1)
    {
        base.GenerateLayer(startY);

        //grass on y=0
        for (int x = 0; x < mapSizeX; x++)
        {
            ReplaceTile(Weed.gameObject,x,0);
        }
    }


}
