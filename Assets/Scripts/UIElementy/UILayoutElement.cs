using System.Collections.Generic;
using SpaceInvaders.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.UIElementy
{
    ///<summary>
    /// pojedynczy element - button do wyboru layoutu statków wroga
    ///  po jego kliknięciu następuje wczytanie sceny GamePlay z wybranym layoutem 
    /// </summary>
    public class UILayoutElement : MonoBehaviour
    {
        [SerializeField] private RectTransform layoutRzadPrefab;
        [SerializeField] private RectTransform layoutDotPrefab;
        [SerializeField] private RectTransform kontener;


        public void Init(List<int> lista, GameManager gameManager)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                RectTransform element = Instantiate(layoutRzadPrefab, Vector3.zero, Quaternion.identity, kontener);
                UtworzDotki(element, lista[i]);
            }

            //dodaj event do buttona
            gameObject.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(gameManager, lista));

        }

        void ButtonClicked(GameManager gameManager, List<int> layoutStatkow)
        {
            //wczytaj scene z gra, z parametrem w postaci listy o rozlozeniu elementow (statkow wroga)  
            int SCENA_GAMEPLAY = 1;
            gameManager.LoadScene(SCENA_GAMEPLAY, layoutStatkow);

        }

        void UtworzDotki(RectTransform element, int ileKropek)
        {

            for (int i = 0;
                i < Mathf.CeilToInt(ileKropek / 2f);
                i++) //UWAGA dzielimy liczbe statkow przez 2f na wypadek gdyby w grze bylo np 20 statkow w rzedzie, a kropek (symbolizujacych te 20 statkow) by sie tyle nie zmiescilo

            {
                RectTransform dot = Instantiate(layoutDotPrefab, Vector3.zero, Quaternion.identity, element.transform);

            }


            //print("ile wyszlo "+(int) Mathf.Ceil(aaa / 2));
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
