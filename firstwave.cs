using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstwave : MonoBehaviour
{
    public int building;  

    public GameObject laser1;
    public GameObject laser2;
    public GameObject laser3;

    void Update()
    {
        if(building >= 21)
        {
            Destroy(laser1);
            Destroy(laser2);
            Destroy(laser3);  
        }
    }
}
