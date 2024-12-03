using System;
using System.Collections.Generic;
using Tables;

namespace FluffyDisket
{
    public partial class AccountManager:CustomSingleton<AccountManager>
    {
        public List<int> PupCharacterKeys;
        
        public void SyncLobby()
        {
            PupCharacterKeys = new List<int>();
            var amount = //ExcelManager.GetInstance().BaseT.GetBaseDataByIndex(10)?.data ??
                         4;
            var key = //ExcelManager.GetInstance().BaseT.GetBaseDataByIndex(11)?.data ??
                101;

            var characters =
                ExcelManager.GetInstance().GachaDatas.GetGachaDataGroup(key)?.GetGachaResults(amount, false) ??
                new List<Tuple<int, int>>();

            foreach (var c in characters)
            {
                PupCharacterKeys.Add(c.Item1);
            }
            
            if(PupCharacterKeys.Count <=0)
                PupCharacterKeys.Add(1);
        }
    }
}