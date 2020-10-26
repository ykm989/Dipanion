﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UGUI의 기능을 스트립트에서 제하기 위해 필요한 네임스페이스

public class hpbar : MonoBehaviour
{
    private float lerpSpeed;
    Status st;
    private Image content;
    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
        GameObject dipanion = GameObject.Find("Dipanion");
        st = dipanion.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (st.chafilhp != content.fillAmount)
        {
            //Debug.Log("현재 피 : " + currenthpget + "fill : " + chafilhp);
            content.fillAmount = Mathf.Lerp(st.chafilhp, content.fillAmount, Time.deltaTime * lerpSpeed);//선형보간법으로 값 수정을 부드럽게 하고자 할 때 사용

        }
    }
}