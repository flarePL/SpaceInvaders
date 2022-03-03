using System;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.Elementy
{
    /// <summary>
    /// Statek wroga
    /// Poruszaniem statków odpowiedzalny jest KontenerGlowny
    /// Strzelaniem - PlayGameState
    /// W przypadku rozbudowy gry (o nowe statki, statki które nie giną od jednego pocisku), zalecane wykorzystanie interfejsów 
    /// </summary>
    public class EnemyShip : MonoBehaviour
    {

        public RzadKontener rzadKontener;
       
        public void DodajDoKontenera(GameObject kontener)
        {
            this.transform.parent = kontener.transform;

        }
        
        public class Factory: PlaceholderFactory<EnemyShip>
        {
        }
    }
   
    
}