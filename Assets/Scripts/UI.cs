using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void OpenMarket()
    {
        SceneManager.LoadScene(3);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }



}
