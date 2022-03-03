using System;
using System.Collections.Generic;
using SpaceInvaders.Elementy.Interfaces;
using SpaceInvaders.Elementy;
using SpaceInvaders.GameStates;
using SpaceInvaders.UIElementy;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceInvaders.Managers
{   ///<summary>
    /// zarządza stanami i wczytywaniem scen
    /// </summary>
    public class GameManager : MonoBehaviour
    {

        [Inject] private PlayGameState.Factory playGameStateFactory;
        [Inject] private LevelSelectionState.Factory levelSelectionState;
        public static List<int> layoutStatkow { get; private set; }

        private IGameState aktualnyStan;

         public static GameManager instance;

         private void Awake()
         {
            if(instance == null)
             {
                 instance = this;
                 SceneManager.sceneLoaded += OnSceneLoaded;
                 DontDestroyOnLoad(gameObject);
             }else if(instance != this)
             {
                 Destroy(gameObject);
             }
             
         }
         void Start()
        {
            
            IGameState aktualnyStan = levelSelectionState.Create();
            this.aktualnyStan = aktualnyStan;
            UIGamePlay.onPlayAgain = LoadScene;
        }

        public void LoadScene(int scenaID, List<int> layoutStatkow)
        {
            GameManager.layoutStatkow = layoutStatkow;
            SceneManager.LoadScene(scenaID);


        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.buildIndex)
            {
                case 0:
                    aktualnyStan = levelSelectionState.Create();
                    break;
                case 1:
                    aktualnyStan = playGameStateFactory.Create();
                    break;

            }
        }

        public void Update()
        {
           
            aktualnyStan.Update();
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
            
        }
    }
}
