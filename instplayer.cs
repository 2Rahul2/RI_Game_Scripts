using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instplayer : MonoBehaviour
{
    public GameObject play;
    void Start()
    {
        Instantiate(play , transform.position , Quaternion.identity);
    }
}
