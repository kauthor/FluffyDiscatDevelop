using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UIHpBar: MonoBehaviour
    {
        [SerializeField] private Image fillArea;

        private BattleUnit owner;

        public void Init(BattleUnit unit)
        {
            fillArea.fillAmount = unit.isPlayer? unit.currentHp/unit.MaxHp : 1;
            owner = unit;
            gameObject.SetActive(true);
            owner.onOwnerUpdate -= OnOwnerUpdate;
            owner.onOwnerUpdate += OnOwnerUpdate;
            owner.onHpUpdate -= OnHpUpdate;
            owner.onHpUpdate += OnHpUpdate;
            owner.OnOwnerDead -= OnOwnerDead;
            owner.OnOwnerDead += OnOwnerDead;
        }

        private void OnHpUpdate(float per)
        {
            fillArea.fillAmount = per;
        }

        private void OnOwnerUpdate(BattleUnit unit)
        {
            transform.position = Camera.main.WorldToScreenPoint(unit.transform.position + new Vector3(0, 1.2f, 0));
        }

        private void OnOwnerDead()
        {
            gameObject.SetActive(false);
        }
    }
}