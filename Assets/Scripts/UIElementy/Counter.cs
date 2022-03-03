using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceInvaders.UIElementy
{
    ///<summary>
    /// odlicza czas trwania gry
    /// </summary>
    public class Counter : MonoBehaviour
    {
        private float czasTrwaniaGry;
        private int czasTrwaniaGrySekundy;
        private int czasTrwaniaGryMinuty;
        private Text durationTextField;
        
        //[Inject]  private Text durationTextField;   Nie udało mi się 'wstrzyknąć tej zależnosci :(  Musiałem zrobić to bez Zenject'a
        public void Ustaw(Text durationTextField)
        {
            this.durationTextField = durationTextField;

        }
        public void WlaczCounter()
        {
            czasTrwaniaGry = 0;
            StartCoroutine(Odliczaj());
            
        }
        public void WylaczCounter()
        {
            StopCoroutine(Odliczaj());
            
        }

        private IEnumerator Odliczaj()
        {
            while (true)
            {
                czasTrwaniaGry += 1;
                czasTrwaniaGryMinuty = (int)czasTrwaniaGry / 60;
                czasTrwaniaGrySekundy = (int)czasTrwaniaGry % 60;
                durationTextField.text = "Time: " + czasTrwaniaGryMinuty + "m" + " " + czasTrwaniaGrySekundy + "s";
                
                yield return new WaitForSeconds(1f);
                
            }
            
            
        }
    }
}