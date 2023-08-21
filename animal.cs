using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal : MonoBehaviour
{
    public Transform pointA;//巡逻地点


    public Transform pointB;


    public float speed = 2.0f;
    private Vector3 targetPosition;
    public int isborthed=0;
    public GameObject Role;
    public GameObject manager;//管理总体空对象

    private void Start()
    {
        targetPosition = pointA.position;
    }

    private void Update()
    {
        if (Vector3.Distance(Role.transform.position, transform.position) < 8.0f)
        {
            isborthed = 1;
            //manager.GetComponent<datalibrary>().

        }

        if (Vector3.Distance(Role.transform.position,transform.position) < 2.0f)//攻击距离
        {
            Role.SetActive(false);
            //manager.GetComponent<datalibrary>().

        }
        if (isborthed > 0)
        {
            targetPosition = Role.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                if (targetPosition == pointA.position)
                    targetPosition = pointB.position;
                else
                    targetPosition = pointA.position;
            }
        }
    }
}
