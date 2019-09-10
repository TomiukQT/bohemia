using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public enum Mode { Add, Remove, ToggleFire };

    public Text m_ModeButtonText;

    public Mode gameMode;

    private void Start()
    {
        gameMode = Mode.Add;
        m_ModeButtonText.text = "Mode: Add";
    }

    public void ChangeMode()
    {
        if (gameMode == Mode.Add)
        {
            gameMode = Mode.Remove;
            m_ModeButtonText.text = "Mode: Remove";
        }
        else if (gameMode == Mode.Remove)
        {
            gameMode = Mode.ToggleFire;
            m_ModeButtonText.text = "Mode: ToggleFire";
        }
        else if (gameMode == Mode.ToggleFire)
        {
            gameMode = Mode.Add;
            m_ModeButtonText.text = "Mode: Add";
        }
    }
    


}
