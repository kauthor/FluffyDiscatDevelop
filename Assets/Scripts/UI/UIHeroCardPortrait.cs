using System;
using Tables;
using Tables.Player;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIHeroCardPortrait:UIMonoBehaviour
    {
        [SerializeField] private Button btnOnClicked;
        [SerializeField] private Button btnOnFire;
        [SerializeField] private Button btnInven;
        [SerializeField] private Text txtJob;
        [SerializeField] private GameObject btnArea;

        private Action<CharacterData> OnClickCard;
        private Action<CharacterData> OnClickFire;
        private Action<CharacterData> OnClickInven;
        private CharacterData data;

        private void Awake()
        {
            btnOnClicked.onClick.RemoveAllListeners();
            btnOnClicked.onClick.AddListener(()=>
            {
                btnArea.gameObject.SetActive(true);
                OnClickCard?.Invoke(data);
            });
            
            btnOnFire.onClick.RemoveAllListeners();
            btnOnFire.onClick.AddListener(()=>
            {
                btnArea.gameObject.SetActive(false);
                OnClickFire?.Invoke(data);
            });
            
            btnInven.onClick.RemoveAllListeners();
            btnInven.onClick.AddListener(()=>
            {
                btnArea.gameObject.SetActive(false);
                OnClickInven?.Invoke(data);
            });
            btnArea.gameObject.SetActive(false);
        }

        public void Init(int id, Action<CharacterData> onClicked=null)
        {
            data = ExcelManager.GetInstance().CharT.GetCharData(id);
            txtJob.text = ((Job) data.job).ToString();

            OnClickCard = onClicked;
        }

        public void InitHandler(Action<CharacterData> onfire = null, Action<CharacterData> onInven = null)
        {
            OnClickFire = onfire;
            OnClickInven = onInven;
        }
    }
}