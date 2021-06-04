using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{

    #region Require components.
    [RequireComponent(typeof(EnemyMover),typeof(EnemySensor),typeof(EnemyAttack))]
    [RequireComponent(typeof(EnemyDeath))]
    #endregion
    public class EnemyBrain : TurnManager {
        
        #region Variables.
        [SerializeField] private UnityEvent deathEvents;

        // Private Variables.......................
        private EnemyMover mover;
        private EnemyAttack enemyAttack;
        private EnemySensor sensor;
        private Board m_board;
        private bool m_isDead;
        #endregion


        #region Methods.
        protected override void Awake(){
            base.Awake();
            mover = GetComponent<EnemyMover>();
            enemyAttack = GetComponent<EnemyAttack>();
            sensor = GetComponent<EnemySensor>();

        }
        

        protected override void Start() {
            m_board = Board.Instance;
            base.Start();
        }

        
        public void PlayTurn(){
            if(!m_isDead){
                StartCoroutine(PlayTurnRoutine());
            }else{
                FinsiedTurn();
            }

        }
        public void KillEnemy(){
            if(!m_isDead){
                m_isDead = true;
                deathEvents?.Invoke();
            }
        }
        private IEnumerator PlayTurnRoutine(){
            if(m_gameHandler != null && !m_gameHandler.GetisGameOver){

                // Updtae Sensore.
                sensor.UpdateSensore();
                // Wait
                yield return new WaitForSeconds(0f);

                if(sensor.GetFoundPlayer){
                    // Invoke the Enemy Attack.....
                    Vector3 playerPosition = new Vector3(m_board.GetPlayerNode.GetCoordinate.x,0f,m_board.GetPlayerNode.GetCoordinate.y);
                    
                    yield return new WaitForSeconds(0.3f);
                    mover.Move(playerPosition,0f);
                    // Attack the Player.
                    enemyAttack.Attack();
                    // Notifie the GameHandler to Loss Level...........
                    m_gameHandler.LossLevel();
                    
                }else{

                    // movement.
                    // Enemy Mover . some More Methodes........
                    mover.MoveOneTurn();// it invokes the finisedMovemnt Event in Enemy Mover.
                    
                
                }
            }
        }
        public bool GetIsDead{
            get{
                return m_isDead;
            }
        }

        #endregion


    }

}
