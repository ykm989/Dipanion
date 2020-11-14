﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testtext : MonoBehaviour
{
    public Text testText;
    public GameObject dipanion;
    public GameObject hpbar;
    new Camera camera;
    
    void Start()
    {
        testText = GetComponent<Text>();
        dipanion = GameObject.Find("Dipanion");
        hpbar = GameObject.Find("hpbag");
        testText.text = "패널 좌표 : " + this.transform.position;
        camera = FindObjectOfType<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        testText.text = "패널 좌표 : " + this.transform.position + "\n마우스 좌표 : " + Input.mousePosition + "\n오브젝트 좌표 : " + (dipanion.transform.position) + "\nHP바 좌표 : " + hpbar.transform.position + "\nTrue 여부 : " + (dipanion.transform.position == Input.mousePosition);
    }
}
