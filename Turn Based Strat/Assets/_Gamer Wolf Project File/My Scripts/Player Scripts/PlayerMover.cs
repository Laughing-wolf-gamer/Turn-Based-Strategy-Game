using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class PlayerMover : Mover {
        
        #region Variables.
        
        #endregion

        private PlayerCompass compass;
        #region Methods.
        protected override void Awake(){
            base.Awake();
            compass = GetComponent<PlayerCompass>();
        }
        

        protected override void Start() {
            base.Start();
            
            m_board.SetPlayer(this);
            // Updating the Board first time.
            UpdateBoard();
            
        }
        public void ShowingCompass(){
            compass.ShowArrow(true);
        }
        
        
        
        private void UpdateBoard(){
            if(m_board != null){
                // Updating the Board for Player Positon on the Board.
                m_board.UpdatePlayerNode();
            }
        }
        protected override IEnumerator MoveCouroutine(float _delayTime, Vector3 _destination){
            compass.ShowArrow(false);
            
            yield return StartCoroutine(base.MoveCouroutine(_delayTime, _destination));

            // Updating the Board for Player Positon on the Board.
            UpdateBoard();
            // Update the Compass for Player......
            compass.ShowArrow(true);

            base.finishedMovementEvent?.Invoke();
        }


        #endregion


    }

}
