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

    }

 

    public void eating()
    {
        Debug.Log("꺼억");
        st.satietyout = 10;
    }
    /*public IEnumerable eatingbutton(int eat)
    {
        Debug.Log("먹기 발동");

        yield return new WaitForSeconds(1.0f);

        st.satietyout = eat;
        Debug.Log(st.satietyout);
    }*/

}
