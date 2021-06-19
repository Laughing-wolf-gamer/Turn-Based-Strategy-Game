using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Key : Interactable {
        

        public enum KeyType{
            Red,Yellow,Blue
        }

        [SerializeField] private KeyType keyType;
        
        public override void Interact(){
            base.Interact();
            Debug.Log("Interacting With Key");
            Destroy(gameObject);
        }
        public KeyType GetKeyType(){
            return keyType;
        }

    }

}