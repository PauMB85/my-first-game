using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {
    menu,
    inTheGame,
    gameOver
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance;


    void Awake (){
        sharedInstance = this;
    }    
    
    void Start() {
        currentGameState = GameState.menu;
    }

    void Update () {
        if(currentGameState != GameState.inTheGame && Input.GetButtonDown("s") ){
            StartGame();
        }
    }

    // Use this for start the game
    public void StartGame()
    {
        ChangeState(GameState.inTheGame);
        PlayerController.sharedInstance.StartGame();
    }

    // called then the player dies
    public void GameOver() {
        ChangeState(GameState.gameOver);
    }

    // called when the player wants finish the game and back to the main menu
    public void BackMainMenu() {
        ChangeState(GameState.menu);
    }

    void ChangeState(GameState newGameState) {


        if(newGameState == GameState.menu){
            //in this case we show the menu
        } else if(newGameState == GameState.inTheGame) {
            // the scene of unity show the game
        } else if(newGameState == GameState.gameOver) {
            // the scene to show is the end of game --> GAME OVER
        }
        currentGameState = newGameState;
    }

}
