using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyDisket
{
    public class AccountManager:CustomSingleton<AccountManager>
    {
        private Dictionary<int, bool> characterOwned;

        public Dictionary<int, bool> CharacterOwned => characterOwned;
        protected override void Awake()
        {
            base.Awake();
            characterOwned = new Dictionary<int, bool>();
            
            //차후 계정 정보를 본격적으로 받아오면, 그때  제대로 초기화한다.

            for(int i=0; i<3; i++)
            {
                characterOwned.Add(i,true);
            }
        }
    }
}