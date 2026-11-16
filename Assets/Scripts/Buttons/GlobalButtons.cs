using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalButtons : MonoBehaviour
{
    [SerializeField] GameObject[] GameStates; //Array for game states, enabling and disabling purposes

    //Open to a specified tab
    public void SwapTab(int tabIndex)
    {
        for (int index = 0; index < GameStates.Length; index++)
        {
            if (index == tabIndex) GameStates[index].SetActive(true); //Enable the state

            else GameStates[index].SetActive(false); //Disable the state
        }
    }


    //Function to load a scene
    public void LoadScene(string inSceneName)
    {
        SceneManager.LoadScene(inSceneName); //Via string, load the scene
    }


    //Function to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
