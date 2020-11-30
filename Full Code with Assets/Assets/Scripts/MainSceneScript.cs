using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneScript : MonoBehaviour
{
	// Loading the main scene
	public void GoToNextSceneAction(){
		//Application.LoadLevel("SceneOne");
		SceneManager.LoadScene("SceneOne");
	}
}
