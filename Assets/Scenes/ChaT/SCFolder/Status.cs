using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public GameObject prfHpbar;
    public GameObject canvas;

    RectTransform hpBar;

    public float height = 1.7f;

    private float chamaxhp;//캐릭터 HP 최대치
    private float chacurhp;//캐릭터 HP 현재
    private float chafilhp;//캐릭터 HP MAX/Current 계산을 저장하기 위한 변수
    public float currenthpget//hp값 캡슐화
    {
        get//변수 = Status.currenthpget
        {
            return chacurhp;
        }
        set//Status.currenthpget = 값//0미만이나 max값 넘어서 부르는거 거루기 위함
        {
            if (value < 0) chacurhp = 0;
            else if (value > chamaxhp) chacurhp = chamaxhp;
            else chacurhp = value;
        }
    }
    private int str, dex, ints, sta, men;//힘,민첩,지능,체력,정신력
    private int valagg, valact, valmor, valmen;//공격성 수치, 행동성 수치, 도덕성 수치, 멘탈 수치

    public void valper(int val, char a)//수치 값 변경 함수
    {
        if (a == 'g')//공격성
        {
            if ((valagg + val) > 100) valagg = 100;
            else if ((valagg + val) < 0) valagg = 0;
            else valagg = val;
        }
        else if (a == 't')//행동성
        {
            if ((valact + val) > 100) valact = 100;
            else if ((valact + val) < 0) valact = 0;
            else valact = val;
        }
        else if (a == 'r')//도덕성
        {
            if ((valmor + val) > 100) valmor = 100;
            else if ((valmor + val) < 0) valmor = 0;
            else valmor = val;
        }
        else if (a == 'n')//멘탈
        {
            if ((valmen + val) > 100) valmen = 100;
            else if ((valmen + val) < 0) valmen = 0;
            else valmen = val;
        }
    }

    void Start()
    {
        hpBar = Instantiate(prfHpbar, canvas.transform).GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
    }
}
