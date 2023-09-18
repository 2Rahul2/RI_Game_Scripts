using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldImage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(new Vector3(Player.position.x , transform.position.y , Player.position.z));

    }
}
