using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class act : MonoBehaviour
{
    [Header("������Ϊ���")]
    public bool isAttack;//�Ƿ�ǹ
    public bool isMove;//�Ƿ��ƶ�



    [Header("�������")]
    public float maxHP;
    public float HP;
    public role usingrole;

    [Header("���xianggaun")]
    public float playerMoveSpeed;
    public float playerJumpSpeed;
    public bool isLive;


    [SerializeField] GameObject projectile;
    [Header("���")]
    public BoxCollider2D playerColl;
    public Rigidbody2D playerRB;
    public bool isHit;
    public Vector2 direction;
    public Animator Playeranim;//����

    public int a;
    // Start is called before the first frame update
    void Start()
    {
        playerColl = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        Playeranim = GetComponent<Animator>();//����Ϸ�����л�ȡ���
        a = 0;
        isAttack = false;
        isLive = true;

        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        playerAttack();
        EnemyDie();

    }

    void PlayerMove()
    {
        float HorizontalNumx = Input.GetAxis("Horizontal");//��ȡ��������
        float HorizontalNumy = Input.GetAxis("Vertical");//��ȡ��������
        float faceNUMx = Input.GetAxisRaw("Horizontal");
        float faceNUMy = Input.GetAxisRaw("Vertical");
        playerRB.velocity = new Vector2(playerMoveSpeed * HorizontalNumx,playerMoveSpeed * HorizontalNumy);

        playerRB.velocity = new Vector2(playerRB.velocity.x,playerMoveSpeed * HorizontalNumy);
        if (HorizontalNumx != 0)
            Playeranim.SetFloat("run", Mathf.Abs(playerMoveSpeed * HorizontalNumx));
        else
            Playeranim.SetFloat("run", Mathf.Abs(playerMoveSpeed * HorizontalNumy));
        if (faceNUMx != 0)
        {
            transform.localScale = new Vector3(-faceNUMx, transform.localScale.y, transform.localScale.z);//��ά���ݣ�����ת��y,z��̶�

        }

        if(HorizontalNumx != 0 && HorizontalNumy != 0)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }

    }

    void playerAttack()
    {
        if (usingrole.isAttack)
        {
            isAttack = true;
            Playeranim.SetBool("attack", true);
            Instantiate(projectile, transform.position, Quaternion.identity);
            a  = 0;
            usingrole.isAttack = false;

        }
        else
        {
            Invoke("fff", 0.2f);
            //isAttack = false;
            
        }
    }

    void fff()
    {
        Playeranim.SetBool("attack", false);
    }



    public void GetBossHit(Vector2 direction)
    {
        if (!isHit)
        {
            //
            //transform.localScale = new Vector3(direction.x, 1, 1);
            isHit = true;
            this.direction = -direction;
            //if(usingplayer2.shanghai == 10)
            HP = 0;// usingplayer2.shanghai;
            Playeranim.SetTrigger("hit");
        }
    }

 
    void EnemyDie()
    {
        if (HP <= 0)
        {
            Playeranim.SetTrigger("die");
            isLive = false;
            Destroy(gameObject);
        }
    }
}
