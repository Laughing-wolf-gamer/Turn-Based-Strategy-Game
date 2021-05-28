using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {
    
    #region Variables.

    [SerializeField] private float boarderWidth = 0.02f;
    [SerializeField] private float linkThickNess = 0.5f;
    [SerializeField] private float scaleTime = 0.3f;
    [SerializeField] private float dealyTime = 0.2f;

    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    public bool isInitialize = false;
    #endregion


    #region Methods.

    

    public void DrawLink(Vector3 _StartPos, Vector3 _EndPos){
        transform.localScale =new Vector3(linkThickNess,1f,0f);
        Vector3 dirVector = (_EndPos - _StartPos);
        float zScale = dirVector.magnitude - boarderWidth * 2f;
        Vector3 newscale = new Vector3(linkThickNess,1f,zScale);
        transform.rotation = Quaternion.LookRotation(dirVector);
        transform.position = _StartPos + (transform.forward * boarderWidth);
        iTween.ScaleTo(gameObject,iTween.Hash(
            "time",scaleTime,
            "scale",newscale,
            "easeType",easeType,
            "dealyTime",dealyTime
        ));
        isInitialize = true;


    }
    
    

    #endregion


}
