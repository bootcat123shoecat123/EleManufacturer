using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TerraControll : MonoBehaviour
{
    // Start is called before the first frame update\
    
    List<List<BuildControll>> currentBuildings;
    
    void Start()
    {

        transform.localScale = new Vector3(AssetConfig.CheseConfig.ceilSize * AssetConfig.CheseConfig.col, AssetConfig.CheseConfig.ceilSize * AssetConfig.CheseConfig.row,1);
        currentBuildings=new List<List<BuildControll>>();
        for (int row = 0;row< AssetConfig.CheseConfig.row; row++)
        {
            currentBuildings.Add(new List<BuildControll>());
            for (int col = 0; col < AssetConfig.CheseConfig.col; col++)
            {
                currentBuildings[row].Add(null);
            }
        }
        GetComponent<SpriteRenderer>().material.SetVector("_size", new Vector2( AssetConfig.CheseConfig.col,AssetConfig.CheseConfig.row));
    }
    public void BuildAtPosition(Vector3 originPos,GameObject build)
    {
 int posX = Math.Clamp(AssetConfig.CheseConfig.col/2+ Mathf.FloorToInt(originPos.x / AssetConfig.CheseConfig.ceilSize),0,AssetConfig.CheseConfig.col-1);
        int posY = Math.Clamp(AssetConfig.CheseConfig.row/2-1+ Mathf.FloorToInt(originPos.z / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.row-1);
        Vector2 checkPos = new Vector2();
        for (int i = 0; i < build.GetComponent<BuildControll>().size.y; i++) {
            checkPos.y = posY + i;
            for (int j = 0; j < build.GetComponent<BuildControll>().size.x; j++) { 
            checkPos.x = posX + j;
            
            if( currentBuildings[(int)checkPos.y][(int)checkPos.x] != null)return;
            }
        }
        GameObject newBuild=Instantiate(build,new Vector3(AssetConfig.CheseConfig.ceilSize * ((posX- AssetConfig.CheseConfig.col / 2)), 0, AssetConfig.CheseConfig.ceilSize * ((posY-AssetConfig.CheseConfig.row / 2))),new Quaternion());
            
        for (int i = 0; i < build.GetComponent<BuildControll>().size.y; i++)
        {
            checkPos.y = posY + i;
            for (int j = 0; j < build.GetComponent<BuildControll>().size.x; j++)
            {
                checkPos.x = posX + j;

                currentBuildings[(int)checkPos.y][(int)checkPos.x] = build.GetComponent<BuildControll>();
            }
        }
        Debug.Log("");
        
    }

}
