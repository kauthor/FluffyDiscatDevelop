using System;
using System.Collections.Generic;
using FluffyDisket.UI;
using Tables.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FluffyDisket.Field
{
    public class FieldManager:MonoBehaviour
    {
        [SerializeField] private LobbyUnit[] unitPrefs;

        [SerializeField] private Transform field;
        //추후 리소스 매니저, 유닛 로더를 만들어서 거기서 불러오자. 지금은...일단 직렬화필드로

        private List<LobbyUnit> managedUnits;

        private void Awake()
        {
            managedUnits = new List<LobbyUnit>();
        }

        private void Start()
        {
            UIManager.GetInstance()?.ChangeView(UIType.LobbyTeamSelect).Init(new UIViewParam());

            var heroOwn = AccountManager.GetInstance().CharacterOwned;

            foreach (var pair in heroOwn)
            {
                if (pair.Value && unitPrefs.Length > pair.Key)
                {
                    var newUnit = Instantiate(unitPrefs[pair.Key], field, true);
                    newUnit.transform.position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0);
                    managedUnits.Add(newUnit);
                }
            }
        }
    }
}