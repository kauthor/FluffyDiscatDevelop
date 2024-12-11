using System;
using Tables;
using Tables.Player;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIHeroGymCard:UIMonoBehaviour
    {

        [SerializeField] private Text txtJob;
        [SerializeField] private GameObject bgDefault;
        [SerializeField] private GameObject bgTrainning;

        [SerializeField] private Button btnClick;
        [SerializeField] private GameObject txtTrainning;

        private bool isTrainning;
        private CharacterData currentdata;
        private Action<CharacterData> OnClicked;

        private void Awake()
        {
            btnClick.onClick.RemoveAllListeners();
            btnClick.onClick.AddListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            OnClicked?.Invoke(currentdata);
        }

        public void Init(bool istrainning, int data, Action<CharacterData> onclick=null)
        {
            currentdata = ExcelManager.GetInstance().CharT.GetCharData(data);
            isTrainning = istrainning;
            if (istrainning)
            {
                bgTrainning.gameObject.SetActive(true);
                bgDefault.gameObject.SetActive(false);
                btnClick.enabled = false;
            }
            else
            {
                bgTrainning.gameObject.SetActive(false);
                bgDefault.gameObject.SetActive(true);
                btnClick.enabled = true;
            }
            txtJob.text = ((Job) currentdata.job).ToString();

            txtTrainning.gameObject.SetActive(isTrainning);
            OnClicked = onclick;
        }
    }
}