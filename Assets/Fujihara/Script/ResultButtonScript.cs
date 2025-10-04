using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("Test GameScene");
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("Test TitleScene");
    }
}
