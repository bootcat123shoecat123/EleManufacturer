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
    GameObject buildPrefab;
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
    public void BuildAtPosition(Vector3 originPos, BuildState state)
    {
        //get build position at map
        int posX = Math.Clamp(AssetConfig.CheseConfig.col / 2 + Mathf.FloorToInt(originPos.x / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.col - 1);
        int posY = Math.Clamp(AssetConfig.CheseConfig.row / 2 - 1 + Mathf.FloorToInt(originPos.z / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.row - 1);
        
        //ここは建物いますが
        Vector2 checkPos = new Vector2();
        for (int i = 0; i < state.size.y; i++) 
        {
            //Y向座標取り
            checkPos.y = posY + i;
            for (int j = 0; j < state.size.x; j++)
            {
                //X向座標取り
                checkPos.x = posX + j;
                
                //建物いる場合
                if (currentBuildings[(int)checkPos.y][(int)checkPos.x] != null) return;
            }
        }

        //建物を生成
        GameObject newBuild = Instantiate(buildPrefab, new Vector3(AssetConfig.CheseConfig.ceilSize * ((posX - AssetConfig.CheseConfig.col / 2)), 0, AssetConfig.CheseConfig.ceilSize * ((posY - AssetConfig.CheseConfig.row / 2))), new Quaternion());
        newBuild.GetComponent<BuildControll>().state = state;
        newBuild.GetComponent<BuildControll>().InitBuild();

        //mapping to 建築マップ
        for (int i = 0; i < state.size.y; i++)
        {
            checkPos.y = posY + i;
            for (int j = 0; j < state.size.x; j++)
            {
                checkPos.x = posX + j;


                //GetComponent<SpriteRenderer>().material.SetVector("_size", new Vector2(AssetConfig.CheseConfig.col, AssetConfig.CheseConfig.row));
                currentBuildings[(int)checkPos.y][(int)checkPos.x] = newBuild.GetComponent<BuildControll>();

            }
        }

        //このシステムを閉める
        buildHint.SetActive(false);
        gameObject.SetActive(false);

    }

    /// <summary>
    /// 建てるつもりの場所でヒント（hint）を表す
    /// </summary>
    /// <param name="originPos">建てる座標</param>
    /// <param name="build">建てる建物</param>
    public bool HintAppend(Vector3 originPos, BuildState state)
    {

        if (!buildHint.activeSelf) buildHint.SetActive(true);

        //対応の座標を取る
        int posX = Math.Clamp(AssetConfig.CheseConfig.col / 2 + Mathf.FloorToInt(originPos.x / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.col - 1);
        int posY = Math.Clamp(AssetConfig.CheseConfig.row / 2 - 1 + Mathf.FloorToInt(originPos.z / AssetConfig.CheseConfig.ceilSize), 0, AssetConfig.CheseConfig.row - 1);
        buildHint.transform.position =new Vector3(AssetConfig.CheseConfig.ceilSize * ((posX - AssetConfig.CheseConfig.col / 2)), 0, AssetConfig.CheseConfig.ceilSize * ((posY - AssetConfig.CheseConfig.row / 2)));

        //check postion doesn't have any build
        Vector2 checkPos = new Vector2();
        for (int i = 0; i < state.size.y; i++)
        {
            checkPos.y = posY + i;
            for (int j = 0; j < state.size.x; j++)
            {
                checkPos.x = posX + j;

                if (currentBuildings[(int)checkPos.y][(int)checkPos.x] != null) {
                    buildHint.GetComponent<SpriteRenderer>().material.SetVector("_size", state.size);
                 
                    buildHint.transform.localScale=new Vector3( state.size.x, state.size.y, 1);
                    buildHint.GetComponent<SpriteRenderer>().material.SetColor("_InsideColor",unBuildColor);
                    return false;
                } 
            }
        }

        buildHint.GetComponent<SpriteRenderer>().material.SetVector("_size", state.size);

        buildHint.transform.localScale= new Vector3(state.size.x, state.size.y, 1);
        buildHint.GetComponent<SpriteRenderer>().material.SetColor("_InsideColor", canBuildColor);
        return true;
    }

}
