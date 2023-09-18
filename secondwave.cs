using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondwave : MonoBehaviour
{
    public int building;

    public GameObject laser1;
    public GameObject laser2;
    public GameObject laser3;

    void Update()
    {
        if(building >= 42)
        {
            Destroy(laser2);
            Destroy(laser2);
            Destroy(laser3);
        }
    }
}
