using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    [RequireComponent(typeof(Mover),typeof(Inputs))]
    public class PlayerBrain : MonoBehaviour {
        
        #region Variables.
        private Inputs input;
        private Mover mover;
        private bool enableInputs = false;
        #endregion


        #region Methods.
        private void Awake(){
            mover = GetComponent<Mover>();
            input = GetComponent<Inputs>();
            
        }
        private void Start(){
            GameHandler.Instance.SetPlayer(this);
        }
        public bool EnableInput{get {return enableInputs;} set{enableInputs = value;}}
        

        
        private void Update(){
            if(mover.GetIsMoveing()) {
                return;
            }

            if(enableInputs){
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
        }
        

        #endregion


    }
}
