using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;   
    }

    public static void GameOver()
    {
        Instance.GameOver_Instance();
    }

    private void GameOver_Instance()
    {
        UIManager _ui = GetComponent<UIManager>();
        if(_ui != null)
        {
            _ui.ToggleDeathPanel();
        }
    }
}
