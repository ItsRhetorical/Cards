using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadCards : MonoBehaviour {

    public TextAsset GameAsset;
    public GameObject CardPrefab;
    public GameObject Deck;
    public List<Card> Cards = new List<Card>();

    Dictionary<string, string> obj;

    List<string> ValidCardComponenets = new List<string>(new string[] { "id", "title", "attack", "cost", "body", "flavor" }); 

    // Use this for initialization
    void Awake () {
        GetCards();
	}

    private void Start()
    {
    }

    public void GetCards()
    {
        //get the xml file
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(GameAsset.text);
        //get a list of nodes <card/>
        XmlNodeList xmlCardList = xmlDoc.GetElementsByTagName("card");
        foreach (XmlNode xmlCard in xmlCardList)
        {
            obj = new Dictionary<string, string>();
            //for each <card/> node create a list of nodes beneath it and look throught hem all
            XmlNodeList cardContent = xmlCard.ChildNodes;
            foreach (XmlNode cardPart in cardContent)
            {
                //if our node isn't in the predefined list write to log (it's bad data)
                if(!ValidCardComponenets.Contains(cardPart.Name))
                    Debug.Log("Bad XML");

                //Add our card data to a dictionary with the node name and value
                obj.Add(cardPart.Name, cardPart.InnerText);
                //Debug.Log(cardPart.Name + " - " + cardPart.InnerText);
            }

            GameObject CardObject = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Card cardComponent = CardObject.GetComponent<Card>();
            CardObject.name = obj["id"];
            CardObject.transform.SetParent(Deck.transform);
            
            cardComponent.id = obj["id"];
            cardComponent.title = obj["title"];
            cardComponent.attack = obj["attack"];
            cardComponent.cost = obj["cost"];
            cardComponent.body = obj["body"];
            cardComponent.flavor = obj["flavor"];
            cardComponent.UpdateCardText();
            
            Cards.Add(cardComponent);
        }

    }
}
