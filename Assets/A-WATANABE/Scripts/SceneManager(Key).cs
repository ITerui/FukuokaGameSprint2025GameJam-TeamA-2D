using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EndGame();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("GameScene");

        }
    }
    private void EndGame()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //ゲーム終了
#else
Application.Quit();  //ゲーム終了
#endif
        }
    }


}
