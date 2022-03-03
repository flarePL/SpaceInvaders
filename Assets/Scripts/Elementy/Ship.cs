using System;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.Elementy
{
    /// <summary>
    /// 'Nasz' statek tzw. Player, tu tylko sterowanie statkiem prawo, lewo  
    /// </summary>
    public class Ship : MonoBehaviour, IShooter, IDamagable
    {
        public Vector3 przesunieciePocisku { get; }= new Vector3(0, 0, 1.0f);
        
        [Inject]
        private GraInstaller.StatekSettings _settings;

      
        [Inject]
        public void Construct()
        {
            //Debug.Log("Construct statek " + s);
            //statek = s;

        }
        private void Start()
        {
            transform.position = new Vector3(0,0,-4f);
           
        }

        public void Shoot()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }

        public void MoveLeft()
        {
            transform.Translate(Vector2.left *2f*  Time.deltaTime, Space.World);
        }

        public void MoveRight()
        {
            transform.Translate(Vector2.right *2f*  Time.deltaTime, Space.World);
        }
       


        public void Strzelaj()
        {
            throw new NotImplementedException();
        }
    }
   
    
}
