using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public string id;
    public string title;
    public string attack;
    public string cost;
    public string body;
    public string flavor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateCardText()
    {
        this.transform.GetChild(0).GetComponent<Text>().text = title;
        this.transform.GetChild(1).GetComponent<Text>().text = attack;
        this.transform.GetChild(2).GetComponent<Text>().text = cost;
        this.transform.GetChild(3).GetComponent<Text>().text = body;
        this.transform.GetChild(4).GetComponent<Text>().text = flavor;
    }
}
