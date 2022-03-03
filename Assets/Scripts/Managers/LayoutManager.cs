using SpaceInvaders.Managers;
using SpaceInvaders.UIElementy;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceInvaders.Managers
{
    ///<summary>
    /// Tworzy Layouty -  ustawienia statków wroga
    /// jesli lista layoutów jest długa, wyświetla strzałki/buttony do przewijania layoutów
    /// </summary>
    public class LayoutManager : MonoBehaviour
    {

        [SerializeField] private RectTransform kontenerNaLayouty;
        [SerializeField] private UILayoutElement layoutElementPrefab;
        [SerializeField] private GameObject strzalkaPrawa;
        [SerializeField] private GameObject strzalkaLewa;

        [Inject] private GraInstaller.WaveLayout[] layouty;

        [Inject] private GameManager gameManager;

        private float SZEROKOSC_JEDNEGO_LAYOUTU;
        private float PADDING_POMIEDZY_ELEMENTAMI;
        private float szerOkna;
        private float zajmowanaSzerokoscElementow;

        void Start()
        {

            strzalkaPrawa.SetActive(false);
            strzalkaLewa.SetActive(false);
            szerOkna = kontenerNaLayouty.rect.width;
            SZEROKOSC_JEDNEGO_LAYOUTU = layoutElementPrefab.GetComponent<RectTransform>().rect.width;
            PADDING_POMIEDZY_ELEMENTAMI = kontenerNaLayouty.GetComponent<HorizontalLayoutGroup>().spacing;

            UtworzLayouty();
            ZapalUkryjStrzalki();

        }

        public void onStrzalkaLeftClick()
        {
            PrzesunKontener(150);
            ZapalUkryjStrzalki();

        }

        public void onStrzalkaRightClick()
        {
            PrzesunKontener(-150);
            ZapalUkryjStrzalki();
        }

        public void ZapalStrzalkaPrawa()
        {
            strzalkaPrawa.SetActive(true);
        }

        public void UkryjStrzalkaPrawa()
        {
            strzalkaPrawa.SetActive(false);
        }

        public void ZapalStrzalkaLewa()
        {
            strzalkaLewa.SetActive(true);
        }

        public void UkryjStrzalkaLewa()
        {
            strzalkaLewa.SetActive(false);
        }

        void PrzesunKontener(int X)
        {
            Vector2 poz = kontenerNaLayouty.anchoredPosition;
            poz.x += X;
            kontenerNaLayouty.anchoredPosition = poz;
        }

        void UtworzLayouty()
        {

            UILayoutElement element;
            for (int i = 0; i < layouty.Length; i++)
            {
                element = Instantiate(layoutElementPrefab, Vector3.zero, Quaternion.identity, kontenerNaLayouty);
                //wpisuje dane o rozkladzie statkow np 5,5,5
                element.Init(layouty[i].waveLayout, gameManager);
            }

            zajmowanaSzerokoscElementow = layouty.Length * SZEROKOSC_JEDNEGO_LAYOUTU +
                                          (layouty.Length - 1) * PADDING_POMIEDZY_ELEMENTAMI;
        }


        void ZapalUkryjStrzalki()
        {

            if (zajmowanaSzerokoscElementow + kontenerNaLayouty.anchoredPosition.x > szerOkna) ZapalStrzalkaPrawa();
            else UkryjStrzalkaPrawa();

            if (kontenerNaLayouty.anchoredPosition.x < 0) ZapalStrzalkaLewa();
            else UkryjStrzalkaLewa();
        }
    }
}