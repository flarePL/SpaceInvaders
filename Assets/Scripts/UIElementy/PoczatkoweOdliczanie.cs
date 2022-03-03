using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.UIElementy
{
    ///<summary>
    /// początkowe odliczanie 5..4..3. etc
    /// </summary>
    public class PoczatkoweOdliczanie : MonoBehaviour
    {
        private Text textField;
        private int liczbaStart=5;
        
        public static Action startGryEvent; 
       
        void Start()
        {
            textField = GetComponent<Text>();
            textField.text = liczbaStart.ToString();
            StartCoroutine(Odliczaj());
        }

        IEnumerator Odliczaj()
        {
            while (liczbaStart>0)
            {
                textField.text = --liczbaStart+"";
                yield return new WaitForSeconds(1f);
                
            }
          
            //START GRY
            startGryEvent?.Invoke();
            textField.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
           
            StopAllCoroutines();
            
        }
    }
    
    
}
