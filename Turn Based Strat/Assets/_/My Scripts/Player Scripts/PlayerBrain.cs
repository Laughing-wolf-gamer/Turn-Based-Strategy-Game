using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace GamerWolf.TurnBasedStratgeyGame{
    [RequireComponent(typeof(PlayerMover),typeof(Inputs))]
    public class PlayerBrain : TurnManager {
        
        #region Variables.
        [SerializeField] private UnityEvent deathEvent;
        private Inputs input;
        private Mover playerMover;
        private bool enableInputs = false;
        #endregion


        #region Methods.
        protected override void Awake(){
            base.Awake();
            input = GetComponent<Inputs>();
            playerMover = GetComponent<Mover>();
            
        }
        protected override void Start(){
            base.Start();
            m_gameHandler.SetPlayer(this);
        }
        public bool EnableInput{get {return enableInputs;} set{enableInputs = value;}}
        

        
        private void Update(){
            if(playerMover.GetIsMoveing() || m_gameHandler.GetCurrentTurn != Turn.Player) {
                return;
            }
            if(enableInputs){
                if(input.GetInputs() != Vector2.zero) {
                    if(input.GetInputs().x > 0) {
                        playerMover.MoveLeft();
                    }else if(input.GetInputs().x < 0) {
                        playerMover.MoveRight();
                    }
                    if(input.GetInputs().y > 0) {
                        playerMover.MoveBack();
                    }else if(input.GetInputs().y < 0) {
                        playerMover.MoveFoward();
                    }
                }
            }
        }
        public override void FinsiedTurn(){
            // Captureing The Enemies if they didnt See Us..
            CaputreEnemies();
            // Finsided the Turn...
            base.FinsiedTurn();
        }
        public void Die(){
            deathEvent?.Invoke();
        }
        private void CaputreEnemies(){
            List<EnemyBrain> enemies = Board.Instance.FindEnemiesAt(Board.Instance.GetPlayerNode);
            if(enemies.Count > 0){
                foreach(EnemyBrain enemy in enemies){
                    enemy.KillEnemy();
                }
            }
        }
        

        #endregion


    }
}
