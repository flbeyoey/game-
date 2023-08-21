
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class role : MonoBehaviour
{
    public float moveSpeed = 4f; // 控制移动速度

    public bool isOpen;
    public bool isAttack;
    public bool isWater;

    public Transform raycastOrigin; // 2D对象，从它发射射线
    public LayerMask targetLayer;   // 目标实体所在的图层
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
        if (opendoor == 1 && Time.time - starttime > 5.0f)//开门时间
        {
            door.SetActive(false);
        }
        if(Vector3.Distance(door.transform.position,transform.position) < 5.0f)//开门距离
        { 
            if (Input.GetKeyDown(KeyCode.E))//反复按会重置
            {
                isOpen = true;
                manager.GetComponent<datalibrary>().DisturbMonster();
                opendoor = 1;
                starttime = Time.time;
            }
        }

        if(isWater)//触发水
        {
            manager.GetComponent<datalibrary>().DisturbMonster();//惊动怪物
        }

        if (Input.GetMouseButtonDown(0) && shootnumber > 0 && Time.time - shootime > 2.0f)//间隔
        {
            shootime = Time.time;
            shootnumber--;
            manager.GetComponent<datalibrary>().DisturbMonster();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, mousePosition - (Vector2)raycastOrigin.position, Mathf.Infinity, targetLayer);
            if (hit.collider != null)
            {
                isAttack = true;
                Debug.Log("击中了：" + hit.collider.gameObject.name);
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
