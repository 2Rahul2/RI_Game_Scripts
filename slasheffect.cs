using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class slasheffect : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform currentpos;
    private playerTwo player;
    void Start()
    {
        player = GameObject.Find("player2").GetComponent<playerTwo>();
        if(player.swordcount == 1){
            transform.DORotate(new Vector3(transform.eulerAngles.x  , transform.eulerAngles.y + 160 , transform.eulerAngles.z + 20) , 0.3f);
        }else{
            transform.DORotate(new Vector3(transform.eulerAngles.x  , transform.eulerAngles.y - 160 , transform.eulerAngles.z - 10) , 0.3f);
        }
        
    }
    private IEnumerator des(float wait){
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine("des" , 0.4f);
        // currentpos = GameObject.Find("player2").transform;
        // transform.eulerAngles = new Vector3(transform.eulerAngles.x , currentpos.eulerAngles.y - 160 , transform.eulerAngles.z);
        // // transform.position = new Vector3(transform.position.x , currentpos.position.y , transform.position.z);
        // // print(currentpos.position.y);
        // if(Input.GetKeyUp(KeyCode.C)){
        //     print("isrotate");
        // }
    }
}
