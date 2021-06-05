using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    [RequireComponent(typeof(BoxCollider))]
    public class Obstacles : MonoBehaviour {
        
        #region Variables.
        
        private BoxCollider box;
        #endregion


        #region Methods.
        private void Awake(){
            box = GetComponent<BoxCollider>();
        }
        

        
        private void OnDrawGizmos(){
            
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position,transform.localScale);
        }

        #endregion


    }

}