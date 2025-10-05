using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class manualManager : MonoBehaviour
{
    public Button button;
    public GameObject Manual;

    // Start is called before the first frame update
    void Start()
    {
        Manual.SetActive(false);
    }
    public void OnPushedButton()
    {
             Manual.SetActive(true);
 
    }
    public void PushedButton()
    {

            Manual.SetActive(false);
 
     }
        // Update is called once per frame
        void Update()
    {
        
    }
}
