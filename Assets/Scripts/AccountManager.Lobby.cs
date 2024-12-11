using System;
using System.Collections.Generic;
using Tables;
using UnityEngine.Serialization;

namespace FluffyDisket
{
    public partial class AccountManager:CustomSingleton<AccountManager>
    {
        public List<int> PubCharacterKeys;

        private Dictionary<int, int> gymCharacterUpgradeDic;

        private List<int> gymTrainningCharacters;

        public void SyncLobby()
        {
            PubCharacterKeys = new List<int>();
            var amount = //ExcelManager.GetInstance().BaseT.GetBaseDataByIndex(10)?.data ??
                         4;
            var key = //ExcelManager.GetInstance().BaseT.GetBaseDataByIndex(11)?.data ??
                101;

            var characters =
                ExcelManager.GetInstance().GachaDatas.GetGachaDataGroup(key)?.GetGachaResults(amount, false) ??
                new List<Tuple<int, int>>();

            foreach (var c in characters)
            {
                PubCharacterKeys.Add(c.Item1);
            }
            
            if(PubCharacterKeys.Count <=0)
            {
                for(int i=0; i<4; i++)
                   PubCharacterKeys.Add(i+1);
            }

            if (gymCharacterUpgradeDic == null)
            {
                gymCharacterUpgradeDic = new Dictionary<int, int>();
                foreach (var pair in characterOwned)
                {
                    //초기값이 -1이면 없는 캐릭터... 굳이 다른 자료를 또 검색할 이유가 없다.
                    gymCharacterUpgradeDic.Add(pair.Key, pair.Value? 0:-1);
                }
            }

            if (gymTrainningCharacters == null)
            {
                gymTrainningCharacters = new List<int>();
            }
        }

        public void TryGetCharacter(int id, Action onEnd=null)
        {
            if (PubCharacterKeys.Contains(id))
            {
                PubCharacterKeys.Remove(id);
                onEnd?.Invoke();
                characterOwned[id] = true;
            }
        }

        public bool TrainEnable() => gymTrainningCharacters.Count < 10;

        public int TrainningCharacterAmount => gymTrainningCharacters.Count;
        
        public bool TryTrainCharacter(int id)
        {
            if (gymTrainningCharacters.Count >= 10)
                return false;
            if (gymCharacterUpgradeDic.ContainsKey(id))
            {
                gymTrainningCharacters.Add(id);
                return true;
            }

            return false;
        }

        public bool IsTrainning(int id) => gymTrainningCharacters?.Contains(id+1) ?? false;

        public int GetTrainedAmount(int id)
        {
            if (gymCharacterUpgradeDic.TryGetValue(id, out var ret))
            {
                return ret;
            }

            return 0;
        }

        public void TrainFinish()
        {
            foreach (var ch in gymTrainningCharacters)
            {
                gymCharacterUpgradeDic[ch]++;
            }
            
            gymTrainningCharacters.Clear();
        }
    }
}