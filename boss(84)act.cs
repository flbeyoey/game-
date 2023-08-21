using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bossact: MonoBehaviour
{

    [Header("״̬���")]
    public bool isSleep;//�ж�bossʯ�񱻾���
    public bool isAttak;//�Ƿ��ֵ���

    public bool aaa;


    [Header("�������")]
    public Transform target;
    public Transform target2;
    public Transform home;
    public act usingact;
    public role usingrole;

    [Header("��ֵ���")]
    public float bossMoveSpeed;
    public float bossJumpSpeed;
    public float bossAttack;
    public float maxHP;
    public float HP;


    [Header("�������")]
    public BoxCollider2D bossColl;
    public Rigidbody2D bossRB;
    public Animator bossAnim;//����
    // Start is called before the first frame update
    void Start() //  ��ʼ��
    {
        isSleep =  true;
        isAttak =  false;
        bossColl = GetComponent<BoxCollider2D>();
        bossRB = GetComponent<Rigidbody2D>();
        bossAnim = GetComponent<Animator>();//����Ϸ�����л�ȡ���
        target = GameObject.FindGameObjectWithTag("Player").transform;
        target2 = GameObject.FindGameObjectWithTag("boom").transform;
        home = GameObject.FindGameObjectWithTag("home").transform;
        aaa = true;

    }

    private void FixedUpdate()
    {
        bossMove();
        bossState();
    }
    // Update is called once per frame
    void Update()
    {
        //bossMove();
       // bossState();
    }


    void bossMove()
    {
        if (!isSleep && aaa)
        {
            target2.position = usingact.transform.position;
            aaa = false;
        }

        if(home.position != transform.position && isSleep)
        {
            bossMoveSpeed = 3;
            transform.position = Vector2.MoveTowards(transform.position, home.position, bossMoveSpeed * Time.deltaTime);
        }

        if(usingrole.isOpen)//����
        {
            bossMoveSpeed = 6;
            transform.position = Vector2.MoveTowards(transform.position, target.position, bossMoveSpeed * Time.deltaTime);
        }

        if (isAttak && !isSleep && usingact.isLive) 
        {
            bossMoveSpeed = 6;
            transform.position = Vector2.MoveTowards(transform.position, target.position, bossMoveSpeed * Time.deltaTime);
            bossAnim.SetFloat("run", Mathf.Abs(bossMoveSpeed));


            if (transform.position.x - target.position.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (transform.position.x - target.position.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if(!isSleep && !isAttak && usingact.isLive)
        {
            bossMoveSpeed = 6;
            transform.position = Vector2.MoveTowards(transform.position, target2.position, bossMoveSpeed * Time.deltaTime);
            bossAnim.SetFloat("run", Mathf.Abs(bossMoveSpeed));
        }
        else
        {
            bossAnim.SetFloat("run", 0);
        }
    }




    void bossState()
    {

        if(usingact.isLive == false && transform.position == home.position)
        {
            isSleep = true;
            
        }


        if(usingact.isLive == false)
        {
            bossMoveSpeed = 3;
            transform.position = Vector2.MoveTowards(transform.position, home.position, bossMoveSpeed * Time.deltaTime);
            isAttak = false;
           
        }

        if(transform.position == target2.position  &&  !isAttak)
        {
            isSleep = true;
        }

        if (usingact.isAttack == true)
        {
            aaa = true;
            isSleep = false;
            usingact.isAttack = false;
        }
        


        else if(Mathf.Abs(transform.position.x - target.position.x) < 10 && Mathf.Abs(transform.position.y - target.position.y)< 10)
        {
            isAttak = true;
        }
        else
        {
            isAttak = false;
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.localScale.x > 0)
                collision.GetComponent<act>().GetBossHit(Vector2.right);
            else if (transform.localScale.x < 0)
                collision.GetComponent<act>().GetBossHit(Vector2.left);


            Debug.Log("okok");
        }
    }


    void pppMove()
    {
        float HorizontalNumx = Input.GetAxis("Horizontal");//��ȡ��������
        float HorizontalNumy = Input.GetAxis("Vertical");
        float faceNUMx = Input.GetAxisRaw("Horizontal");
        float faceNUMy = Input.GetAxisRaw("Vertical");
        bossRB.velocity = new Vector2(bossMoveSpeed * HorizontalNumx, bossMoveSpeed * HorizontalNumy);

        //playerRB.velocity = new Vector2(playerRB.velocity.x,playerMoveSpeed * HorizontalNumy);
        if(HorizontalNumx != 0)
            bossAnim.SetFloat("run", Mathf.Abs(bossMoveSpeed * HorizontalNumx));
        else
            bossAnim.SetFloat("run", Mathf.Abs(bossMoveSpeed * HorizontalNumy));
        if (faceNUMx != 0)
        {
            transform.localScale = new Vector3(-faceNUMx, transform.localScale.y, transform.localScale.z);//��ά���ݣ�����ת��y,z��̶�

        }

    }



}

