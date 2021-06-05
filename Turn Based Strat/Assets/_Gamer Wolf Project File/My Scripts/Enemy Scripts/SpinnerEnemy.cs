using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class SpinnerEnemy : EnemyMover {
        
        protected override void Awake(){
            base.Awake();
        }
        protected override void Start(){
            base.Start();
            faceTarget = true;
        }
        public override void MoveOneTurn(){
            Spin();
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


    }

}
