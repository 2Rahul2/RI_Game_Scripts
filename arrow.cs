using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class arrow : MonoBehaviour
{
    public GameObject target , textD;
    private RectTransform rt;
    private float distance;
    public Transform player; 
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position , target.transform.position);
        Vector3 objscreenpos = Camera.main.WorldToScreenPoint(target.transform.position);

        Vector3 dir = (objscreenpos - rt.position).normalized;

        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir , Vector3.up));
        
        Vector3 cross = Vector3.Cross(dir , Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle;
        rt.localEulerAngles = new Vector3(rt.localEulerAngles.x , rt.localEulerAngles.y , angle);
        textD.GetComponent<TextMeshProUGUI>().text = "Objective "+ distance.ToString("0")+"M";
    }
}
