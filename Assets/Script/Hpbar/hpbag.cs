using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpbag : MonoBehaviour
{
    private Image content;
    GameObject dipanion;
    bool showhpbar = true;

    Vector3 head = new Vector3(0, 1.5f, 0);
    void Start()
    {
        content = GetComponent<Image>();
        dipanion = GameObject.Find("Dipanion");
    }

    // Update is called once per frame
    void Update()
    {
        if (showhpbar)
        {
            this.transform.position = (dipanion.transform.position + head);
        }
    }
}
