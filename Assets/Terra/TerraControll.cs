using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TerraControll : MonoBehaviour
{
    // Start is called before the first frame update\
    List<List<GameObject>> Buildings;
    [SerializeField] List<GameObject> Builds;
    void Start()
    {

        transform.localScale = new Vector3(AssetConfig.CheseConfig.ceilSize * AssetConfig.CheseConfig.col, AssetConfig.CheseConfig.ceilSize * AssetConfig.CheseConfig.row,1);
        Buildings=new List<List<GameObject>>();
        for (int row = 0;row< AssetConfig.CheseConfig.row; row++)
        {
            Buildings.Add(new List<GameObject>());
            for (int col = 0; col < AssetConfig.CheseConfig.col; col++)
            {
                Buildings[row].Add(null);
            }
        }
        GetComponent<SpriteRenderer>().material.SetVector("_size", new Vector2( AssetConfig.CheseConfig.col,AssetConfig.CheseConfig.row));
    }
    public void BuildAtPosition(Vector3 position)
    {

       int posX = Math.Clamp(AssetConfig.CheseConfig.col/2+ Mathf.FloorToInt( position.x / AssetConfig.CheseConfig.ceilSize),0,AssetConfig.CheseConfig.col-1);
        int posY = Math.Clamp(AssetConfig.CheseConfig.row/2-1+ Mathf.FloorToInt(position.z / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.row-1);
        if (Buildings[posY][posX]==null)
        {
            GameObject newBuild=Instantiate(Builds[0],new Vector3(AssetConfig.CheseConfig.ceilSize * ((posX- AssetConfig.CheseConfig.col / 2)), 0, AssetConfig.CheseConfig.ceilSize * ((posY-AssetConfig.CheseConfig.row / 2))),new Quaternion());
            Buildings[posY][posX] = newBuild;
        }
        
        
    }

}
