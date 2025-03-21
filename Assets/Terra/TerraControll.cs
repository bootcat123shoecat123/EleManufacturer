using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TerraControll : MonoBehaviour
{
    // Start is called before the first frame update\
    ///////////////////
    ///   変数設定　 ///
    //////////////////
    List<List<BuildControll>> currentBuildings;

    [SerializeField]
    GameObject buildHint;
    [SerializeField]
    Color unBuildColor;
    [SerializeField]
    Color canBuildColor;
    void Start()
    {
        InitTerra();


    }

    /////////////////////
    /// 　インストール  ///
    /////////////////////
    void InitTerra()
    {
        //大きさ
transform.localScale = new Vector3(AssetConfig.CheseConfig.ceilSize * AssetConfig.CheseConfig.col, AssetConfig.CheseConfig.ceilSize * AssetConfig.CheseConfig.row, 1);

        //建築マップ生成
        currentBuildings = new List<List<BuildControll>>();
        for (int row = 0; row < AssetConfig.CheseConfig.row; row++)
        {
            currentBuildings.Add(new List<BuildControll>());
            for (int col = 0; col < AssetConfig.CheseConfig.col; col++)
            {
                currentBuildings[row].Add(null);
            }
        }

        //grid
        GetComponent<SpriteRenderer>().material.SetVector("_size", new Vector2(AssetConfig.CheseConfig.col, AssetConfig.CheseConfig.row));
    }
    /////////////////////
    ////     関数　　  ///
    /////////////////////
    /// <summary>
    /// 指定されるのところに建てます
    /// </summary>
    /// <param name="originPos">建て場所</param>
    /// <param name="build">建てたい建物</param>
    public void BuildAtPosition(Vector3 originPos, GameObject build)
    {
        //get build position at map
        int posX = Math.Clamp(AssetConfig.CheseConfig.col / 2 + Mathf.FloorToInt(originPos.x / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.col - 1);
        int posY = Math.Clamp(AssetConfig.CheseConfig.row / 2 - 1 + Mathf.FloorToInt(originPos.z / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.row - 1);
        
        //check postion doesn't have any build
        Vector2 checkPos = new Vector2();
        for (int i = 0; i < build.GetComponent<BuildControll>().size.y; i++)
        {
            checkPos.y = posY + i;
            for (int j = 0; j < build.GetComponent<BuildControll>().size.x; j++)
            {
                checkPos.x = posX + j;

                if (currentBuildings[(int)checkPos.y][(int)checkPos.x] != null) return;
            }
        }

        //建物を生成
        GameObject newBuild = Instantiate(build, new Vector3(AssetConfig.CheseConfig.ceilSize * ((posX - AssetConfig.CheseConfig.col / 2)), 0, AssetConfig.CheseConfig.ceilSize * ((posY - AssetConfig.CheseConfig.row / 2))), new Quaternion());

        //mapping to 建築マップ
        for (int i = 0; i < build.GetComponent<BuildControll>().size.y; i++)
        {
            checkPos.y = posY + i;
            for (int j = 0; j < build.GetComponent<BuildControll>().size.x; j++)
            {
                checkPos.x = posX + j;
                GetComponent<SpriteRenderer>().material.SetVector("_size", new Vector2(AssetConfig.CheseConfig.col, AssetConfig.CheseConfig.row));
                currentBuildings[(int)checkPos.y][(int)checkPos.x] = build.GetComponent<BuildControll>();

            }
        }

        buildHint.SetActive(false);
        gameObject.SetActive(false);

    }
    public bool HintAppend(Vector3 originPos, GameObject build)
    {

        if (!buildHint.activeSelf) buildHint.SetActive(true);
        int posX = Math.Clamp(AssetConfig.CheseConfig.col / 2 + Mathf.FloorToInt(originPos.x / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.col - 1);
        int posY = Math.Clamp(AssetConfig.CheseConfig.row / 2 - 1 + Mathf.FloorToInt(originPos.z / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.row - 1);
        buildHint.transform.position =new Vector3(AssetConfig.CheseConfig.ceilSize * ((posX - AssetConfig.CheseConfig.col / 2)), 0, AssetConfig.CheseConfig.ceilSize * ((posY - AssetConfig.CheseConfig.row / 2)));
        //check postion doesn't have any build
        Vector2 checkPos = new Vector2();
        for (int i = 0; i < build.GetComponent<BuildControll>().size.y; i++)
        {
            checkPos.y = posY + i;
            for (int j = 0; j < build.GetComponent<BuildControll>().size.x; j++)
            {
                checkPos.x = posX + j;

                if (currentBuildings[(int)checkPos.y][(int)checkPos.x] != null) {
                    buildHint.GetComponent<SpriteRenderer>().material.SetVector("_size", build.GetComponent<BuildControll>().size);
                 
                    buildHint.transform.localScale=new Vector3( build.GetComponent<BuildControll>().size.x, build.GetComponent<BuildControll>().size.y, 1);
                    buildHint.GetComponent<SpriteRenderer>().material.SetColor("_InsideColor",unBuildColor);
                    return false;
                } 
            }
        }

        buildHint.GetComponent<SpriteRenderer>().material.SetVector("_size", build.GetComponent<BuildControll>().size);

        buildHint.transform.localScale= new Vector3(build.GetComponent<BuildControll>().size.x, build.GetComponent<BuildControll>().size.y, 1);
        buildHint.GetComponent<SpriteRenderer>().material.SetColor("_InsideColor", canBuildColor);
        return true;
    }

}
