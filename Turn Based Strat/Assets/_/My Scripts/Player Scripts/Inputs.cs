using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Inputs : MonoBehaviour {
        
        #region Variables.
        private float horizontalInput;
        private float verticalInputs;

        
        #endregion


        #region Methods.
        private void Update(){
            
            verticalInputs = Input.GetAxisRaw("Vertical");
            horizontalInput = Input.GetAxisRaw("Horizontal");
            
        }
        public Vector2 GetInputs(){
            return new Vector2(horizontalInput,verticalInputs).normalized;
        }
        

        #endregion


    }

}