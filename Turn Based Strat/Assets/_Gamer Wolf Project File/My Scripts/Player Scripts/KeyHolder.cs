using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.TurnBasedStratgeyGame{
    public class KeyHolder : MonoBehaviour {
        
        [SerializeField] private List<Key.KeyType> keyList;

        private void Awake() {
            keyList = new List<Key.KeyType>();
        }
        public void AddKey(Key.KeyType _keyType){
            keyList.Add(_keyType);
        }
        public void RemoveKey(Key.KeyType _keyType){
            keyList.Remove(_keyType);
        }
        public bool ContainsKey(Key.KeyType keyType){
            return keyList.Contains(keyType);
        }
        

        
        

        


    }

}