using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusp : MonoBehaviour
{
    Text text, holimoli;
    Status st;
    // Start is called before the first frame update
    void Start()
    {
        st = GameObject.Find("Dipanion").GetComponent<Status>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        statusupdate();
        gameObject.transform.GetChild(9).GetComponent<Button>().onClick.AddListener(() => gameObject.SetActive(false));
    }
    private void statusupdate()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "체력 : " + st.staout.ToString();
        gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "민첩 : " + st.dexout.ToString();
        gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "정신력 : " + st.menout.ToString();
        gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "지능 : " + st.intout.ToString();
        gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = "힘 : " + st.strout.ToString();
        gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = "상태 : " + "피.곤.해";
        gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = "기분 : " + "살.려.줘";
        gameObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = "공복도 : " + "5";
        gameObject.transform.GetChild(8).gameObject.GetComponent<Text>().text = "경험치 : " + "※";
    }
}
