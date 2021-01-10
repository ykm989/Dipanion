using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eatpanel : MonoBehaviour
{
    private bool ButtonDown;
    Status st;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        st = GameObject.Find("Dipanion").GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => st.satietyout = 10);
        //if (SystemInput.GetMouseButtonUp(0)) gameObject.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => eatingbutton(10));

    }

    /*private void eating(int eat)
    {
        Debug.Log("발동");
        st.satietyout = eat;
        float aasdfas = st.satietyout;
        Debug.Log(aasdfas);
    }*/

    public void eating()
    {
        Debug.Log("꺼억");
        st.satietyout = 10;
    }
    public IEnumerable eatingbutton(int eat)
    {
        Debug.Log("먹기 발동");

        yield return new WaitForSeconds(1.0f);

        st.satietyout = eat;
        Debug.Log(st.satietyout);
    }

}
