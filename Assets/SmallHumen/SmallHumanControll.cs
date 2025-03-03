using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SmallHumanControll : MonoBehaviour
{
    [SerializeField] float moveSpeed=0.07f;
    [SerializeField] float findDistance = 0.5f;
    [SerializeField] GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goal == null) return;

        if (math.abs(Vector3.Distance(goal.transform.position, transform.position))>findDistance)
        {
           
            GetComponent<Rigidbody>().velocity= (goal.transform.position-transform.position).normalized* moveSpeed;



        }
    }
}
