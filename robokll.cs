using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class robokll : MonoBehaviour
{
    public int numofbomber;
    public Text bombtext;
    void Update()
    {
        bombtext.text = numofbomber.ToString();
    }
}
