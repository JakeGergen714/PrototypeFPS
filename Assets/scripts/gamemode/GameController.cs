using System;
using System.Collections;
using System.Collections.Generic;
using gamemode;
using gamemode.JakeTDM;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public JakeTDM gamemode;
    
    // Start is called before the first frame update
    void Start()
    {
        gamemode.startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        gamemode.tick();

        if (gamemode.isGameOver())
        {
            gamemode.stopGame();
        }
    }
}
