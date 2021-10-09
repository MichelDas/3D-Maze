using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField] private GameObject pausePanel, gameOverPanel, gameCompletePanel;

	[SerializeField] private bool gameStarted;
	[SerializeField] private PlayerControl player;

	[SerializeField] private bool gameOver, gameComplete;


	private Text gameTimeText;
	private float gameTime;

	public int collectableCollected,collectablesToCollect;
	private Text collectableText;

	private GameObject EndPoint;

	private static GameManager instance;

	public static GameManager Instance
	{
		get
		{
			return instance;
		}
	}

	// Use this for initialization
	void Start () 
	{
		
		instance = this;
		player = FindObjectOfType<PlayerControl> ();
		InitialisePanel ();
		gameStarted = true;
		gameTime = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {
		HandleGameOver ();
		HandleGameComplete ();
		HandleUI ();
	}

	private void InitialisePanel()
	{
		pausePanel = GameObject.Find ("PausePanel");	
		gameCompletePanel = GameObject.Find ("GameCompletePanel");
		gameOverPanel = GameObject.Find ("GameOverPanel");
		gameTimeText = GameObject.Find ("GameTime").GetComponent<Text>();
		collectableText = GameObject.Find ("CollectableText").GetComponent<Text> ();
		EndPoint = GameObject.Find ("EndPoint");

		pausePanel.SetActive (false);
		gameCompletePanel.SetActive (false);
		gameOverPanel.SetActive (false);
		EndPoint.SetActive (false);
	}

	public void PausePressed()
	{
		if ( gameStarted )
		{
			gameStarted = false;
			Time.timeScale = 0;
			pausePanel.SetActive (true);
		}
	}

	public void RestartPressed()
	{
		SceneManager.LoadScene ("level");
	}

	public void MainMenuPressed()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}

	public void PauseBackPressed()
	{
		gameStarted = true;
		Time.timeScale = 1;
		pausePanel.SetActive (false);
	}

	public bool GetGameStarted()
	{
		return gameStarted;
	}

	public void SetGameComplete(bool flag)
	{
		gameComplete = flag;
	}

	public void HandleGameOver()
	{
		if ( player.dead )
		{
			gameStarted = false;
			gameOver = true;
			StartCoroutine (ShowGameOverPanel ());
		}
	}

	IEnumerator ShowGameOverPanel()
	{
		yield return new WaitForSeconds (1f);
		gameOverPanel.SetActive (true);
	}

	private bool endpointActived;

	private void HandleGameComplete()
	{
		if ( collectableCollected >= collectablesToCollect  && gameStarted && !endpointActived)
		{
			endpointActived = true;
			EndPoint.SetActive (true);
		}

		if ( gameComplete && gameStarted )
		{
			gameStarted = false;
			player.anim.SetBool ("Dance", true);
			StartCoroutine (ShowGameCompletePanel ());
		}
	}

	IEnumerator ShowGameCompletePanel()
	{
		yield return new WaitForSeconds (4f);
		gameCompletePanel.SetActive (true);
	}

	float min, sec;
	string minParse, secParse;
	private void HandleUI()
	{
		if(gameStarted)
			gameTime += Time.deltaTime;
		min = gameTime / 60;
		sec = gameTime % 60;

		collectableText.text = ": " + collectableCollected;
		minParse = (min < 10) ? "0" : "";
		secParse = (sec < 10) ? "0" : "";
		gameTimeText.text = minParse + Mathf.RoundToInt( min) + ":" + secParse + Mathf.RoundToInt( sec);
	}


}
