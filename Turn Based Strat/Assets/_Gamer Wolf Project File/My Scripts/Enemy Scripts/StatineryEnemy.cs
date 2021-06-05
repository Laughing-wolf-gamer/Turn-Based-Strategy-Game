using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{

    public class StatineryEnemy : EnemyMover {

        [SerializeField] private float standTime = 0.3f;


        protected override void Awake(){
            base.Awake();
        }
        protected override void Start(){
            base.Start();
            faceTarget = false;
        }


        public override void MoveOneTurn(){
            Stand();
        }

        
        private void Stand(){
            StartCoroutine(StandRoutine());
        }
        private IEnumerator StandRoutine(){
            yield return new WaitForSeconds(standTime);

            base.finishedMovementEvent?.Invoke();
        }
    }
}
