using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomswitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;

    public GameObject[] rooms;
    List<string> roomNames = new List<string>();

    private void Start() {
        roomNames.Add("room1");
        roomNames.Add("room2");
        roomNames.Add("room3");
        roomNames.Add("room4");

    }
    private void OnTriggerEnter(Collider other) {
        // if(other.gameObject.tag == "room2")
        // {
        //     for(int i = 0 ; i <3 ;i ++)
        //     {
        //         if(rooms[i].name == "room2"){
        //             rooms[i].SetActive(true);
        //             rooms[i-1].SetActive(false);
        //             rooms[i+1].SetActive(true);
        //         }
        //     }
        // }

        for(int j = 0 ;j < 4 ; j++)
        {
            if(other.gameObject.tag == roomNames[j])
            {
                for(int i = 0 ; i <3 ;i ++)
                {
                    if(rooms[i].name == roomNames[j])
                    {
                        rooms[i].SetActive(true);
                        rooms[i-1].SetActive(false);
                        rooms[i+1].SetActive(true);
                    }
                } 
            }
        }

    }
}
