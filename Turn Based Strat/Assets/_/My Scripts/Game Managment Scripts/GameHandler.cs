using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace GamerWolf.TurnBasedStratgeyGame{
    
    public class GameHandler : MonoBehaviour {
        
        #region Variables.
        public static GameHandler Instance {get; private set;}

        // Public Variables........................................

        [Header("Game Events.")]
        [SerializeField] private float delayTime = 0.5f;

        // Events..........................................
        [SerializeField] private UnityEvent OnGameStartEvents;
        [SerializeField] private UnityEvent OnPlayLevelEvents;
        [SerializeField] private UnityEvent OnLevelEndEvents;


        // Private Varibles.......................................
        private Board m_board;
        private PlayerBrain m_player;

        private bool hasLevelStarted = false;
        private bool isGamePlaying = false;
        private bool isGameOver = false;
        private bool hasLevelEnded = false;

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
            m_board = GetComponent<Board>();
        }
        private void Start(){
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
                StartCoroutine("RunGameLoop");
            }else{
                Debug.Log("Board or Player is Missing.");
            }
        }
        // Main Games Loop.
        private IEnumerator RunGameLoop(){

            yield return StartCoroutine("StartLevelRoutine");
            yield return StartCoroutine("PlayLevelRoutine");
            yield return StartCoroutine("EndLevelRoutine");
        }
        // Play all the events on the Game starting.
        private IEnumerator StartLevelRoutine(){
            Debug.Log("StartLevelRoutine");
            m_player.EnableInput = false;
            while (!hasLevelStarted){
                
                // let the player click the play button.
                yield return null;
                
            }
            /// Invokes the Level Start Event......
            
            OnGameStartEvents?.Invoke();
        }

        // Play all the events while the games is playing.
        private IEnumerator PlayLevelRoutine(){
            Debug.Log("PlayLevelRoutine");
            isGamePlaying = true;
            yield return new WaitForSeconds(delayTime);
            m_player.EnableInput = true;
            /// Invokes the Level Event......
            OnPlayLevelEvents?.Invoke();
            while(!isGameOver){

                yield return null;

            }
        }

        // Play all the events at the end of the level.
        private IEnumerator EndLevelRoutine(){
            Debug.Log("EndLevelRoutine");
            isGameOver = true;
            m_player.EnableInput = false;

            while(!hasLevelEnded){

                yield return null;
            }
            /// Invokes the Level end Event.
            OnLevelEndEvents?.Invoke();
            RestartLevel();
        }
        private void RestartLevel(){
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene);
        }
        public void PlayLevel(){
            hasLevelStarted = true;
        }

        public bool GethasLevelStarted{get{return hasLevelStarted;}}
        public bool GetisGamePlaying{get{return isGamePlaying;}}
        public bool GetisGameOver{get{return isGameOver;}}
        public bool GethasLevelEnded{get{return hasLevelEnded;}}
        

        #endregion


    }

}
