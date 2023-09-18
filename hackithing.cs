using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hackithing : MonoBehaviour
{
    public int startangle;
    public int endangle;
    public GameObject[] lineparticle;

    public int boolindex;
    public int boolindexdupi;
    public controlhack checktrue;
    // Start is called before the first frame update
    void Start()
    {
        boolindexdupi = checktrue.counts.Length;
        for(int i = 0 ; i < lineparticle.Length ; i++)
        {
            lineparticle[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyUp(KeyCode.L))
        // {
        //     transform.rotation = Quaternion.Euler(0 , 0 , 90+startangle);
        //     startangle += 90;
        // }

        if(checktrue.counts[boolindex] == true)
        {
            if(startangle > 360)
            {
                transform.rotation = Quaternion.Euler(0 , 0 , 90);
                startangle = 90;
            }

            if(startangle == endangle)
            {
                for(int i = 0 ; i < lineparticle.Length ; i++)
                {
                    lineparticle[i].SetActive(true);
                }
                checktrue.counts[boolindex+1] = true;
            }else{
                for(int i = 0 ; i < lineparticle.Length ; i++)
                {
                    lineparticle[i].SetActive(false);
                }
                for(int i = boolindex+1 ; i< checktrue.counts.Length ; i++)
                {
                    //checktrue.counts[boolindex+1] = false;
                    checktrue.counts[i] = false;
                }
                //for(int i = boolindex ; i < checktrue.counts.Length ; i++)
                // if(boolindexdupi == checktrue.counts.Length)
                // {
                //     print("NOne");
                //     boolindexdupi = checktrue.counts.Length;
                // }else{
                //     boolindexdupi ++;
                // }


            }
        }else{
            for(int i = 0 ; i < lineparticle.Length ; i++)
            {
                lineparticle[i].SetActive(false);
            }
        }
        
    }

    public void buttonrotate()
    {
        transform.rotation = Quaternion.Euler(0 , 0 , 90+ startangle);
        startangle += 90;
    }
}
