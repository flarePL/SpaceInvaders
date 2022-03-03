using System;
using System.Collections.Generic;
using SpaceInvaders.Elementy;
using SpaceInvaders.Managers;
using SpaceInvaders.GameStates;
using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "GraSettingsInstaller", menuName = "Installers/GraInstaller")]
public class GraInstaller : ScriptableObjectInstaller
{
    public StatekWrogaSettings statekWrogaSettings;
    public StatekSettings statekSettings;
    public PociskSettings pociskSettings;
    public Ship statekPrefab;
    public Pocisk pociskPrefab;
    public EnemyShip statekWrogaPrefab;
    
    //UI
   
    
    public WaveLayout[] layouty;// = new WaveLayout[2]{};

    //public WaveLayout[] layouty2 = new[] { new WaveLayout(new List<int>(1,2,3))   };
    //przyklad 5,5,5,5    1,3,5     10      ile elementow w rzedzie
    //5,5,5,5 - 4 rzedy, w kazdym po 5 statkow wroga
    //1,3,5 - 3 rzedy, w 1wszym rzedzie(liczonym od góry) 1 statek, w 2gim - 3 statki, w 3cim - 5 statkow 
    //10    - tylko jeden rzad z 10cioma statkami wroga
    public override void InstallBindings()
    {   
        
       Container.Bind<Ship>().FromComponentInNewPrefab(statekPrefab).AsSingle();
       Container.Bind<Pocisk>().FromComponentInNewPrefab(pociskPrefab).AsSingle();
       Container.Bind<StatekSettings>().FromInstance(statekSettings).AsSingle();
       Container.Bind<StatekWrogaSettings>().FromInstance(statekWrogaSettings).AsSingle();
       Container.Bind<PociskSettings>().FromInstance(pociskSettings).AsSingle();
       Container.Bind<WaveLayout[]>().FromInstance(layouty).AsSingle();
       Container.BindFactory<PlayGameState, PlayGameState.Factory>().AsSingle();//.WhenInjectedInto<ShipStateFactory>();
       
       
       Container.BindFactory<LevelSelectionState, LevelSelectionState.Factory>().AsSingle();//.WhenInjectedInto<ShipStateFactory>();
       Container.BindFactory<Pocisk, Pocisk.Factory>().FromComponentInNewPrefab(pociskPrefab).UnderTransformGroup("Pociski");//.AsSingle();
       Container.BindFactory<EnemyShip, EnemyShip.Factory>().FromComponentInNewPrefab(statekWrogaPrefab)
           .UnderTransformGroup("StatkiWroga");
       Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
       //nie dziala Container.Bind<UIGamePlay>().FromComponentInHierarchy().AsSingle(); :(

      
    }


    [Serializable]
    public class StatekSettings
    {
        public float predkoscStatku;
        public float interwalStrzelania;
    }
    [Serializable]
    public class StatekWrogaSettings
    {
        public float poczatkowaPredkoscStatkuNaBoki;
        public float interwalStrzelania;
    }
    [Serializable]
    public class PociskSettings
    {
        public float predkosc;
        public float czasDoUsuniecia;
    }
   
    [Serializable]
    public class WaveLayout
    {   
        public List<int> waveLayout = new List<int>{5,5,5} ;

        public WaveLayout(List<int> lista)
        {
            waveLayout = lista;

        }

    }

}