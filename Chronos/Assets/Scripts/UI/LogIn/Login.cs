﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // calls User Interface Unity library
using System; //calls general system
using System.Text.RegularExpressions; //calls the text library in systems and Regular expressions
using UnityEngine.SceneManagement; //allows me to call the next scene in my build

public class Login : MonoBehaviour
{

    private static Login instance;
    public static Login Instance { get { return instance; } }
    private void Awake() // ensures no other log in instance is available but this
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public static PlayerData currentData;

    public GameObject username;//space to take the physical input field object
    public GameObject password;//space to take the physical input field object
    public string Username; //to hold the user's input
    private string Password;//to hold the user's input
    private String[] Lines;
    private string DecryptedPass;//used to check decrypted pass

    public void LoginButton()
    {
        bool UN = false; // username set to false
        bool PW = false; // password set to false
        if (Username != "")
        {
            if (System.IO.File.Exists(Application.persistentDataPath + Username + ".json"))
            {
                UN = true;
                //takes the user's input; then takes that directory file and stores the data in lines
                Lines = System.IO.File.ReadAllLines(Application.persistentDataPath + Username + ".json");
            }
            else
            {
                Debug.LogWarning("Username Invalid!");
            }
        }
        else //tells user that there is no input
        {
            Debug.LogWarning("Username Field Empty");
        }
        //checks password is correct
        if (Password != "")
        {
            if (System.IO.File.Exists(Application.persistentDataPath + Username + ".json"))
            {
                int i = 1;
                foreach (char c in Lines[2])//lines 2 is the password section, since the password is stored in the second (third element)
                {
                    i++;//increments through each password character
                    char Decrypted = (char)(c / i);// this is the function to unravel the hash
                    DecryptedPass += Decrypted.ToString(); //takes actual value
                }
                //checks password entered is true
                if (Password == DecryptedPass)
                {
                    PW = true;
                }
                else//notifies that the password is false
                {
                    Debug.LogWarning("Password is invalid");
                    DecryptedPass = "";
                }

            }
            else//notifies that the password is false
            {
                Debug.LogWarning("Password Is invalid");
                DecryptedPass = "";
            }
        }
        else//notifies that the password hasn't been entered
        {
            Debug.LogWarning("Password Field Empty");
            DecryptedPass = "";
        }
        if (UN == true && PW == true)//checks both are true
        {
            print("Login Successfull");
            username.GetComponent<InputField>().text = "";//clears inputted text
            password.GetComponent<InputField>().text = "";//clears inputted text
        }
    }

    // Update is called once per fram
    void Update()
    {
        //allows the user to shift through each box with tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();//goes to next input field
            }
        }
        //allows the user to enter inputted details with the return button
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Password != "")
            {
                //otherwise user can enter via the login button
                LoginButton();
            }

        }
        //shows what has been entered
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }

}
