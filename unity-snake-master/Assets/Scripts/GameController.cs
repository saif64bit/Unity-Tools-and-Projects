using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject title;
    public static bool isPaused = true;
    private static bool reload = false;
    private static GameObject gameOverPanelStatic;
    public Text lengthText = null;

    void Awake()
    {
        gameOverPanelStatic = gameOverPanel;
        isPaused = true;
    }

    void Update()
    {
        lengthText.text = Snake.length.ToString();   

        if (isPaused) {
            if (title.activeSelf) {
                if (Input.anyKey) {
                    title.SetActive (false);
                    isPaused = false;
                }
            } else {
                if (reload) {
                    StartCoroutine (reloading ());
                    reload = false;
                }
            }
        }
    }

    public static void FailGame()
    {
        gameOverPanelStatic.SetActive(true);
        SpawnFood.foodCount = 0;
        isPaused = true;
        reload = true;
    }

    IEnumerator reloading () {
        yield return new WaitForSeconds (3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
