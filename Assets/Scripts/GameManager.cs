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

    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;

    public int collectedCoins = 0;

    void Awake (){
        sharedInstance = this;
    }    
    
    void Start() {
        currentGameState = GameState.menu;
        ShowMenuCanvas();
        //LevelGenerator.sharedInstance.GenerateInitialBlocks();
    }

    void Update () {
        /*if(currentGameState != GameState.inTheGame ){
            StartGame();
        }*/
    }

    // Use this for start the game
    public void StartGame()
    {
        
        ChangeState(GameState.inTheGame);
        PlayerController.sharedInstance.StartGame();
        ViewInGame.sharedInstace.UpdateHighScoreLabel();
    }

    // called then the player dies
    public void GameOver() {
        ChangeState(GameState.gameOver);
        LevelGenerator.sharedInstance.RestoreGame();
    }

    // called when the player wants finish the game and back to the main menu
    public void BackMainMenu() {
        ChangeState(GameState.menu);
    }

    void ChangeState(GameState newGameState) {


        if(newGameState == GameState.menu){
            //in this case we show the menu
            ShowMenuCanvas();
        } else if(newGameState == GameState.inTheGame) {
            // the scene of unity show the game
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        } else if(newGameState == GameState.gameOver) {
            // the scene to show is the end of game --> GAME OVER
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }
        currentGameState = newGameState;
    }

    void ShowMenuCanvas() {
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }

    public void CollectCoin()
    {
        collectedCoins++;
        ViewInGame.sharedInstace.UpdateCoinsLabel();
    }
}
