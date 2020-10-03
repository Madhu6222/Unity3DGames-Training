using UnityEngine;
using UnityEngine.SceneManagement;

public class Creadits : MonoBehaviour
{

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
