using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float xmoved = 0f;//x축 이동방향
    public float ymoved = 0f;//y축 이동방향
    public float speed = 100f;//이동속도
    private Vector3 direction = Vector3.zero;
    void Start()
    {
        move_direction();

    }


    void move_direction()
    {
        direction.x = Random.Range(-1.0f, 1.0f);
        direction.y = Random.Range(-1.0f, 1.0f);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        //move_direction();
        outcheck();
    }
    void outcheck()//화면 밖 나갔는지 체크
    {
        Vector3 vp = Camera.main.WorldToViewportPoint(this.transform.position);
        if (vp.x < 0f + 0.001)//왼쪽 화면 밖
        {
            direction.x *= -1f;
            direction.y = Random.Range(-1.0f, 1.0f);
        }
        else if (vp.x > 1f - 0.01f)//우측 화면 밖
        {
            direction.x *= -1f;
            direction.y = Random.Range(-1.0f, 1.0f);
        }
        else if (vp.y < 0f + 0.01f)//하단 화면 밖
        {
            direction.x = Random.Range(-1.0f, 1.0f);
            direction.y *= -1f;
        }
        else if (vp.y > 1f - 0.01f)
        {
            direction.x = Random.Range(-1.0f, 1.0f);
            direction.y *= -1f;
        }
    }
    void Move()//이동 함수
    {
        //transform.position = direction;
        
        this.transform.Translate(direction  * Time.deltaTime * speed);
        //Debug.Log(direction);
    }
}
