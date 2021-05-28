using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Board : MonoBehaviour {
        
        #region Variables.
        public static float spacing = 2f;
        public static Vector2[] direction = {
            new Vector2(spacing,0f),
            new Vector2(-spacing,2f),
            new Vector2(0f,spacing),
            new Vector2(0f,-spacing)
        };


        #endregion


        #region Methods.

        private void Start() {
            
        }

        
        private void Update(){
            
        }

        #endregion


    }

}