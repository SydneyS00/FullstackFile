using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(GetDate());
        //StartCoroutine(GetUsers());
        //StartCoroutine(Login("testuser2", "123456")); 
        //StartCoroutine(RegisterUser("testuser3", "13579"));
    }


    public IEnumerator GetDate()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackendTutorial/GetDate.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);

                //Or retrieve results as binary data
                byte[] results = www.downloadHandler.data; 
            }
        }
    }

    public IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackendTutorial/GetUsers.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);

                //Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator Login(string userName, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", userName);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/Login.php", form))
        {
            yield return www.SendWebRequest(); 

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error); 
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Main.instance.userInfo.SetCredentials(userName, password);
                Main.instance.userInfo.SetID(www.downloadHandler.text);

                if(www.downloadHandler.text.Contains("Wrong credentials :( ") || www.downloadHandler.text.Contains("Username does not exist"))
                {
                    Debug.Log("Try again!");
                }

                else
                {
                    //if we logged in correctly enable the profile and disable the login page
                    Main.instance.UserProfile.SetActive(true);
                    Main.instance.LoginPage.SetActive(false);
                }
                
    
               

            }
        }
    }

    public IEnumerator RegisterUser(string userName, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", userName);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {

        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/GetItemsIDs.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                //callback function to pass results
                callback(jsonArray); 

            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback) 
    {

        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/GetItem.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;

                //callback function to pass results
                callback(jsonArray);

            }
        }
    }

    public IEnumerator SellItem(string itemID, string userID) //System.Action<string> callback)
    {

        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/SellItem.php", form))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show results as text
                Debug.Log(www.downloadHandler.text);

                //callback function to pass results
                //callback("");

            }
        }
    }
}
