using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    
    public class EnemyMover : Mover {
        
        #region Methods.
        public virtual void MoveOneTurn(){
            Debug.Log("Enemies Moveing.");
        }
        
        
        #endregion


    }

}
