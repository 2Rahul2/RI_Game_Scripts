using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class basedest : MonoBehaviour
{
    public Text builddes;
    public Text wavetext;

    public int numbuild;
    public int numofop;

    private void Update() {
        if(numbuild < 21)
        {          
            builddes.text = numbuild.ToString() +"/21" ;
        }
        else
        {    
            builddes.text = numbuild.ToString() + "/42";
        }


        if(numofop < 110)
        {
            wavetext.text = "First_Wave";
        }
        else
        {
            wavetext.text = "Second_Wave";
        }
        
    }

}
