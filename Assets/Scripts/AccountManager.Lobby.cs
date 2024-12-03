using System;
using System.Collections.Generic;
using Tables;
using UnityEngine.Serialization;

namespace FluffyDisket
{
    public partial class AccountManager:CustomSingleton<AccountManager>
    {
        public List<int> PubCharacterKeys;
        
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
    }
}