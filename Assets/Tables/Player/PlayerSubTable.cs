using System;
using FluffyDisket;
using UnityEngine;

namespace Tables.Player
{
    [CreateAssetMenu]
    public class PlayerSubTable:ScriptableObject
    {
        [SerializeField] public CharacterStat stat;
    }
}