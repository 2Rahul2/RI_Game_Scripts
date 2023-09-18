using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePuzzle : MonoBehaviour
{
    [System.Serializable]
    public struct device{
        public bool ispreviousdevice;
        public GameObject currentDevice;
        public GameObject[] PreviousDevice;

        public float currentAngle;
        public float finalAngle;
        public bool isAligned;
    }
    public device[] mystruct = new device[2];
    public int indexstruct =0;
    private int prevObjIndex=0;
    private bool runonce=true;
    // Start is called before the first frame update
    void Start()
    {
        mystruct[0].currentAngle =50;
    }

    // Update is called once per frame
    void Update()
    {
        if(mystruct[indexstruct].currentAngle == mystruct[indexstruct].finalAngle){
            
            foreach(GameObject PrevDevice in mystruct[indexstruct].PreviousDevice){
                if(PrevDevice.GetComponent<rotatedevice>().aligned == true){
                    prevObjIndex += 1;
                }
            }

        }
        // print(mystruct[0].currentAngle);
    }
}
