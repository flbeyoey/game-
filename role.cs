
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class role : MonoBehaviour
{
    public float moveSpeed = 4f; // �����ƶ��ٶ�

    public bool isOpen;
    public bool isAttack;
    public bool isWater;

    public Transform raycastOrigin; // 2D���󣬴�����������
    public LayerMask targetLayer;   // Ŀ��ʵ�����ڵ�ͼ��
    public GameObject manager ;

    public GameObject door;

    int opendoor = 0;
    float starttime;
    int shootnumber = 8;

    float shootime;



    void Start()
    {
        isWater = false;
        isOpen = false;
        starttime = Time.time;
    }

    void Update()
    {
        //Debug.Log(Time.time - starttime);
        if (opendoor == 1 && Time.time - starttime > 5.0f)//����ʱ��
        {
            door.SetActive(false);
        }
        if(Vector3.Distance(door.transform.position,transform.position) < 5.0f)//���ž���
        { 
            if (Input.GetKeyDown(KeyCode.E))//������������
            {
                isOpen = true;
                manager.GetComponent<datalibrary>().DisturbMonster();
                opendoor = 1;
                starttime = Time.time;
            }
        }

        if(isWater)//����ˮ
        {
            manager.GetComponent<datalibrary>().DisturbMonster();//��������
        }

        if (Input.GetMouseButtonDown(0) && shootnumber > 0 && Time.time - shootime > 2.0f)//���
        {
            shootime = Time.time;
            shootnumber--;
            manager.GetComponent<datalibrary>().DisturbMonster();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, mousePosition - (Vector2)raycastOrigin.position, Mathf.Infinity, targetLayer);
            if (hit.collider != null)
            {
                isAttack = true;
                Debug.Log("�����ˣ�" + hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "enemy")
                {
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
        //move
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput < 0) horizontalInput = horizontalInput * horizontalInput * horizontalInput;
        else horizontalInput = horizontalInput * horizontalInput * horizontalInput;

        if (verticalInput < 0) verticalInput = verticalInput * verticalInput * verticalInput;
        else verticalInput = verticalInput * verticalInput * verticalInput;

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (moveDirection != Vector2.zero)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {

            isWater = true;

            Debug.Log("okok");
        }
        else
        {


            isWater = false;


        }
    }
}
