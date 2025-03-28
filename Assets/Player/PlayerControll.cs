using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    Touch touch;
    [SerializeField] float dragBeginTime = 0.3f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] public List<BuildState> BuildStates;

    float coldDawn = 0;
    int currentBuildNumber = 0;

    public Vector3 targetPosition { get; private set; }

    /// </summary>
    // Start is called before the first frame update
    private void Awake()
    {
        coldDawn = dragBeginTime;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        CharacterMove();

        //建設機能が閉まっている
        if (FindFirstObjectByType<TerraControll>() == null) return;

        //今マウス座標に対応する地面を捜す
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseRayEnd = mouseRay.GetPoint(100);
        Debug.DrawRay(mouseRay.origin, mouseRayEnd, Color.red, 3f);
        RaycastHit hit = new RaycastHit();

        //探した結果を判断する
        if (Physics.Raycast(mouseRay.origin, mouseRayEnd, out hit))
        {

            if (FindAnyObjectByType<TerraControll>() && hit.collider?.gameObject.layer == 6)
            {

                targetPosition = hit.point;
                if(!FindFirstObjectByType<TerraControll>().HintAppend(targetPosition, BuildStates[currentBuildNumber])) return;
                if (Input.GetMouseButtonDown(0))
                {

                    Build();
                    Debug.Log(targetPosition);
                }
                return;
            }



        }


        // 地面ではない場合­
        if (hit.collider?.gameObject == gameObject)
        {
            Debug.Log(gameObject.name);
        }
    }

    private void DragRelease()
    {
        throw new NotImplementedException();
    }

    private void Dragging()
    {
        throw new NotImplementedException();
    }

    private void DragStart()
    {
        throw new NotImplementedException();
    }



    void CharacterMove()
    {

#if UNITY_EDITOR
        Vector3 pos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (pos != Vector3.zero)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = (pos.x < 0);

            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", true);

            GetComponent<Rigidbody>().velocity = pos * moveSpeed;
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", false);
            GetComponent<Rigidbody>().velocity *= 0.1f;
        }
        targetPosition = transform.position;
#endif
        if (Input.touchCount > 0)
        {

            if (coldDawn < 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {

                    DragStart();

                }
                if (touch.phase == TouchPhase.Moved)
                {

                    Dragging();

                }
                if (touch.phase == TouchPhase.Ended)
                {

                    DragRelease();

                }
            }
            else
            {
                coldDawn -= Time.deltaTime;
            }

        }

    }

    /////////////////////
    ////     関数　　  ///
    /////////////////////

    /// <summary>
    /// 建物建て
    /// </summary>
    public void Build()
    {

        FindAnyObjectByType<TerraControll>().BuildAtPosition(targetPosition, BuildStates[currentBuildNumber]);
    }
    public void ChangeBuildPlan(float moveVector)
    {
        int newBuildPlan=(int)(currentBuildNumber + moveVector);
        if (newBuildPlan > BuildStates.Count - 1)
        {
            newBuildPlan = 0;
        }
        currentBuildNumber = newBuildPlan;
        GameObject.Find("BuildList").GetComponent<Image>().sprite=BuildStates[currentBuildNumber].buildTexture;

    }

}
