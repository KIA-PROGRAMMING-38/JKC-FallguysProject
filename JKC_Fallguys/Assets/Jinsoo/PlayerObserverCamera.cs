using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserverCamera : MonoBehaviour
{
    private Transform _observerTransform;

    private void Awake()
    {
        _observerTransform = transform.Find("ObserverView").GetComponent<Transform>();
        Debug.Assert(_observerTransform != null);
    }

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
