﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Character_Move : MonoBehaviour
{
    public float speed; //이동 속도
    public float direction; //이동 방향을 트랜지션으로 보내는 용도.
    private float ran1; //초기 방향 결정
    private float ran2; //이동을 할지 말지
    private float action = 1.0f; //움직일지 말지를 트랜지션으로 보내는 용도.
    private int cnt = 0; //최소한의 행동후 방향 변경
    private Animator anim; //애니메이터를 불러오기 위한 변수
    Status status; //Status.cs 스크립트에 변수들을 가져오기 위함.
    int agg; //공격성
    int act; //행동성
    int mor; //도덕성
    int men; //멘탈

    void Start()
    {
        speed = 1.0f;

        status = GameObject.Find("Dipanion").GetComponent<Status>();

        agg = status.aggout; //공격성
<<<<<<< Updated upstream
=======
        agg = 7; //임시
>>>>>>> Stashed changes
        act = status.actout; //행동성
        mor = status.morout; //도덕성
        men = status.menout; //멘탈
        //status.menout = 6;

        anim = GetComponent<Animator>();
        ran1 = Random.Range(-1.0f, 1.0f);
        if(ran1 > 0f)
        {
            direction = 1f; //오른쪽
        }
        else
        {
            direction = -1f; //왼쪽
        }
        
    }

    void Update()
    {
        cnt++;
        if (cnt > 300) //Update 함수가 300번 호출되면 방향을 방향을 바꿀지 말지 한번 돌려봄.
        {
            ran2 = Random.Range(1f, 10f);
            if (ran2 <= agg) //예를들어 agg가 7이면 1~10중 뽑는 ran2가 7보다 아하일 경우(즉 70%) 움직임.
            {
                ran1 = Random.Range(-1.0f, 1.0f);
                if (ran1 > 0f)
                {
                    direction = 1f; //오른쪽
                }
                else
                {
                    direction = -1f; //왼쪽
                }
                speed = 1.0f;
                action = 1.0f;
            }
            else
            {
                speed = 0.0f;
                action = -1.0f;
            }

            cnt = 0;
        }

        anim.SetFloat("MoveX", direction); //트렌지션에 MoveX라는 변수에 direction값을 보냄.
        anim.SetFloat("Action", action);
<<<<<<< Updated upstream

=======
        ///////////////////////////////////////////////
        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("Eat", 1);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("Eat", -1);
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Eat"))
        {
            speed = 0.0f;
            anim.SetFloat("Action", -1.0f);
        }
        if (action > 0)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Move_R Anim"))
            {
                speed = 1.0f;
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Move_L Anim"))
            {
                speed = 1.0f;
            }
        }
        /////////////////////////////////////////////////
>>>>>>> Stashed changes
        transform.Translate(new Vector3(direction * speed * Time.deltaTime, 0f, 0f)); //움직이기

        Vector3 vp = Camera.main.WorldToViewportPoint(this.transform.position);
        if (vp.x < 0) //좌측 화면 밖
        {
            direction = direction * (-1);
        }
        else if (vp.x > 1) //우측 화면 밖
        {
            direction = direction * (-1);
        }
    }
}