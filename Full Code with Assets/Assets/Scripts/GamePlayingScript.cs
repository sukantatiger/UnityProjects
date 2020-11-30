 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // We are working with UI
using UnityEngine.UIElements;

public class GamePlayingScript : MonoBehaviour{

	int gameTime = 120;
	int timeMin, timeSec;
	int roundShot = 0;
	int enemyKilled = 0;
	public Text timerTextView;
	public Text scoreTextView;
	public Text finalResult;
	public GameObject gunGo;
	public GameObject targetGo;
	// Game Over Variables
	public GameObject gameOverPanel;
	bool gameIsPaused = false;
	public GameObject tryAgain;
	
	// For sound in the project
	AudioSource gunShotAudioSource;
	public AudioClip gunShotClip;

	// For particle System
	public ParticleSystem bulletFire;

	// Start is called before the first frame update
	void Start(){
        
		InvokeRepeating("timeControlAction", 0, 1);
		// Creating audio source
		gunShotAudioSource = gameObject.GetComponent<AudioSource>();
		scoreTextView.text = enemyKilled.ToString() + " / " + roundShot.ToString();
    }

    void Update()
    {
		if (!gameIsPaused) 
		{
			UnityEngine.Cursor.visible = false;
			// Play shooting sound
			//if(Input.GetKeyUp(KeyCode.Space))
			if (Input.GetMouseButtonUp(0))
			{

				shootAction();
			}

			// Make the gun always point to target icon
			gunGo.transform.LookAt(targetGo.transform);

			// if mouse pointer speed is high then divide otherwsie multiply with a integer.
			float h = Input.GetAxis("Mouse X") * 5;
			float v = Input.GetAxis("Mouse Y") * 5;


			// Translate according to mouse pointer movement
			Vector3 targetMov = new Vector3(h, v, 0);
			targetGo.transform.Translate(targetMov);

			// Setting the crosshair
			float x = targetGo.transform.position.x;
			float y = targetGo.transform.position.y;

			if (x > 55)
				x = 55;
			if (x < -55)
				x = -55;
			if (y > 38)
				y = 38;
			if (y < -25)
				y = -25;
			targetGo.transform.position = new Vector3(x, y, 0);
		}
		
		

    }

	// Code for timer
    void timeControlAction()
	{
		if (gameTime > 0) 
		{
			timeMin = (int)gameTime / 60;
			timeSec = gameTime % 60;
			timerTextView.text = "Time Left: " + timeMin.ToString() + ":" + timeSec.ToString("D2");
			gameTime = gameTime - 1;
		}
        else 
		{
			if (enemyKilled >= 50)
			{
				finalResult.text = "YOU WIN!";
				tryAgain.SetActive(false);
			}
			else { 
				finalResult.text = " YOU LOSE!";
			}
			//Game Over coding
			timerTextView.text = "Time Over";
			timerTextView.color = Color.red;
			Time.timeScale = 0; // Freezing everything
			gameIsPaused = true; // Indicating game is finished
			gameOverPanel.GetComponent<CanvasGroup>().alpha = 1;
			gameOverPanel.GetComponent<CanvasGroup>().interactable = true;
			gameOverPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
			UnityEngine.Cursor.visible = true;

		}

		
		
	}

	// Code for shooting sound
	void shootAction()
	{
		// Getting the direction of crosshair and saving it in a 2d vector
		Vector2 dir = new Vector2(targetGo.transform.position.x, targetGo.transform.position.y);
		// Shoot a invisible ray on any object (Enemy)
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, dir);
		// If it hit the enemy then count will be increased
		if (hit.collider != null && hit.collider.gameObject != targetGo)
		{
			// we have shot one of the enemy
			enemyKilled++;
			Destroy(hit.collider.gameObject);
		}
		// Incrementing number of rounds fired whenever we press the space button
		roundShot++;
		scoreTextView.text = enemyKilled.ToString() + " / " + roundShot.ToString();
		// Playing gunshot sound
		gunShotAudioSource.PlayOneShot(gunShotClip);
		// Firing bullet
		bulletFire.Emit(1);
	}

	// Game Resetart functionality
	public void gameRestart() {
		Time.timeScale = 1; // Unfreez everything
		gameIsPaused = false; // Indicating game has restarted
		gameOverPanel.GetComponent<CanvasGroup>().alpha = 0;
		gameOverPanel.GetComponent<CanvasGroup>().interactable = false;
		gameOverPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
		UnityEngine.Cursor.visible = false;
		timerTextView.color = Color.black;
		//Resetting timer
		gameTime = 120;
		roundShot = 0;
		enemyKilled = 0;
	}
}
