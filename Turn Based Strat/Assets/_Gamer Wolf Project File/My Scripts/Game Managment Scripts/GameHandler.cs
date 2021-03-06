using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using GamerWolf.Utilitys;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace GamerWolf.TurnBasedStratgeyGame{
    [System.Serializable]
    public enum Turn{
        Player,Enemy
    }
    public class GameHandler : MonoBehaviour {
        
        #region Variables.
        public static GameHandler Instance {get; private set;}

        /// Public Variables........................................

        [Header("Game Events.")]
        [SerializeField] private float delayTime = 0.5f;

        #region Events ##################.

        /// Events..........................................
        [SerializeField] private UnityEvent OnGameStartEvents;
        [SerializeField] private UnityEvent OnPlayLevelEvents;
        [SerializeField] private UnityEvent OnLevelEndEvents;
        [SerializeField] private UnityEvent loseLevelEvents;
        [SerializeField] private List<EnemyBrain> m_enemyList = new List<EnemyBrain>();
        [SerializeField] private List<Door> m_doorsList = new List<Door>();
        
        
        

        #endregion

        #region Private Variables###############################......
        // Private Varibles.......................................
        private Board m_board;
        private PlayerBrain m_player;

        private bool hasLevelStarted = false;
        private bool isGamePlaying = false;
        private bool isGameOver = false;
        private bool hasLevelEnded = false;

        private Turn m_currentTurn = Turn.Player;

        #endregion
        [SerializeField] private int playerTurnCount;
        

        #endregion


        #region Methods.

        public void SetPlayer(PlayerBrain player){
            this.m_player = player;
        }
        private void Awake(){
            if(Instance == null){
                Instance = this;
            }else{
                Destroy(Instance);
            }
            
        }
        private void Start(){
            m_board = Board.Instance;
            if(m_player == null){
                StartCoroutine("FindPlayerRoutine");
            }else{
                StartCoroutine("RunGameLoop");
            }
            
        }
        private IEnumerator FindPlayerRoutine(){
            while(m_player == null){
                yield return null;
            }
            if(m_board != null && m_player != null){
                StopCoroutine(nameof(RunGameLoop));
                StartCoroutine(nameof(RunGameLoop));
            }
        }
        // Main Games Loop.
        private IEnumerator RunGameLoop(){

            yield return StartCoroutine(nameof(StartLevelRoutine));
            yield return StartCoroutine(nameof(PlayLevelRoutine));
            yield return StartCoroutine(nameof(EndLevelRoutine));
        }
        // Play all the events on the Game starting.
        private IEnumerator StartLevelRoutine(){
            DebugController.SetRoutineText("StartLevelRoutine");
            m_player.SetInputState(false);
            /// Invokes the Level Start Event......
            OnGameStartEvents?.Invoke();
            while(!hasLevelStarted){
                DebugController.SetDebugTexts("Click To Play");
                // let the player click the play button.
                yield return null;
                
            }
            
        }

        // Play all the events while the games is playing.
        private IEnumerator PlayLevelRoutine(){
            DebugController.SetRoutineText("PlayLevelRoutine");
            isGamePlaying = true;
            yield return new WaitForSeconds(delayTime);
            m_player.SetInputState(true);
            /// Invokes the Level Event......
            OnPlayLevelEvents?.Invoke();
            while(!isGameOver){
                yield return null;
                // Check form Game Over Condition.
                isGameOver = isWinner();

            }
            
        }
        
        

        // Play all the events at the end of the level.
        private IEnumerator EndLevelRoutine(){
            DebugController.SetRoutineText("EndLevelRoutine");
            isGameOver = true;
            m_player.SetInputState(false);
            /// Invokes the Level end Event.
            OnLevelEndEvents?.Invoke();
            
            
            LevelTaskSystem.SetCompletedTask(playerTurnCount,isGameOver,m_player.hasPickedUpItem);
            
            while(!hasLevelEnded){
                DebugController.SetGameOverText(string.Concat(isGameOver,(isWinner() ? "/n Player Won ": " /n Enemy Won ")));
                yield return null;
            }
            Debug.Log("Game Over..................................");
            
        }

        private bool isWinner(){
            return m_board.GetPlayerNode == m_board.GetGoalNode;
        }
        public bool isPlayerHiden(){
            foreach(Node n in m_board.GetHideThePlayerNodesList){
                if(n == m_board.GetPlayerNode){
                    return true;
                }
            }
            return false;
        }
    
            
        public Interactable isOnItem(){
            for(int i = 0; i < m_board.GetItemNodeList.Count; i++){
                if(m_board.GetItemNodeList[i] == m_board.GetPlayerNode){
                    return m_board.GetPickUpItemList[i];
                }
            }
            return null;
        }
        public void OpenDoors(Key.KeyType keyType){
            foreach(Door door in m_doorsList){
                if(door.GetDoorKeyType() == keyType){
                    door.OpenDoor();
                    for (int i = 0; i < m_board.GetDoorNodeList.Count; i++){
                        m_board.GetDoorNodeList[i].ConnectDoorNode(door.GetDoorKeyType());
                    }
                    
                }
            }
            

        }
        public void LevelEnded(){
            hasLevelEnded = true;
            RestartLevel();
        }
        public void LossLevel(){
            StartCoroutine(LossLevelRoutine());
        }

        private IEnumerator LossLevelRoutine(){
            isGameOver = true;
            yield return new WaitForSeconds(2f);
            loseLevelEvents?.Invoke();
            Debug.Log("You Loss..................................");
        }
        private void RestartLevel(){
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene);
        }
        public void PlayLevel(){
            hasLevelStarted = true;
        }
        private void PlayPlayerTurn(){
            m_currentTurn = Turn.Player;
            // Increase the Number of Steps Taken by the Player.
            playerTurnCount++;
            
            m_player.GetIsTurnComplete = true;
        }
        private void PlayEnemyTurn(){
            m_currentTurn = Turn.Enemy;
            foreach(EnemyBrain enemeies in m_enemyList){
                if(!enemeies.GetIsDead){
                    enemeies.GetIsTurnComplete = false;
                    enemeies.PlayTurn();
                }
            }
        }
        // Check if the Enemies had completed thier Turn.
        private bool isEnemyTurnCompleted(){
            foreach(EnemyBrain enemy in m_enemyList){
                if(enemy.GetIsDead){
                    continue;
                }
                if(!enemy.GetIsTurnComplete){
                    return false;
                }
            }
            return true;
        }
        private bool EnemiesAllDead(){
            foreach(EnemyBrain enemy in m_enemyList){
                if(!enemy.GetIsDead){
                    return false;
                }
            }
            return true;
        }
        
        public void UpdateTurn(){
            switch (m_currentTurn){
                case Turn.Player:
                    playerTurnCount++;
                    if(m_player != null){

                        // Play Enemy's Turn if Player turn is completed and any enemy is Not Dead.
                        if(m_player.GetIsTurnComplete && !EnemiesAllDead()){
                            PlayEnemyTurn();
                        }

                    }
                break;
                case Turn.Enemy:
                    // Play Player's Turn if Enemy turn is completed and Player is Not dead...
                    if(isEnemyTurnCompleted()){
                        PlayPlayerTurn();
                    }
                break;
            }
            
            DebugController.SetTurnText(m_currentTurn.ToString());
        }

        public bool GethasLevelStarted{get{return hasLevelStarted;}}
        public bool GetisGamePlaying{get{return isGamePlaying;}}
        public bool GetisGameOver{get{return isGameOver;}}
        public bool GethasLevelEnded{get{return hasLevelEnded;}}
        public Turn GetCurrentTurn{get{return m_currentTurn;}}
        public List<EnemyBrain> GetEnemyList {get{return m_enemyList;}}
        

        #endregion


    }

}
