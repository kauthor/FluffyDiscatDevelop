using System;
using FluffyDisket.Substance;
using UnityEngine;

namespace Tables
{
    [Serializable]
    public struct SubstanceEffectOption
    {
        public int type;
        public int value1;
        public int value2;
    }
    
    [Serializable]
    public struct SubstanceOption
    {
        public int id;
        public int nameId;
        public EffectType effectType;
        public ResetType resetType;
        public int duration;

        public SubstanceEffectOption[] options;
        public int parentEffect;
        public int group;
    }
    
    [CreateAssetMenu]
    public class SubstanceTable :ScriptableObject
    {
        [SerializeField] private SubstanceOption[] substanceOptions;

        public void SetSubstanceData(SubstanceOption[] arr)
            => substanceOptions = arr;

        public SubstanceOption GetSubstanceOption(int id)
        {
            if (id > substanceOptions.Length)
                return new SubstanceOption();

            return substanceOptions[id - 1];
        }
    }
}