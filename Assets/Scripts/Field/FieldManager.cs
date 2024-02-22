using System;
using FluffyDisket.UI;
using UnityEngine;

namespace FluffyDisket.Field
{
    public class FieldManager:MonoBehaviour
    {
        private void Start()
        {
            UIManager.GetInstance()?.ChangeView(UIType.LobbyTeamSelect).Init(new UIViewParam());
        }
    }
}