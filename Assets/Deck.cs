using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour,IPointerClickHandler {

    public List<Card> CardDeck = new List<Card>();
    public Transform Hand;
    public GameObject CardPrefab;


    // Use this for initialization
    void Start () {
        Debug.Log("Test");
        CardDeck = GameObject.Find("Main Camera").GetComponent<LoadCards>().Cards;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        CardFromDeckTo(Hand);
    }

    public void CardFromDeckTo(Transform Location, int index = 0)
    {
        if (index >= CardDeck.Count)
            return;
        CardDeck[index].transform.SetParent(Location);
        

        CardDeck.RemoveAt(index);
    }
}
