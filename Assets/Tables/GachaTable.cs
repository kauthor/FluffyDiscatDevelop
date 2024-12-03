using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tables
{
    [Serializable]
    public class GachaData
    {
        public int gachaType;
        public int gachaId;
        public int rate;
        public int rewardType;
        public int rewardValue;
        public int rewardCount;
        
    }

    public class GachaDataGroup
    {
        public int GachaId { get; private set; }
        private List<GachaData> _datas;
        public int Sum { get; private set; }

        public void AddData(GachaData data)
        {
            _datas.Add(data);
            Sum += data.rate;
        }
        public GachaDataGroup(int id)
        {
            _datas = new List<GachaData>();
            GachaId = id;
        }

        public List<Tuple<int, int>> GetGachaResults(int amount, bool allowDupli=false)
        {
            List<int> keys = new List<int>();
            List<Tuple<int, int>> ret = new List<Tuple<int, int>>();
            int doubleCheck = 0;
            for (int i = 0; i < amount; i++)
            {
                var rand = Random.Range(0, Sum-doubleCheck);
                int current = 0;

                foreach (var VARIABLE in _datas)
                {
                    if (!allowDupli && keys.Contains(VARIABLE.rewardValue))
                    {
                        continue;
                    }
                    current += VARIABLE.rate;
                    if (rand <= current)
                    {
                        ret.Add(new Tuple<int, int>(VARIABLE.rewardValue, VARIABLE.rewardCount));
                        if (!allowDupli)
                        {
                            keys.Add(VARIABLE.rewardValue);
                            if (!allowDupli)
                            {
                                doubleCheck += VARIABLE.rate;
                            }
                        }
                        break;
                    }
                }
            }

            return ret;
        }

        public Tuple<int, int> GetGachaResult()
        {
            var rand = Random.Range(0, Sum);
            int current = 0;

            foreach (var VARIABLE in _datas)
            {
                current += VARIABLE.rate;
                if (rand <= current)
                    return new Tuple<int, int>(VARIABLE.rewardValue, VARIABLE.rewardCount);
            }
            
            return new Tuple<int, int>(0, 0);
        }
    }
    
    
    public class GachaTableDatas
    {
        private Dictionary<int, GachaDataGroup> gachaDic;

        public GachaDataGroup GetGachaDataGroup(int id)
        {
            var ret = gachaDic.TryGetValue(id, out var val);
            if (ret)
                return val;
            
            return null;
        }
        
        public GachaTableDatas(GachaData[] datas)
        {
            gachaDic = new Dictionary<int, GachaDataGroup>();

            foreach (var dat in datas)
            {
                if (gachaDic.ContainsKey(dat.gachaId))
                {
                    var g = gachaDic[dat.gachaId];
                    g.AddData(dat);
                }
                else
                {
                    GachaDataGroup newGroup = new GachaDataGroup(dat.gachaId);
                    newGroup.AddData(dat);
                    gachaDic.Add(dat.gachaId, newGroup);
                }
            }
        }
    }
    
    [Serializable]
    public class GachaTable
    {
        [SerializeField] private GachaData[] _gachaDatas;

        public GachaData[] RawDatas => _gachaDatas;
    }
}