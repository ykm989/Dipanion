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

    //Vector3 head = new Vector3(0, 1.5f, 0);

    bool showhpbar = true;
    //Camera.main.WorldToViewportPoint(dipanion.transform.position);

    void Start()
    {
        content = GetComponent<Image>();
        dipanion = GameObject.Find("Dipanion");
        st = dipanion.GetComponent<Status>();
        //hpbag = GameObject.Find("hpbag");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (showhpbar)
        {
            this.transform.position = (dipanion.transform.position + head);
            //hpbag.transfrom.position = (dipanion.transform.position + head);
        }*/
        if (st.chafilhp != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(st.chafilhp, content.fillAmount, Time.deltaTime * lerpSpeed);//선형보간법으로 값 수정을 부드럽게 하고자 할 때 사용

        }
    }
}
