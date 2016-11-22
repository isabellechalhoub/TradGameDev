using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public AudioSource clip1;
    public AudioSource clip2;

    // Use this for initialization
    void Start () 
	{
        clip1 = GameObject.Find("MenuMusic").GetComponent<AudioSource>();
        clip2 = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
    }

	// Event handling when Restart button clicked
	public void RestartLevel()
	{
        clip1.Stop();
        clip2.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// Event handling when Exit button clicked - go to main menu
	public void ExitLevel()
	{
        clip2.Stop();
        clip1.Play();
        clip1.time = 2.0f;
        Debug.Log("here");
        Debug.Log(clip1.isPlaying);
        Debug.Log(clip1.name);
        SceneManager.LoadScene(0);
	}

	// Even handling for Exit game button on main menu
	public void ExitGame()
	{
		Application.Quit();
	}

	// Event handling for Play button on main menu - load level 1
	public void Play()
	{
        clip1.Stop();
        clip2.Play();
        SceneManager.LoadScene(1);
	}
}
