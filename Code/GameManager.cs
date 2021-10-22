using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    public GameObject pauseMenu;
    public GameObject quitBtn;
    private void Start()
    {
        Resume();

#if UNITY_WEBGL
        quitBtn.SetActive(false);
#endif

 #if UNITY_EDITOR
        quitBtn.SetActive(true);
#endif

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PublicVars.paused)
            {
                Resume();
            }
            else
            {
                pauseMenu.SetActive(true);
                PublicVars.paused = true;
                Time.timeScale = 0;
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        PublicVars.paused = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PublicVars. paused = false;
        PublicVars.Herb = 0;
        PublicVars.numHearts = 3;
        PublicVars.life = 3;
        PublicVars.bossBeaten = false;
        PublicVars.doubleDamage = false;
    }

    public void Quit()
    {
    #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif
        // change to quit to main menu!!!!!!!!
    }

    public void useHerb(){
        if (PublicVars.Herb > 0){
            if (PublicVars.Herb == 3){
                PublicVars.doubleDamage = true;
            }
            PublicVars.numHearts += PublicVars.Herb;
            PublicVars.life += PublicVars.Herb;
            PublicVars.Herb = 0;
        }
    }
}
