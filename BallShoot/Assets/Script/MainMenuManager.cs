using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Star", 0);
            SceneManager.LoadScene(1);
        }
    }
}
