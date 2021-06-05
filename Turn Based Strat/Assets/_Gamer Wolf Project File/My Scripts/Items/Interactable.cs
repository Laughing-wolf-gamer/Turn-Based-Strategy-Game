using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Interactable : MonoBehaviour {
        
        

        public virtual void Interact(){
            Debug.Log("Interacting");
        }


    }

}
