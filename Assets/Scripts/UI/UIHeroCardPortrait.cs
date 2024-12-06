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
        [SerializeField] private Text txtJob;

        private Action<CharacterData> OnClickCard;
        private CharacterData data;

        private void Awake()
        {
            btnOnClicked.onClick.RemoveAllListeners();
            btnOnClicked.onClick.AddListener(()=> OnClickCard?.Invoke(data));
        }

        public void Init(int id, Action<CharacterData> onClicked=null)
        {
            data = ExcelManager.GetInstance().CharT.GetCharData(id);
            txtJob.text = ((Job) data.job).ToString();

            OnClickCard = onClicked;
        }
    }
}