using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    [RequireComponent(typeof(Mover),typeof(Inputs))]
    public class PlayerBrain : MonoBehaviour {
        
        #region Variables.

        #endregion

        private Mover mover;
        private Inputs input;
        #region Methods.
        private void Awake(){
            mover = GetComponent<Mover>();
            input = GetComponent<Inputs>();
        }

        

        
        private void Update(){
            if(mover.GetIsMoveing()) {
                return;
            }

            if(input.GetInputs() != Vector2.zero) {
                if(input.GetInputs().x > 0) {
                    mover.MoveLeft();
                }else if(input.GetInputs().x < 0) {
                    mover.MoveRight();
                }
                if(input.GetInputs().y > 0) {
                    mover.MoveBack();
                }else if(input.GetInputs().y < 0) {
                    mover.MoveFoward();
                }
            }
        }

        #endregion


    }
}
