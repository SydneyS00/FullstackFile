using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance; 
    public Web web;
    public UserInfo userInfo;

    public GameObject UserProfile;
    public GameObject LoginPage; 

    void Start()
    {
        instance = this; 
        web = GetComponent<Web>();
        userInfo = GetComponent<UserInfo>();
    }
}
