using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UGUI의 기능을 스트립트에서 제하기 위해 필요한 네임스페이스

public class Status : MonoBehaviour
{
    //private float lerpSpeed = 0.05f;

    private Image content;

    private int satiety = 0;
    private float chamaxhp;//캐릭터 HP 최대치
    private float chacurhp;//캐릭터 HP 현재
    public float chafilhp;//캐릭터 HP MAX/Current 계산을 저장하기 위한 변수
    public float currenthpget//hp값 캡슐화
    {
        get//변수 = Status.currenthpget
        {
            return chacurhp;
        }
        set//Status.currenthpget = 값//0미만이나 max값 넘어서 부르는거 거루기 위함
        {
            if ((chacurhp + value) < 0) chacurhp = 0;
            else if ((chacurhp + value) > chamaxhp) chacurhp = chamaxhp;
            else chacurhp += value;

            chafilhp = chacurhp / chamaxhp;
        }
    }
    private int str, dex, ints, sta, men;//힘,민첩,지능,체력,정신력
    private int valagg, valact, valmor, valmen;//공격성 수치, 행동성 수치, 도덕성 수치, 멘탈 수치
    private int level, exp;
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
    public int strout
    {
        get { return str; }
        set { str += value; }
    }
    public int satietyout
    {
        get { return satiety; }
        set { satiety = satiety + value; }
    }
    public int dexout
    {
        get { return dex;}
        set { dex += value; }
    }
    public int intout
    {
        get { return ints; }
        set { ints += value; }
    }
    public int staout
    {
        get { return sta; }
        set { sta += value; }
    }
    public int menout
    {
        get { return men; }
        set { men += value; }
    }
    public int levelout
    {
        get { return level; }
        set { level += value; }
    }
    public int aggout
    {
        get { return valagg; }
        set { valagg += value; }
    }
    public int actout
    {
        get { return valact; }
        set { valact += value; }
    }
    public int morout
    {
        get { return valmor; }
        set { valmor += value; }
    }

    void Start()
    {
        Initialize(100, 100);//처음 체력을 설정
    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) currenthpget = -10;//테스트를 위해 누르면 피 달게 설정
        if (Input.GetKeyDown(KeyCode.O)) currenthpget = 10;//테스트를 위해 누르면 피 차게 설정
    }
    
    public void Initialize(float currentValue, float maxValue)//HP 설정
    {
        chamaxhp = maxValue;
        currenthpget = currentValue;
        chafilhp = chacurhp / chamaxhp;

        str = 10;
        dex = 11;
        ints = 12;
        sta = 13;
        men = 14;
        satiety = 15;
        level = 1;
    }
}
