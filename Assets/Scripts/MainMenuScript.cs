﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayPressed()
	{
		SceneManager.LoadScene ("level");
	}

	public void ExitPressed()
	{
		Application.Quit ();
	}
		
}
