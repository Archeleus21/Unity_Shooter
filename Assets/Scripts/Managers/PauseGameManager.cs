using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameManager : MonoBehaviour
{
    public Text pauseText;
    public Text resumeButton;
    public Text exitButton;
    public Image pauseFader;

    Animator pauseAnimator;

	// Use this for initialization
	void Awake ()
    {
        pauseAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
      
    public void PauseGame()
    {
        pauseAnimator.SetTrigger("Game Paused");           
        Time.timeScale = 0;          
    }

    public void ClickedResumeButton()
    {
        Time.timeScale = 1;
        pauseAnimator.SetTrigger("Game UnPaused");
    }

    public void ClickedExitButton()
    {
        Application.Quit();
    }
}
