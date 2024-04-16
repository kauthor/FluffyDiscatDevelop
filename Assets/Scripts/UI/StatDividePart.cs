using System;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class StatDividePart:MonoBehaviour
    {
        [SerializeField] private Text txtStatPoint;
        [SerializeField] private Button btnPlus;
        [SerializeField] private Button btnMinus;
        private int multiplyAmount;
        private int initStatPoint;
        private int current;
        public int CurrentSigned => current;

        private Action OnPlus;
        private Action OnMinus;

        public void Init(int multiply, Action onPlus, Action onMinus, int currentStatPoint)
        {
            multiplyAmount = multiply;
            initStatPoint = currentStatPoint;
            current = 0;
            OnPlus = onPlus;
            OnMinus = onMinus;
            btnPlus.onClick.RemoveAllListeners();
            btnPlus.onClick.AddListener(OnPlusClicked);
            btnMinus.onClick.RemoveAllListeners();
            btnMinus.onClick.AddListener(OnMinusClicked);
            btnMinus.enabled = false;
            btnPlus.enabled = true;

            txtStatPoint.text = initStatPoint.ToString();
        }


        private void OnPlusClicked()
        {
            current++;
            OnPlus?.Invoke();
        }

        private void OnMinusClicked()
        {
            current--;
            OnMinus?.Invoke();
        }

        public void UpdateComponents(bool canClickPlus)
        {
            btnPlus.enabled = canClickPlus;
            btnMinus.enabled = current > 0;

            txtStatPoint.text = (initStatPoint + current * multiplyAmount).ToString();
        }
    }
}