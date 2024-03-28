using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using TMPro;

public class ItemManager : MonoBehaviour
{
    Action<string> _createItemsCallback; 

    public void Start()
    {
        _createItemsCallback = (jsonArrayString) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString)); 
        };

        CreateItems(); 
    }
    public void CreateItems()
    {
        string userID = Main.instance.userInfo.UserID; 
        StartCoroutine(Main.instance.web.GetItemsIDs(userID, _createItemsCallback)); 
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        //Parsing json array string as an array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray; 

        for (int i = 0; i < jsonArray.Count; i++)
        {
            //Create local variables
            bool isDone = false; 
            string itemId = jsonArray[i].AsObject["itemID"];
            string id = jsonArray[i].AsObject["ID"]; 

            JSONObject itemInfoJson = new JSONObject();

            //Create a callback to get the info from the web class
            Action<string> getItemInfoCallback = (itemInfo) => {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject; 
            };

            StartCoroutine(Main.instance.web.GetItem(itemId, getItemInfoCallback));

            //Wait until the callback is called from web
            yield return new WaitUntil(() => isDone == true);

            //Instantiate GameObject (item prefab) 
            GameObject itemGo = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            itemGo.transform.SetParent(this.transform);
            itemGo.transform.localScale = Vector3.one;
            itemGo.transform.localPosition = Vector3.zero;

            //fill information
            itemGo.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = itemInfoJson["name"];
            itemGo.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = itemInfoJson["price"];
            itemGo.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = itemInfoJson["description"];


            //Set the sell button 
            itemGo.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() => {
                string iID = itemId;
                string uID = Main.instance.userInfo.UserID;

                StartCoroutine(Main.instance.web.SellItem(iID, uID)); 

            });
                
            //continue to the next item 

        }  
    }
}
