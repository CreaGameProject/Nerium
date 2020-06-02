﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        var cameraTf = GameObject.FindWithTag("MainCamera").transform;
        cameraTf.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
