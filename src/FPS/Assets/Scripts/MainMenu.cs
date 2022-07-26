using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 //This changes the scene to whataver is next in the index order
public void PlayGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

}
 //This changes the scene to whataver is specified
public void MainMenuPage()
{
    SceneManager.LoadScene("MainMenu");
}

public void ControlsPage()
{
    SceneManager.LoadScene("Controls");
}

public void QuitGame()
    {
        Application.Quit();
    }

}
