using UnityEngine;

namespace FluffyDisket.UI
{
    public class UIStageRewardView:UIMonoBehaviour
    {
        public override UIType type => UIType.StageReward;

        public void TryUsePotion(int id, int playerId)
        {
            var character = BattleManager.GetInstance().CurrentPlayerCondition.currentHpDatas[playerId];
            var itemData = AccountManager.GetInstance().TryGetPotion(id);

            if (itemData != null)
            {
                float need = character.maxHp - character.currentHp;
                float consumeEnable = itemData.ItemGauge;
                float consumeAmount = Mathf.Min(need, consumeEnable);

                BattleManager.GetInstance().CurrentPlayerCondition.currentHpDatas[playerId].currentHp =
                    character.currentHp + consumeAmount;

                BattleManager.GetInstance().CurrentPlayerCondition.currentHpDatas[playerId].remainHpPer =
                    BattleManager.GetInstance().CurrentPlayerCondition.currentHpDatas[playerId].currentHp /
                    character.maxHp;
                
                AccountManager.GetInstance().TryConsumePotion(id, (int)consumeAmount);
            }
        }
    }
}