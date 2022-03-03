using Interfaces;
using Zenject;

namespace SpaceInvaders.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityObject=UnityEngine.Object;
   
        public class ObjectPool<T> where T : MonoBehaviour , IFromPooler<T>
        {
            //gdzie T - typ przechowywanych/tworzonych objektow
            private Stack<T> stos = new Stack<T>();
            private PlaceholderFactory<T> prefabFactory;
            public ObjectPool(PlaceholderFactory<T> prefabFactory)
            {
               this.prefabFactory = prefabFactory;
            }
            public T Wydaj()
            {
                if (stos.Count > 0)
                { 
                    return stos.Pop();
                }
                return UtworzObiekt();
               
            }

            public T UtworzObiekt()
            {
                T tempObject = prefabFactory.Create();
                tempObject.objectPool = this;
                return tempObject;
            }

           
            public void Odbierz(T element)
            {
               stos.Push(element);  
            }
           
        
        }

       
       
    
   
}