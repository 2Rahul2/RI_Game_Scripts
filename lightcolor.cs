using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcolor : MonoBehaviour
{
    public Color[] colors;
    public float duration , transitionspeed;
    private Light light;
    public int ind1 , ind2;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start() {
        light = GetComponent<Light>();
    }
    void Update()
    {
        duration += Time.deltaTime/transitionspeed;
        light.color = Color.Lerp(colors[ind1] , colors[ind2] , duration);
        if(duration>=1){
            int num = ind1;
            ind1 =  ind2;
            ind2= num;
            duration =0;
        }
    }
}
