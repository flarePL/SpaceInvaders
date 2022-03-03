using System;
using System.Collections;
using Interfaces;
using SpaceInvaders.GameStates;
using SpaceInvaders.Utils;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.Elementy
{
    /// <summary>
    /// Pocisk w grze, statki wroga jak i 'nasz statek' strzelają tymi samymi pociskami, pocis dezaktywuje się po jakimś określonym czasie
    /// inne rozwiązanie to pocisk który zostaje usunięty po kolizji z oiektem poza sceną, tworzeniem pocisków zarzadza ObjectPooler 
    /// </summary>
    /// 
    public class Pocisk : MonoBehaviour,IFromPooler<Pocisk>
    {
        public ObjectPool<Pocisk> objectPool { get; set; }
        private bool czyAktywny;
        private Vector3 kierunek;
        private float predkosc = 2f;  //todo z SO
            
        [Inject]
        private GraInstaller.PociskSettings _settings;
        
        [SerializeField]
        private float czasDoUsuniecia;
       
        public PlayGameState playGameState; 
      

        [Inject]
        public void Construct()
        {
            czasDoUsuniecia = _settings.czasDoUsuniecia;
            transform.gameObject.SetActive(true);
        }

        public void Odpal(Vector3 pozycjaStartowa, Vector3 kierunek)
        {
            transform.position = pozycjaStartowa;
            this.kierunek = kierunek;
            transform.gameObject.SetActive(true);
            czyAktywny = true;
           
            StartCoroutine(odliczajDoSamodestrukcji());
        }
        
        IEnumerator odliczajDoSamodestrukcji()
        {
            yield return new WaitForSeconds(czasDoUsuniecia);
            czyAktywny = false;
            zwrocDoPoolera();
        }
        private void zwrocDoPoolera()
        {
            transform.gameObject.SetActive(false);
            objectPool.Odbierz(this);
        }
        private void Update()
        {
            if (czyAktywny)
            {
               transform.Translate(kierunek * predkosc * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemies")
            {
                playGameState.Zderzenie("Enemies");
                playGameState.OdejmijZniszczonyStatekZRzadKontener(other.gameObject.GetComponent<EnemyShip>());
                zwrocDoPoolera();
                Destroy(other.gameObject);
            
            }

            if (other.gameObject.tag == "Player")
            {
                playGameState.Zderzenie("Player");
            }
             

        }

        public class Factory : PlaceholderFactory<Pocisk>
        {
       
        }
    }
}

