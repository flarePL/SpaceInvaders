using SpaceInvaders.Elementy;
using SpaceInvaders.Utils;
using UnityEngine;

namespace Interfaces
{    /// <summary>
    /// zastosowanie w ObjectPollerze, obiekt który jest tworzony przez ObjectPoolera musi posiadac inforacje o ... ObjectPoolerze
    /// aby do niego wrócić
    /// </summary>
    public interface IFromPooler<T> where T : MonoBehaviour,IFromPooler<T>
    {
       ObjectPool<T> objectPool { get; set; }
    }
}