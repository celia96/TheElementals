using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// Pause menu scipt
public class PauseMenu : MonoBehaviour
{
	public enum Status
	{
		Active,
		Inactive
	}

	[Tooltip ("Panel with the menu items on them. Gets enabled and disabled.")]
	[SerializeField] GameObject UIPanel = null;

	Status status;
	public string firstLevel;
	public string menu;

	void Start ()
	{
		status = Status.Inactive;
		Time.timeScale = 1;
		UIPanel.SetActive (false);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (status == Status.Active) {
				Time.timeScale = 1;
				Close ();
			} else if (status == Status.Inactive) {
				Time.timeScale = 0;
				Open ();
			}
		}
	}

	/// <summary>
	/// Open the Pause Menu and pause the game.
	/// </summary>
	public void Open ()
	{
		status = Status.Active;
		UIPanel.SetActive (true);
	}

	/// <summary>
	/// Close the Pause Menu and unpause the game.
	/// </summary>
	public void Close ()
	{
		status = Status.Inactive;
		UIPanel.SetActive (false);

	}

	public void NewGame() {
		SceneManager.LoadScene(firstLevel);  
	}


	public void Menu (){
		SceneManager.LoadScene(menu);  
	}

	public void Continue() {
		Close ();
		Time.timeScale = 1;
	}

	/// <summary>
	/// Loads the scene.
	/// </summary>
	public void LoadScene(string scene){
		SceneManager.LoadScene (scene);
	}
}
