using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
namespace GamerWolf.TurnBasedStratgeyGame{
    [RequireComponent(typeof(MaskableGraphic))]
    public class ScreenFader : MonoBehaviour {
        
        #region Variables.........................................
        [SerializeField] private Color solidColor = Color.white,clearColor = new Color(1f,1f,1f,0f);
        [SerializeField] private float delay = 0.5f;
        [SerializeField] private float timeToFade = 1f;
        [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;


        


        private MaskableGraphic graphic;

        #endregion


        #region Methods.

        private void Awake() {
            graphic = GetComponent<MaskableGraphic>();
            UpdateColor(solidColor);
        }
        private void UpdateColor(Color _color){
            graphic.color = _color;
        }
        public void FadeOff(){
            iTween.ValueTo(gameObject,iTween.Hash(
                "from",solidColor,
                "to",clearColor,
                "dealyTime",delay,
                "easeType",easeType,
                "OnUpdateTarget", gameObject,
                "onUpdate","UpdateColor"
            ));
        }

        
        

        #endregion


    }

}
