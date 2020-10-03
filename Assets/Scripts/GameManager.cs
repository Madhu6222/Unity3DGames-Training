using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{

    bool gameHasEnded = false;
    public float restartDelay =1f;
    // public GameObject completeLevelUI;
    
    //public void CompleteLevel()
    //{
    //    completeLevelUI.SetActive(true);
    //}

    public void EndGame()
    {

        if (gameHasEnded==false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);

        }
        
    }

     void Restart()
      {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }

    public void ResetHighScore()
     {
         PlayerPrefs.DeleteKey("HighScore");
         FindObjectOfType<Text>().text = "0";
     }
}
