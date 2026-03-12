using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float speed =5f;
    [SerializeField]float laneDistance=1f;
    int currentLane=0;
    int totalLanes=5;
    float targetX=0f;
    Rigidbody Rb;
    
    void Start()
    {
        Rb=GetComponent<Rigidbody>();
        Rb.freezeRotation=true;
        currentLane=totalLanes/2;

    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLane++;
        }
        currentLane=Mathf.Clamp(currentLane,0,totalLanes-1);
        targetX = (currentLane-(totalLanes/2))*laneDistance;
        
    }
    void FixedUpdate()
    {
        Vector3 targetPosition=new Vector3(targetX,Rb.position.y,Rb.position.z);
        Rb.MovePosition(Vector3.MoveTowards(Rb.position,targetPosition,speed*Time.fixedDeltaTime));
    }
}
