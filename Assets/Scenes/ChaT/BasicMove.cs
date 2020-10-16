using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
    public float movePower = 1f;//이동할때 가할 힘의 값
    public float jumpPower = 1f;//점프할때 가할 힘의 값
    public float speed = 10f;

    Rigidbody2D rigid; //Rigidbody2D 컴포넌트를 가져와서 상속할 변수?

    Vector3 movement;//
    bool isJumping = false; //점핑 상태에 대한 bool
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();//rigid2D컴포넌트를 가져오기
    }


    void Update()
    {
        
        if (Input.GetButtonDown("Jump"))//GetButton() 메소드를 누르고 있는 상태, GetButtonDown()해당 버튼이 눌릴 때의 상태.GetButtonUP()은 해당이 떨어질때 상태
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        move();
        jump();
    }
    bool OutCheck()//화면 밖으로 나가는지 아닌지 반환
    {
        Vector3 vp = Camera.main.WorldToViewportPoint(transform.position);//현 오브젝트의 좌표를 뷰포트좌표로 치환
        bool tf = false;
        if (vp.x < 0f)//왼쪽 화면 밖으로 나갈 시
        {
            vp.x = 0f;
            tf = true;
        }
        if (vp.x > 1f)//우측 화면 밖으로 나갈 시
        {
            vp.x = 0f;
            tf = true;
        }
        if (vp.y > 1f)//상단 화면 밖으로 나갈 시
        {
            vp.y = 0f;
            tf = true;
        }
        if (vp.y < 0f)//하단 화면 밖으로 나갈 시
        {
            vp.y = 0f;
            tf = true;
        }
        
        return tf;
    }
    void move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0) // 0보다 작으면 좌측 키 AxisRaw는 인트 Axis는 float 좌우 키보드 감지
        {
            moveVelocity = Vector3.left; //좌측 이동
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) // 0보다 크면 우측 키
        {
            moveVelocity = Vector3.right;//우측 이동
        }
        transform.position += speed * moveVelocity * movePower * Time.deltaTime;//transform.Translate(Vector3.right(이동방향) * 속도 * 변위값 * Time.deltaTime, 기준좌표(World는 절대 Self 상대)
    }/*
      * Time.deltaTime은 초당 프레임을 계산값과 평균치를 반환 하여 컴퓨터 성능에 따라 속도 차이나는것을 방지함 20프레임 컴퓨터는 1/20, 10프레임 컴퓨터는 1/10 이런 느낌으로
      */

    void jump()
    {
        if (!isJumping) return;
        rigid.velocity = Vector2.zero;//Rigidbody2D.velocity = new Vector2(xSpeed,ySpeed);
        //velocity는 rigidbody의 속도를 나타냄, 값을 지정하면 오브젝트의 질량과 상관없이 일정 속도를 줌

        Vector2 JumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(JumpVelocity, ForceMode2D.Impulse);
        //rigidbody에 힘을 가해 가속도를 줌 F=ma공식이 적용rigidbody2d.AddForce(vector * speed);

        isJumping = false;//점프 상태를 false로 돌려서 다시 점프 막기
    }
}