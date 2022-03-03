using System.Collections.Generic;
using SpaceInvaders.Enums;
using SpaceInvaders.UIElementy;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceInvaders.UIElementy
{
    ///<summary>
    /// posiada referencje do pol tekstowych z wynikiem (aktualizuje wynik gru), licznikiem trwania gry
    /// oraz zamiast tworzenia nowego statnu(GameOver - WON, LOST) wyświetla informację o wyniku rozgrywki 
    /// </summary>
    public class UIGamePlay : MonoBehaviour
    {

        [SerializeField] private Text scoreTextField;
        [SerializeField] private Text enemiesKilledTextField;
        [SerializeField] private Text durationTextField;
        [SerializeField] private Text GameOverText;
        [SerializeField] private Text Won_lost;
        [SerializeField] private Button playAgainBtn;

        private float czasTrwaniaGry;
        private int czasTrwaniaGrySekundy;
        private int czasTrwaniaGryMinuty;
        [Inject] private Counter counter;

        public delegate void PlayAgain(int scenaID, List<int> lista);
        public static PlayAgain onPlayAgain;
        
        private void Start()
        {
            counter.Ustaw(durationTextField);
            playAgainBtn.onClick.AddListener(onPlayAgainClicked);
        }
        void onPlayAgainClicked()
        {
            onPlayAgain?.Invoke(0,new List<int>());
         
        }
        public void UpdateScore(int score)
        {
            scoreTextField.text = "Score: " + score;
            enemiesKilledTextField.text = "Enemies killed: " + score;
        }
        
        public void UpdateEnemiesKilled(int enemiesKilled)
        {
            scoreTextField.text = "Killed: " + enemiesKilled;
        }

        public void ZacznijOdliczac()
        {
           counter.WlaczCounter();
        }
        public void ZakonczOdliczac()
        {
            counter.WylaczCounter();
        }

        public void GameOver(WynikGry wynikGry)
        {
           
            switch (wynikGry)
            {
               case WynikGry.WON:
                    Won_lost.text = "YOU WON";
                    break;
                case WynikGry.LOST:
                    Won_lost.text = "YOU LOST";
                    break;
                   
            }
            GameOverText.gameObject.SetActive(true);
            Won_lost.gameObject.SetActive(true);
            playAgainBtn.gameObject.SetActive(true);
            
        }
       
    }

    
}
