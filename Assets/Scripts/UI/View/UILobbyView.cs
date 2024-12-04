using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UILobbyView:UIMonoBehaviour
    {
        
        [SerializeField] private Button btnGoGame;
        public override UIType type => UIType.Lobby;

        private void Awake()
        {
            btnGoGame.onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync("Scenes/SelectScene");
            });
        }
    }
}