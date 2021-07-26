using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
	
	/// <summary>
	/// A static instance of this script as object to be called when needed
	/// </summary>
	public static ChangeScene Instance {

		get;
		private set;
	}

	private void OnEnable () {

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable () {

		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	
	private void Awake () {

		InitialSet();
	}

	// Sets this as its own instance
	private void InitialSet () {

		if (Instance == null) {
			
			Instance = this;
		}
	}

	/// <summary>
	/// Load the next scene by index in the build list and locks the mouse
	/// </summary>
	public void LoadNextScene () {
		
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount + 1) {

			ChangeTo(SceneManager.GetActiveScene().buildIndex + 1);
		} else {

			ChangeTo(0);
		}
	}

	/// <summary>
	/// Load the previous scene by index in the build list and locks the mouse
	/// </summary>
	public void LoadPreviousScene () {
		
		ChangeTo(SceneManager.GetActiveScene().buildIndex - 1);
	}
	
	/// <summary>
	/// Load the scene with the specified index and locks the mouse
	/// </summary>
	/// <param name="sceneIndex">The index of the scene to be loaded</param>
	public void ChangeTo (int sceneIndex) {
		
		SceneManager.LoadScene(sceneIndex);
	}

	/// <summary>
	/// Load the scene with the specified name and locks the mouse
	/// </summary>
	/// <param name="sceneName">The name of the scene to be loaded</param>
	public void ChangeTo (string sceneName) {
		
		SceneManager.LoadScene(sceneName);
	}

	/// <summary>
	/// Restart the current scene
	/// </summary>
	public void RestartScene () {

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>
	/// Restart the current scene after 'timeToRestart' seconds
	/// </summary>
	/// <param name="timeToRestart">Time in seconds to restart the scene</param>
	public void RestartScene (float timeToRestart) {

		Invoke ("RestartScene", timeToRestart);
	}

	/// <summary>
	/// Quits the application
	/// </summary>
	public void QuitGame () {

		Application.Quit();
	}

	// global scene setup, at the moment nothing is global, everything is scene-dependant.
	private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		
	}
}