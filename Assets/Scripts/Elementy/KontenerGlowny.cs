using System;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Elementy
{
    /// <summary>
    /// Kontener Glowny i jedyny na scenie, zawierający RzadKontenery, przesuwa statkami wroga 
    /// </summary>
    public class KontenerGlowny:MonoBehaviour
    {
        private Coroutine ruszanieCoroutine;
        public int NajwiekszaLiczbaStatkowWRzedzie { get; set; } = 0;
        private float szerOkna; //tak naprawde to jest to polowa szerokosc
        private int kierunek = 1; //czy w lewo (-1), czy w prawo (+1)sie porusza kontener
        private int PREDKOSC_PORUSZANIA = 30; 
        private void Start()
        {   
           
            Camera cam = Camera.main;
            //mozliwe ze jest na to gotowy wzor //todo sprawdzic to
            szerOkna = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            
        }

        public void ZacznijRuszac()
        {
            ruszanieCoroutine = StartCoroutine("RuszajKonteneremGlownym");
        }
        public void PrzestanRuszac()
        {
            StopCoroutine(ruszanieCoroutine);
           
        }
        public void ZmienKierunek()
        {
             //porusza sie w prawo
            if(transform.position.x>(szerOkna-NajwiekszaLiczbaStatkowWRzedzie/2)) kierunek *= -1;
            //porusza sie w lewo
            if(transform.position.x<-(szerOkna-NajwiekszaLiczbaStatkowWRzedzie/2)) kierunek *= -1;
           

        }
        private IEnumerator RuszajKonteneremGlownym()
        {
            while (true)
            {
                transform.Translate(Vector3.right * kierunek * PREDKOSC_PORUSZANIA * Time.deltaTime);
                ZmienKierunek();
                yield return new WaitForSeconds(.75f);
                
            }
           
        }
        private void OnDestroy()
        {
           
            StopAllCoroutines();
            
        }
        
    }
}