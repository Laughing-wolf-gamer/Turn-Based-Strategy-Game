using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public enum ItemType{
        PICK_UP_ITEM,
        HIDE_THE_PLAYER_ITEM,
        KEY,
    }
    public class Interactable : MonoBehaviour {
        
        
        public ItemType itemType;
        public virtual void Interact(){

        }
        public ItemType GetITemType(){
            return itemType;
        }


    }

}
