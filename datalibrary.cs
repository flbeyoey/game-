using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class datalibrary : MonoBehaviour
{
    public float shoot = 0;
    public Transform RolePlace;
    public GameObject[] animals;//С�ּ���

    public void DisturbMonster()
    {
        foreach (var animal in animals)
        {
            if (Vector3.Distance(animal.transform.position, RolePlace.position) < 15.0f)//������Χ
            {
                animal.GetComponent<animal>().isborthed = 1;
            }
        }
    }
    public void Result(int option)//���
    {
        
    }
}
