using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
public class loading : MonoBehaviour
{
    [SerializeField] private float loadingTime;

    private void Awake()
    {
        loadingTime = Random.Range(3f, 5f);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(loadingTime);
        SceneManager.LoadScene(1);
    }
}
