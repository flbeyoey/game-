using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class datalibrary : MonoBehaviour
{
    public float shoot = 0;
    public Transform RolePlace;
    public GameObject[] animals;//小怪集合

    public void DisturbMonster()
    {
        foreach (var animal in animals)
        {
            if (Vector3.Distance(animal.transform.position, RolePlace.position) < 15.0f)//触动范围
            {
                animal.GetComponent<animal>().isborthed = 1;
            }
        }
    }
    public void Result(int option)//结局
    {
        
    }
}
