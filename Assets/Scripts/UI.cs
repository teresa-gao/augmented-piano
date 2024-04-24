using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text buttonText;

    void Start()
    {
        title.text = "Menu";
        buttonText.text = "Play";
    }

    public void TogglePiece(string piece)
    {
        Play.pianoPiece = piece;
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
