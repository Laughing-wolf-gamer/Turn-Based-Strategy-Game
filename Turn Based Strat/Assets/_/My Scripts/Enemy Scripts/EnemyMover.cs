using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public enum EnemyType{
        PATROLING_TYPE,
        STATIONERY_TYPE,
        SPINNER_TYPE,
    }
    public class EnemyMover : Mover {
        
        
        [SerializeField] private EnemyType enemyType;
        
        
        [SerializeField] private float standTime = 0.1f;
        [SerializeField] private Vector3 directionToMove = new Vector3(0f,0f,Board.spacing);

        #region Methods.

        protected override void Awake(){
            base.Awake();
        }
        protected override void Start(){
            base.Start();
            faceTarget = true;
        }
        
        public void MoveOneTurn(){
            switch (enemyType){
                case EnemyType.PATROLING_TYPE:
                    Patrol();
                    break;
                case EnemyType.STATIONERY_TYPE:
                    Stand();
                    break;
                case EnemyType.SPINNER_TYPE:
                    Spin();
                    break;
            }
        }
        private void Spin(){
            StartCoroutine(nameof(SpinningRoutine));
        }
        private IEnumerator SpinningRoutine(){
            Vector3 localForward = new Vector3(0f,0f,Board.spacing);
            destination = (transform.TransformVector(localForward * -1f) + transform.position);
            FaceTargetDirection();
            yield return new WaitForSeconds(rotateTime);

            base.finishedMovementEvent?.Invoke();
        }
        private void Stand(){
            StartCoroutine(StandRoutine());
        }
        private IEnumerator StandRoutine(){
            yield return new WaitForSeconds(standTime);

            base.finishedMovementEvent?.Invoke();
        }


        private void Patrol(){
            StartCoroutine(PatrollingRoutine());
        }
        private IEnumerator PatrollingRoutine(){
            Vector3 startPos = new Vector3(m_currentNode.GetCoordinate.x,0f,m_currentNode.GetCoordinate.y);
            
            // Move to new Distination to One space forward vector in local Vector.........
            Vector3 newDestination = startPos + transform.TransformVector(directionToMove);
            // Move to new Distination to Two space forward vector in local Vector.........
            Vector3 nextDestination = startPos + transform.TransformVector(directionToMove * 2f);
            // Move the Enemy to postionion.
            Move(newDestination,0f);

            while(isMoveing){
                yield return null;
            }
            if(m_board != null){
                Node newDestinationNode = m_board.FindNodeAt(newDestination);
                Node nextDestinationNode = m_board.FindNodeAt(nextDestination);
                if(nextDestinationNode == null && !newDestinationNode.GetLinkedNodes.Contains(nextDestinationNode)){
                    destination = startPos;
                    FaceTargetDirection();
                    yield return new WaitForSeconds(rotateTime);
                }
                
            }

            base.finishedMovementEvent?.Invoke();
        }
        #endregion


    }

}
