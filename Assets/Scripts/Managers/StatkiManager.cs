using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SpaceInvaders.Elementy;
using SpaceInvaders.Enums;
using SpaceInvaders.GameStates;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Managers
{
    ///<summary>
    /// tworzy kontener głowny, kontenery rzedy i na końcu tworzy statki
    /// po zniszczonym statku wroga sprawdza który rzad jest aktywnie strzelającym
    ///  </summary>
    /// 
    public class StatkiManager:MonoBehaviour
    {
        public  KontenerGlowny kontenerStatkiGlowny;
        private List<RzadKontener> listaKontenerowRzedow = new List<RzadKontener>();
        public List<int> layoutStatkow;
       
        private float interwalStrzelania;
        public PlayGameState playGameState;
        private Vector3 przesunieciePocisku = new Vector3(0, 0, 0.6f);
        private Coroutine strzelanieCoroutine;
        public void UstawStatki(List<int> layoutStatkow, EnemyShip.Factory enemyShipFactory)
        {
            kontenerStatkiGlowny = new GameObject("KontenerNaWszystkieStatki").AddComponent<KontenerGlowny>();
           
            this.layoutStatkow = layoutStatkow;
            int ileRzedow = layoutStatkow.Count;
        
            int statkowWRzedzie;
       
            RzadKontener rzadKontener = null;
        
       
            for (int i = 0; i < ileRzedow; i++)
            {
                statkowWRzedzie = layoutStatkow[i];
                kontenerStatkiGlowny.NajwiekszaLiczbaStatkowWRzedzie = Math.Max(kontenerStatkiGlowny.NajwiekszaLiczbaStatkowWRzedzie, statkowWRzedzie);
                rzadKontener = new GameObject("KontenerRzad_" + i).AddComponent<RzadKontener>();
                rzadKontener.liczbaStatkow = statkowWRzedzie;
                rzadKontener.listaStatkowAktywnych = new List<EnemyShip>();
                rzadKontener.transform.parent = kontenerStatkiGlowny.transform;
                listaKontenerowRzedow.Add(rzadKontener);
                
                EnemyShip enemyShip;
                
                for (int j = 0; j < statkowWRzedzie; j++)
                {
                    enemyShip = enemyShipFactory.Create();
                    enemyShip.rzadKontener = rzadKontener;
                    enemyShip.DodajDoKontenera(rzadKontener.gameObject);
                    enemyShip.transform.SetPositionAndRotation(new Vector3(j,0,0),Quaternion.identity );  //todo dodac parametr z SO - jaka odleglosc miedzy statkami, j*TaOdleglosc, teraz jest 1
                    rzadKontener.listaStatkowAktywnych.Add(enemyShip);
                }
            }
            //ostatni rzad utworzony (ten najblizej statku) - jest aktywne strzelajacym
            rzadKontener.czyAktywnieStrzelajacy = true;
      
        }
        public void WysrodkujStatki()
        {
            int ODLEGLOSC_X_POMIEDZY_STATKAMI = 1; //todo dodac parametr z SO - jaka odleglosc miedzy statkami, teraz jest 1
            int ODLEGLOSC_Y_POMIEDZY_RZEDAMI = 1; //todo dodac parametr z SO - jaka odleglosc miedzy statkami, teraz jest 1
            int Z_START = 4;
            for (int i = 0; i < listaKontenerowRzedow.Count; i++)
            {
                float szerokosc = (listaKontenerowRzedow[i].liczbaStatkow-1) * ODLEGLOSC_X_POMIEDZY_STATKAMI ;  
                listaKontenerowRzedow[i].gameObject.transform.localPosition =new Vector3(-szerokosc/2,0,Z_START-i*ODLEGLOSC_Y_POMIEDZY_RZEDAMI);
            
            }
        
        
        }
        public void ustawKolejnyRzadAktywnym()
        {
            for (int i = listaKontenerowRzedow.Count-1; i > 0; i--)
            {
                if(listaKontenerowRzedow[i].liczbaStatkow>0){ 
                    listaKontenerowRzedow[i].czyAktywnieStrzelajacy = true;
                    return;
                }
            }
            //przeszedlem przez cala liste i nie mam aktywnych rzedow tzn wszystkie statki zostaly zniszczone, GAME OVER
            print("GAMEOVER");
            playGameState.GameOver(WynikGry.WON);
        
       
        }
        public void OdejmijZniszczonyStatekZRzadKontener(EnemyShip enemyShip)
        {
            enemyShip.rzadKontener.liczbaStatkow--;
            enemyShip.rzadKontener.listaStatkowAktywnych.Remove(enemyShip);
            if (enemyShip.rzadKontener.liczbaStatkow == 0)
            {
                enemyShip.rzadKontener.czyAktywnieStrzelajacy = false;
                ustawKolejnyRzadAktywnym();
            }
        }
        public void PrzestanStrzelac()
        {
            StopCoroutine(strzelanieCoroutine);
        }
        public void ZacznijStrzelac()
        {
            strzelanieCoroutine = StartCoroutine(Strzelaj());
        }
        IEnumerator Strzelaj()
        {
            while (true)
            {
                //wybierz rzad i wylosuj statek z listy, wez jego pozycje
                playGameState.WystrzelPocisk(WylosujPozycjeStrzelajacego()-przesunieciePocisku,Vector3.back);
                yield return new WaitForSeconds(1f);
                
            }
         }
        public Vector3 WylosujPozycjeStrzelajacego()
        {
            //wybierz rzad
            var wylosowanyRzad = from rzad in listaKontenerowRzedow
                where rzad.czyAktywnieStrzelajacy
                select rzad;

            foreach (RzadKontener rzad in wylosowanyRzad)
            {
                int ktory = Random.Range(0, rzad.listaStatkowAktywnych.Count-1);
                Vector3 pozycjaStatku = rzad.listaStatkowAktywnych[ktory].transform.position;
                
                return pozycjaStatku;
            }

            return Vector3.zero;
        }

    }
}