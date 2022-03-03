
using SpaceInvaders.Elementy;
using SpaceInvaders.Enums;
using SpaceInvaders.Elementy.Interfaces;
using SpaceInvaders.Managers;
using SpaceInvaders.UIElementy;
using SpaceInvaders.Utils;
using UnityEngine;
using Zenject;

using Vector3 = UnityEngine.Vector3;

namespace SpaceInvaders.GameStates
{
    /// <summary>
    /// Stan własciwy gry, posiada informacje o UI, kontenerze ze statkami wroga, Playerze, ObjectPoolerze dla pocisków
    /// tu odbywa się zarzadzanie w/w elementami 
    /// </summary>
    public class PlayGameState : IGameState
    {

        private Ship statek;
        private ObjectPool<Pocisk> pociskiObjectPool;
        private GameManager gameManager;
        private bool czyGraAktywna;
        private int score;
        private UIGamePlay UIgamePlay;
        private StatkiManager statkiManager;


        //Injected przez Zenject, dodałem ten komentarz, ponieważ nie jest oczywiste że konstruktor PlayGameState otrzymuje referencje do obiektow prefab Ship, Pocisk
        public PlayGameState(Ship statek, Pocisk.Factory pociskFactory, GameManager gameManager,
            EnemyShip.Factory enemyShipFactory) //,UIGamePlay UIgamePlay)
        {
            this.statek = statek;
            this.gameManager = gameManager;

            pociskiObjectPool = new ObjectPool<Pocisk>(pociskFactory);
            PoczatkoweOdliczanie.startGryEvent += StartGry;
            UIgamePlay = GameObject.Find("UIGamePlay").GetComponent<UIGamePlay>();
            statkiManager = new GameObject("StatkiManager").AddComponent<StatkiManager>();
            statkiManager.UstawStatki(GameManager.layoutStatkow, enemyShipFactory);
            statkiManager.WysrodkujStatki();
            statkiManager.playGameState = this;
        }

        public void WystrzelPocisk(Vector3 pozycja, Vector3 kierunek)
        {
            Pocisk pocisk = pociskiObjectPool.Wydaj();
            pocisk.Odpal(pozycja, kierunek);
            pocisk.playGameState = this;

        }

        void StartGry()
        {
            PoczatkoweOdliczanie.startGryEvent -= StartGry;
            czyGraAktywna = true;
            statkiManager.kontenerStatkiGlowny.ZacznijRuszac();
            statkiManager.ZacznijStrzelac();
            UIgamePlay.ZacznijOdliczac();
        }

        public void GameOver(WynikGry wynikGry)
        {

            czyGraAktywna = false;
            statkiManager.kontenerStatkiGlowny.PrzestanRuszac();
            statkiManager.PrzestanStrzelac();
            UIgamePlay.ZakonczOdliczac();
            UIgamePlay.GameOver(wynikGry);
        }


        public void Zderzenie(string msg)
        {
            switch (msg)
            {
                case "Enemies":
                    score++;
                    UIgamePlay.UpdateScore(score);
                    break;
                case "Player":
                    GameOver(WynikGry.LOST);
                    break;


            }
        }


        public void OdejmijZniszczonyStatekZRzadKontener(EnemyShip enemyShip)
        {
            statkiManager.OdejmijZniszczonyStatekZRzadKontener(enemyShip);
        }


        public void Print(string msg)
        {
            Debug.Log(msg);
        }

        public void Update()
        {
            //todo przeniesc do sterowania
            if (!czyGraAktywna) return;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                statek.MoveLeft();
            } 
            if (Input.GetKey(KeyCode.RightArrow))
            {
                statek.MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Pocisk pocisk = pociskiObjectPool.Wydaj();
                pocisk.Odpal(statek.transform.position + statek.przesunieciePocisku, Vector3.forward);
                pocisk.playGameState = this;
            }

        }

        public class Factory : PlaceholderFactory<PlayGameState>
        {

        }



    }
}
