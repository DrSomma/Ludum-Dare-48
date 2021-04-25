using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerGenaration_Layer_0 : LayerGenaration
{
    public WorldTile Fossile;
    public WorldTile Weed;

    public override void GenerateLayer(int seed = -1)
    {
        base.GenerateLayer(seed);

        //grass on y=0
        for (int x = 0; x < mapSizeX; x++)
        {
            ReplaceTile(Weed.gameObject,x,0);
        }

        for (int i = 0; i < Random.Range(0,3); i++)
        {
            ReplaceRandomTile(Fossile.gameObject);
        }
    }

    private void ReplaceRandomTile(GameObject replace)
    {
        GetRandomTile(out int x, out int y);
        ReplaceTile(replace, x, y);
    }

    private void ReplaceTile(GameObject replace, int x, int y)
    {
        GameObject replaceTile = layerMap[x, y].gameObject;
        GameObject replaceBy = Instantiate(replace);
        replaceBy.transform.SetParent(this.transform);
        replaceBy.transform.position = replaceTile.transform.position;
        Destroy(replaceTile);
        layerMap[x, y] = replaceBy.GetComponent<WorldTile>();
    }
}
