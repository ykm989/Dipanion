using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float xmoved = 0f;//x축 이동방향
    public float ymoved = 0f;//y축 이동방향
    public float speed = 10f;//이동속도
    private Vector3 direction = Vector3.zero;
    void Start()
    {
        //xmoved = Random.Range(-1.0f, 1.0f);
        //ymoved = Random.Range(-1.0f, 1.0f);

    }


    void move()
    {
        Vector3 direction = Vector3.zero;
        direction.x = xmoved;
        direction.y = ymoved;

    }
    // Update is called once per frame
    void Update()
    {
        Move();
        outcheck();
    }
    void outcheck()//화면 밖 나갔는지 체크
    {
        Vector3 vp = Camera.main.WorldToViewportPoint(transform.position);
        int rr = 0;
        if (vp.x < 0f)//왼쪽 화면 밖
        {
            xmoved *= -1f;
            ymoved = Random.Range(-1.0f, 1.0f);
        }
        else if (vp.x > 1f)//우측 화면 밖
        {
            xmoved *= -1f;
            ymoved = Random.Range(-1.0f, 1.0f);
        }
        else if (vp.y < 0f)//하단 화면 밖
        {
            xmoved = Random.Range(-1.0f, 1.0f);
            ymoved *= -1f;
        }
        else if (vp.y > 1f)
        {
            xmoved = Random.Range(-1.0f, 1.0f);
            ymoved *= -1f;
        }
    }
    void Move()//이동 함수
    {
        transform.position += direction * speed * Time.deltaTime;
        //transform.Translate(direction * speed * Time.deltaTime,Space.World);
    }
}
