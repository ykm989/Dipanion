using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class allofmousec : MonoBehaviour
{
    float maxdisteance = 15f;
    Vector3 mouseposition;
    int openrmenuflag;

    void Start()
    {
        openrmenuflag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        controlrmenu();
        controlhpbar();
    }
    private void controlrmenu()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouseposition = Input.mousePosition;
            mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);

            RaycastHit2D hit = Physics2D.Raycast(mouseposition, transform.forward, maxdisteance);
            if (hit)
            {
                GameObject rmenu = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
                rmenu.SetActive(true);
                rmenu.transform.position = Camera.main.WorldToScreenPoint(mouseposition + new Vector3(0.45f, -0.6f, 0));
                openrmenuflag = 1;
            }
        }
        if (Input.GetMouseButtonDown(0) && openrmenuflag == 1)
        {
            GameObject rmenu = GameObject.Find("rmenu");
            GameObject statuspannel = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
            if (!statuspannel.activeSelf) rmenu.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => statuspannel.SetActive(true));
            else rmenu.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => statuspannel.SetActive(false));
            openrmenuflag = 0;
        }
        if (Input.GetMouseButtonUp(0)) GameObject.Find("rmenu").SetActive(false);
    }
    private void controlhpbar()
    {
        if (Input.GetKeyDown(KeyCode.Q)) GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.W)) GameObject.Find("hpbag").SetActive(false);
    }
}
