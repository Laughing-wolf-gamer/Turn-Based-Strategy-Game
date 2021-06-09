using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    
    public class PickUpItem : Interactable {
        
        
        
        public override void Interact(){
            Debug.Log("Item is Picked");
            Destroy(transform.gameObject);
        }

    }

}