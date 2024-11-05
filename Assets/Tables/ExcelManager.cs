using System;
using FluffyDisket;
using Tables.Player;
using UnityEngine;

namespace Tables
{
    [Serializable]
    public class ExcelManager:CustomSingleton<ExcelManager>
    {
        [SerializeField] private BaseTable _baseTable;
        [SerializeField] private CharacterTable _charTable;
        [SerializeField] private CharNameTable _charNameTable;
        //[SerializeField] private PlayerSubTable[] _playerSubTables;
        [SerializeField] private ExpTable _expTable;
        [SerializeField] private MonsterGroupTable _monsterGroupTable;
        [SerializeField] private MonsterLevelTable _monsterLevelTable;
        [SerializeField] private MonsterTable _monsterTable;
        [SerializeField] private StageTable _stageTable;
        [SerializeField] private TraitTable _traitTable;

        [SerializeField] private SubstanceTable _substanceTable;
        [SerializeField] private ItemTable _itemTable;
        //public void SetCharTable

        public ItemTable ItemT => _itemTable;
        public BaseTable BaseT => _baseTable;
        public CharacterTable CharT => _charTable;
        public CharNameTable CharNameT => _charNameTable;
        public ExpTable ExpT => _expTable;
        public MonsterGroupTable MonsterGroupT => _monsterGroupTable;
        public MonsterLevelTable MonsterLevelT => _monsterLevelTable;
        public MonsterTable MonsterT => _monsterTable;
        public StageTable StageT => _stageTable;

        public TraitTable TraitT => _traitTable;

        public SubstanceTable SubstanceT => _substanceTable;
    }
}