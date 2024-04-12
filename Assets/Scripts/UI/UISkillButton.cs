using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace FluffyDisket.UI
{
    public class UISkillButton:MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text name;
        [SerializeField] private Image gauge;
        private BattleUnit owner;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                if(owner)
                   owner.SkillUse(true);
            });
        }

        public void Init(BattleUnit unit)
        {
            owner = unit;
            name.text = owner.CharacterClassPublic.ToString();
            owner.onOwnerUpdate -= OnOwnerUpdate;
            owner.onOwnerUpdate += OnOwnerUpdate;
        }

        private void OnOwnerUpdate(BattleUnit unit)
        {
            //gauge.fillAmount = unit.SkillCoolRegain / unit.CharacterAbility.SkillCoolTIme;
        }

        public void Dispose()
        {
            if (owner)
                owner.onOwnerUpdate -= OnOwnerUpdate;
            button.onClick.RemoveAllListeners();
            owner = null;
        }
    }
}