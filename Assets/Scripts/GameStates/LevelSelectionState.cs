using System.Collections;
using System.Collections.Generic;
using SpaceInvaders.Elementy;
using SpaceInvaders.Elementy.Interfaces;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.GameStates
{
    /// <summary>
    /// STAN 1wszy, domyslny, wybór gry  
    /// </summary>
    public class LevelSelectionState : IGameState
    {
        public LevelSelectionState(GraInstaller.WaveLayout[] layouty)
        {
          
        }

        public void Przelacz() 
        {
            
        }

        public void Update()
        {


        }

        public class Factory : PlaceholderFactory<LevelSelectionState>
        {

        }


    }
}
