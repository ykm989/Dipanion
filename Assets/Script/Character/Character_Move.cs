using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour
{
    public float speed; //이동 속도
    public float direction; //이동 방향
    private float random; //초기 방향 결정
    private Animator anim; //애니메이터를 불러오기 위한 변수

    void Start()
    {
        speed = 1.0f;
        anim = GetComponent<Animator>();
        random = Random.Range(-1.0f, 1.0f);
        if(random > 0f)
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
        anim.SetFloat("MoveX", direction); //트렌지션에 MoveX라는 변수에 direction값을 보냄.
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