﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PManager : MonoBehaviour
{
    public GameObject player;//to get my player controller script
    public GameObject crosshair;
    public GameObject time;//to get my timescript
    [SerializeField] private bool isPaused;//verify the game is paused etc.
    public GameObject PauseMenu;
    public GameObject GameManager;
    public float TimeUnfrozen;//variable to control the factor of normal time etc
    PlayerController controller;
    public bool lockCursor;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//checks if the P key has been pressed
        {
            isPaused = !isPaused;//if game isn't paused
            Cursor.lockState = CursorLockMode.None;// cursor is unlocked
            Cursor.visible = true;//the cursor will be visible
        }
        if (Input.GetKeyDown(KeyCode.Escape))//if the user presses the escape key
        {
            Cursor.lockState = CursorLockMode.None;//cursor is unlocked
            Cursor.visible = true;//the cursor will be visible
        }
        if (isPaused)//if it is paused
        {
            ActivateMenu();//call my active menu method
        }
        else
        {
            Resume();//otherwise call the reume method
        }

    }

    public void Start()
    {//start my game off with my cursor locked and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ActivateMenu()
    {
        PauseMenu.SetActive(true);//the menu will be visible on screen
        player.GetComponent<PlayerController>().enabled = false;//to turn my playerController off - you initialy refreence to the parent
        crosshair.SetActive(false);//turns off my crosshairs
        time.GetComponent<TimeEffect>().enabled = false;//to make my game stop completely
        Time.timeScale = 0;//turns time off;
    }

    public void Resume()
    {
        time.GetComponent<TimeEffect>().enabled = true;//to make my time effect activate again
        crosshair.SetActive(true);//turns my crosshair back on
        PauseMenu.SetActive(false);//turns off my pause menu
        player.GetComponent<PlayerController>().enabled = true;//to turn my playerController back on 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
