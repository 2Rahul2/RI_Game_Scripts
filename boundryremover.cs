using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundryremover : MonoBehaviour
{
    public int num_oppo;
    public GameObject boundry;
    // Start is called before the first frame update
    void Start()
    {
        num_oppo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(num_oppo >= 110)
        {
            Destroy(boundry);
        }
    }
}
