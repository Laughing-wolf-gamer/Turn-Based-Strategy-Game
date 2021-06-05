using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamerWolf.Utilitys{
    public class Utility : MonoBehaviour {
        public static Vector3Int GetVector3Int(Vector3 inputVector){
            return new Vector3Int(Mathf.RoundToInt(inputVector.x),Mathf.RoundToInt(inputVector.y),Mathf.RoundToInt(inputVector.z));
        }
        public static Vector2Int GetVector2Int(Vector2 _inputVector){
            return new Vector2Int(Mathf.RoundToInt(_inputVector.x),Mathf.RoundToInt(_inputVector.y));
        }
        


    }

}
