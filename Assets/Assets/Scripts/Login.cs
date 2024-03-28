using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Login : MonoBehaviour
{
    [Header("Login Page")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button createNewButton;

    [Header("Images")]
    public GameObject loginImage;
    public GameObject createNewImage;

    [Header("Create New Page")]
    public TMP_InputField newUsernameInput;
    public TMP_InputField newPasswordInput;
    public TMP_InputField confirmPassword;
    public Button submitButton; 


    

    void Start()
    {
        loginButton.onClick.AddListener(() =>
        {
            //Call the login function to pass the inputs we put in
            StartCoroutine(Main.instance.web.Login(usernameInput.text, passwordInput.text)); 
        });

        submitButton.onClick.AddListener(() =>
        {
            //TESTING
            //Call the register function to pass the inputs we put in for a new account

            if(confirmPassword.text != newPasswordInput.text)
            {
                Debug.Log("Passwords do not match..please enter again."); 
            }
            else
            {
                StartCoroutine(Main.instance.web.RegisterUser(newUsernameInput.text, newPasswordInput.text));
            }
            
        });
    }

    public void ClickNewUser()
    {
        createNewImage.SetActive(true);
        loginImage.SetActive(false); 
    }
}
