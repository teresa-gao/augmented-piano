using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text buttonText;
    public TMP_Dropdown dropdown;
    public TMP_Dropdown dropdown2;

    void Start()
    {
        title.text = "Menu";
        buttonText.text = "Play";
    }

    public void TogglePiece(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        Play.piece = dropdown.options[index].text;
    }

    public void TogglePiece2(TMP_Dropdown dropdown2)
    {
        int index = dropdown2.value;
        Debug.Log((dropdown2.options[index].text));
        Play._noteVelocity = float.Parse(dropdown2.options[index].text);
    }

    public void ClickButton()
    {
        Play.isPlaying = !Play.isPlaying;

        if (Play.isPlaying)
        {
            buttonText.text = "Pause";
        }
        else
        {
            buttonText.text = "Play";
        }
    }
}
