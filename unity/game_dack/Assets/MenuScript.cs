using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("Scenes/levels/map_1", LoadSceneMode.Single);
    }
    public void ContinueButtonClicked()
    {
        SaveLoad.Load();
        switch (SaveLoad.Level)
        {
            case 0:
                SceneManager.LoadScene("Scenes/levels/map_" + (SaveLoad.Level + 1), LoadSceneMode.Single);
                break;
            //case 1:
            //    SceneManager.LoadScene("Scenes/levels/map_" + (SaveLoad.Level + 1), LoadSceneMode.Single);
            //    break;
            //case 2:
            //    SceneManager.LoadScene("Scenes/levels/map_" + (SaveLoad.Level + 1), LoadSceneMode.Single);
            //    break;
            default:
                //SceneManager.LoadScene("Scenes/levels/map_1", LoadSceneMode.Single);
                Debug.Log("Level not exist");
                break;
        }
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
