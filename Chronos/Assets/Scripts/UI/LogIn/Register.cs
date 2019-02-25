﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // calls User Interface Unity library
using System;//calls general system
using System.Text.RegularExpressions;//calls the text library in systems and Regular expressions
using SimpleJSON;

public class Register : MonoBehaviour
{
    //all object Boxes on the UI 
    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject confPassword;
    //all the data that will be inputted into the objects
    private string Username;
    private string Email;
    private string Password;
    private string ConfPassword;
    private string form;
    private bool EmailValid = false;
    //all characters it can accept
    private string[] Characters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                    "A", "B","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                                    "0","1","2","3","4","5","6","7","8","9","_","-"};

    //Register Button code
    public void RegisterButton()
    {
        //current status of all input fields are false
        bool UN = false; // username
        bool EM = false; // email
        bool PW = false; // password
        bool CP = false; // confirm password
        int UScore = 0;
        //check the file path
        // checks the directory to see if the name exists
        if (Username != "")
        {
            //Checks availability of the username is all directories
            if (!System.IO.File.Exists(Application.persistentDataPath + Username + ".json"))
            {
                UN = true;
            }
            else
            {
                Debug.LogWarning("Username Taken");
            }
        }
        else//if empty it will relay a message to the console
        {
            Debug.LogWarning("Username field empty");
        }
        if (Email != "")
        {
            EmailValidation();
            //if true, tests that it is an email containint @ and letter etc, not that if it really exists 
            if (EmailValid)
            {
                if (Email.Contains("@"))
                {
                    if (Email.Contains("."))
                    {
                        EM = true;
                    }
                    else
                    {
                        Debug.LogWarning("Email is Incorrect");
                    }
                }
                else//email doesn't follow email format
                {
                    Debug.LogWarning("Email is Incorrect");
                }

            }
            else//notifies in console that there is no valid email
            {
                Debug.LogWarning("Email is Incorrect");
            }
        }//notifies in console that field is empty
        else
        {
            Debug.LogWarning("Email Field Empty");
        }
        if (Password != "")
        {
            //checks to see passwword character length is 6+
            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                Debug.LogWarning("Password must be at least 6 Characters Long");
            }
        }
        else//notifies in console that the password box is empty
        {
            Debug.LogWarning("Password field Empty");
        }
        if (ConfPassword != "") //checks password confirmation box
        {
            if (ConfPassword == Password)
            {
                CP = true;//makes sure that the passwords match
            }
            else//notifies through console that passwords dont match
            {
                Debug.LogWarning("Passwords don't match");
            }
        }
        else//otherwise notifies that password field is empty
        {
            Debug.LogWarning("Confirm Password field is empty");
        }
        //encrypts password entered by multiplying the letter e.g. a = 1 then a*2 = b
        if (UN == true && EM == true && PW == true && CP == true)
        {
            bool Clear = true;
            int i = 1;
            foreach (char c in Password)
            {
                if (Clear)
                {
                    Password = "";
                    Clear = false;
                }
                i++;//increments through each password character
                //hashes password, shifting character value
                char Encrypted = (char)(c * i);
                Password += Encrypted.ToString();//stored as unreadable string in file
            }
            //stores in the certain format
            form = (Username + "\n" + Email + "\n" + Password + "\n" + UScore);
            //writes to the file the user's data
            System.IO.File.WriteAllText(Application.persistentDataPath + Username + ".json", form);
            //gets the input of the user and registers the text to the form
            username.GetComponent<InputField>().text = "";
            //gets the user's input and tegisters the text to the form
            email.GetComponent<InputField>().text = "";
            //gets the user's input and tegisters the text to the form
            password.GetComponent<InputField>().text = "";
            //gets the user's input and tegisters the text to the form
            confPassword.GetComponent<InputField>().text = "";
            print("Registration Complete");// once done, notifies complete registration in console
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if the cursor is on that field
        //Allows user to flip through using the tab button to each box
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //goes from box to box
            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();//goes to next input field
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();//goes to next input field
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confPassword.GetComponent<InputField>().Select();//goes to next input field
            }
        }
        //allows the user to press return to complete form
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Email != "" && Password != "" && ConfPassword != "")
            {
                //also allows the form to be entered by a button press
                RegisterButton();
            }
        }
        //registers the user's input in the username tab
        Username = username.GetComponent<InputField>().text;
        //registers the user's input in the username tab
        Email = email.GetComponent<InputField>().text;
        //registers the user's input in the Password tab
        Password = password.GetComponent<InputField>().text;
        //registers the user's input in the ConfPassword tab
        ConfPassword = confPassword.GetComponent<InputField>().text;
    }
    //validates the email entered
    void EmailValidation()
    {
        //starts off a false
        bool SW = false;
        bool EW = false;
        //runs through each character entered
        for (int i = 0; i < Characters.Length; i++)
        {
            //checks characters entered comply with the characters array
            if (Email.StartsWith(Characters[i]))
            {
                SW = true;
            }
        }
        //runs through each character entered
        for (int i = 0; i < Characters.Length; i++)
        {
            //checks characters entered comply with the characters array
            if (Email.StartsWith(Characters[i]))
            {
                EW = true;
            }
        }
        //finaly ensures both email boxes entered are true
        if (SW == true && EW == true)
        {
            EmailValid = true;
        }
        else
        {
            EmailValid = false;
        }
    }
}