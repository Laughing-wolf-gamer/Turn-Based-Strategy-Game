using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class Door : MonoBehaviour {
        
        
        [SerializeField] private Key.KeyType doorKey;
        private Collider m_collider;
        private void Awake(){
            m_collider = GetComponent<Collider>();
        }

        
        public Key.KeyType GetDoorKeyType(){
            return doorKey;
        }
        public void OpenDoor(){
            m_collider.enabled = false;
            gameObject.SetActive(false);
            Debug.Log("Opening The Door");
        }

    }

}
