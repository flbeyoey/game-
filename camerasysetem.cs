using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerasystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTarget;
    public float movetime;

    private void LateUpdate()//��update֮��ʵ�ֹ���
    {
        if (playerTarget != null)
        {
            if (playerTarget.position != transform.position)
            {
                transform.position = Vector3.Lerp(transform.position, playerTarget.position, movetime * Time.deltaTime);
                //A+(B-A)*time

            }
        }
    }
}