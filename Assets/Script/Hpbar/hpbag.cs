using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpbag : MonoBehaviour
{
    GameObject dipanion;
    float head = 1.2f;
    void Start()
    {
        dipanion = GameObject.Find("Dipanion");
        this.gameObject.SetActive(false);
    }
  
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(new Vector3(dipanion.transform.position.x, dipanion.transform.position.y + head, 0));
    }
}