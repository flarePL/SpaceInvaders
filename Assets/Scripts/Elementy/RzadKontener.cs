using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Elementy
{
    /// <summary>
    /// RzadKontener - tu znajdują się bezpośrednio statki wroga, kontener ten posiada informację czy jest kontenerem strzelającym
    /// tzn czy pozostały jakiekolwiek statki  oraz czy jest to kontener najnizszy 
    /// </summary>
    public class RzadKontener : MonoBehaviour
    {
        public bool czyAktywnieStrzelajacy;
        public int liczbaStatkow;
        public List<EnemyShip> listaStatkowAktywnych;

        public RzadKontener(int liczbaStatkow )
        {
            this.liczbaStatkow = liczbaStatkow;


        }
    }
}