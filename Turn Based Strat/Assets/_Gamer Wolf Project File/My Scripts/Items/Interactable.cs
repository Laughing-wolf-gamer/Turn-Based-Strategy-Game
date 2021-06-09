using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public enum ItemType{
        PICK_UP_ITEM,
        HIDE_THE_PLAYER_ITEM,
    }
    public class Interactable : MonoBehaviour {
        
        
        public ItemType itemType;
        public virtual void Interact(){
            Debug.Log("Interacting");
        }


    }

}
