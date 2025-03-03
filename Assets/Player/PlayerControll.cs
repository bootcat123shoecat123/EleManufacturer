using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    Touch touch;
    [SerializeField]float dragBeginTime = 0.3f;
    [SerializeField] float moveSpeed = 1f;
    float coldDawn = 0;

    // Start is called before the first frame update
    private void Awake()
    {
            coldDawn=dragBeginTime;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        CharacterMove();
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

        }
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
    public void Build()
    {

        FindAnyObjectByType<TerraControll>().BuildAtPosition(transform.position);
    }
    
}
