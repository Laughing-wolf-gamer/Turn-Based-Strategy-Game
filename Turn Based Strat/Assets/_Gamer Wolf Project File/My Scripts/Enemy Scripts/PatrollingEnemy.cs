using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GamerWolf.TurnBasedStratgeyGame{
    public class PatrollingEnemy : EnemyMover {
        
        
        private Vector3 directionToMove = new Vector3(0f,0f,Board.spacing);

        public override void MoveOneTurn(){
            Patrol();
        }

        protected override void Awake(){
            base.Awake();
        }
        protected override void Start(){
            base.Start();
            faceTarget = true;
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

        


    }

}
