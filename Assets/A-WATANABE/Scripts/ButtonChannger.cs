using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChannger : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] int selectedIndex;

    private Color unselectedColor = new Color(0.3f, 0.3f, 0.3f); // à√ÇﬂÇÃÉOÉåÅ[
    private Color defaultNormalColor;


    void UpdateButtonColors()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var colors = buttons[i].colors;

            if (i == selectedIndex)
            {
                colors.normalColor = unselectedColor;
            }
            else 
            { 
                colors.normalColor = defaultNormalColor;
            }

            buttons[i].colors = colors;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonColors();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
