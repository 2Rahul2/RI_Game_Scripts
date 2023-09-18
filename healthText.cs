using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthText : MonoBehaviour
{
    public Text hp;

    private void Start() {
        hp.text = "lol";
    }
    public void sethealth(float playerheal)
    {
        hp.text = playerheal.ToString();
    }
}
