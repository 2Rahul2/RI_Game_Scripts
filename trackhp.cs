using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackhp : MonoBehaviour
{
    public buildings build;

    public bool numm;

    private void Start() {
        numm = false;
        //build = GetComponent<buildings>();
    }
    void Update()
    {
        if(build._buildhealth <= 0 ){
            numm = true;
            Destroy(gameObject);
        }
    }
}