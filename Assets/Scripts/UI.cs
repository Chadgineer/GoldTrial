using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void OpenMarket()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }

    public void GoBack()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
