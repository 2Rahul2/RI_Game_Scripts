using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerOneult : MonoBehaviour
{
    private Transform finalpos;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(90,0,0);
        transform.position = new Vector3(transform.position.x ,transform.position.y +15 , transform.position.z);
        // finalpos.position = new Vector3(transform.position.x , transform.position.y +8 , transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this , 5f);
    }
}
