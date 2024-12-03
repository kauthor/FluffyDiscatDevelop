using System;
using System.Collections.Generic;
using FluffyDisket;
using FluffyDisket.UI;
using Tables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPubPopup : PopupMonoBehavior
{
    public override PopupType type => PopupType.Pub;

    private List<int> LobbyData;
    [SerializeField] private Toggle[] toggleCharacter;
    [SerializeField] private Button btnPrev;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnGet;
    [SerializeField] private Text txtGold;
    private int currentIndex = 0;
    private int currentPrice = 0;
    private int currentCharacterID;
    public static void OpenPopup()
    {
        var pop = PopupManager.GetInstance().GetPopup(PopupType.Pub);
        if (pop == null)
            return;
        if (pop is UIPubPopup pub)
        {
            pub.Init();
            pub.gameObject.SetActive(true);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(() =>
        {
            OnClickPrevNext(false);
        });
        
        btnPrev.onClick.RemoveAllListeners();
        btnPrev.onClick.AddListener(() =>
        {
            OnClickPrevNext(true);
        });
        
        btnGet.onClick.RemoveAllListeners();
        btnGet.onClick.AddListener(GetCharacter);

        foreach (var t in toggleCharacter)
        {
            t.onValueChanged.RemoveAllListeners();
            t.onValueChanged.AddListener(_=>
            {
                if (_)
                {
                    //t.enabled = false;
                    int id = 0;
                    for (int i=0; i< toggleCharacter.Length; i++)
                    {
                        var toggle = toggleCharacter[i];
                        if (toggle != t)
                        {
                            
                        }
                        else
                        {
                            id = i;
                        }
                    }

                    currentIndex = id;
                    btnNext.enabled = LobbyData.Count > currentIndex + 1;
                    btnPrev.enabled = currentIndex!=0;
                    var characterData = ExcelManager.GetInstance().CharT.GetCharData(LobbyData[id]);
                    txtGold.text = characterData.Price.ToString();

                    currentCharacterID = LobbyData[id];
                    currentPrice = characterData.Price;
                }
            });
        }
    }

    private void OnClickPrevNext(bool prev)
    {
        var length = LobbyData.Count;
        if ( (length > currentIndex + 1 && !prev) || (currentIndex != 0 && prev))
        {
            currentIndex += prev ? -1 : 1;
            toggleCharacter[currentIndex].Select();
            btnNext.enabled = length > currentIndex + 1;
            btnPrev.enabled = currentIndex != 0;
        }
    }


    private void GetCharacter()
    {
        var gold = AccountManager.GetInstance().GetOwnedGold();
        if (gold >= currentPrice)
        {
            AccountManager.GetInstance().TryGetCharacter(currentCharacterID, () =>
            {
                AccountManager.GetInstance().UseGold(currentPrice);
                SyncFromLobby();
            });
        }
    }

    private void Init()
    {
        SyncFromLobby();
    }

    private void SyncFromLobby()
    {
        LobbyData = AccountManager.GetInstance().PubCharacterKeys;
        for (int i = 0; i < toggleCharacter.Length; i++)
        {
            toggleCharacter[i].gameObject.SetActive(LobbyData.Count >i);
        }

        currentIndex = 0;
        btnNext.enabled = LobbyData.Count > 1;
        btnPrev.enabled = false;
        toggleCharacter[0].Select();
    }
}