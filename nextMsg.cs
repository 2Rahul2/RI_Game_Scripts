using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class nextMsg : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] myDialogue;
    private int countDialogue , count=0;
    public TextMeshProUGUI DialogueTextBar;
    void Start()
    {
        countDialogue = myDialogue.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)){
            if(count<countDialogue){
                DialogueTextBar.text = myDialogue[count];
                count++;
            }else{
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
