using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusp : MonoBehaviour
{
    Text text, holimoli;
    Status st;
    GameObject dipanionobject;

    Image dipanionviewimage;
    // Start is called before the first frame update
    void Start()
    {
        dipanionobject = GameObject.Find("Dipanion");
        st = dipanionobject.GetComponent<Status>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        statusupdate();
        gameObject.transform.GetChild(9).GetComponent<Button>().onClick.AddListener(() => gameObject.SetActive(false));
    }
    private void statusupdate()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = st.staout.ToString();//체력
        gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = st.dexout.ToString();//민첩
        gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = st.menout.ToString();//정신력
        gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = st.intout.ToString();//지능
        gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = st.strout.ToString();//힘
        gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = "상태 : " + "피.곤.해";
        gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = "기분 : " + "살.려.줘";
        gameObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = "공복도 : " + st.satietyout.ToString();
        gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = st.levelout.ToString();//level

        Sprite dipanionsprite = dipanionobject.gameObject.GetComponent<SpriteRenderer>().sprite;
        gameObject.transform.GetChild(10).gameObject.GetComponent<Image>().sprite = dipanionsprite;
    }
}
