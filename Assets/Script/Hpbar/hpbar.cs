using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UGUI의 기능을 스트립트에서 제하기 위해 필요한 네임스페이스

public class hpbar : MonoBehaviour
{
    private float lerpSpeed;
    Status st;
    private Image content;
    GameObject dipanion;

    void Start()
    {
        content = GetComponent<Image>();
        dipanion = GameObject.Find("Dipanion");
        st = dipanion.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (st.chafilhp != content.fillAmount) content.fillAmount = Mathf.Lerp(st.chafilhp, content.fillAmount, Time.deltaTime * lerpSpeed);//선형보간법으로 값 수정을 부드럽게 하고자0 할 때 사용
    }
}
