using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class visual : MonoBehaviour
{

    public VisualEffect vfx;

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Y)){
        // vfx.Stop();
        vfx.Play();
            
        }
    }
}
