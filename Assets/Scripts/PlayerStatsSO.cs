using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CelikenVP
{
    public enum StatType
    {
        Might,
        Armor,
        MaxHealth,
        Recovery,
        Cooldown,
        Area,
        Speed,
        Duration,
        Amount,
        MoveSpeed,
        Magnet,
        Luck,
        Growth,
        Greed,
        Curse,
        Charm,
    }

    public enum StackingMode
    {
        Additive,
        Multiplicative,
    }

    [CreateAssetMenu(menuName = "Player Stats")]
    public class PlayerStatsSO : SerializedScriptableObject
    {
        public class Stat
        {
            [SerializeField] private StatType type;
            [SerializeField] private StackingMode stacking;
            [SerializeField] private float baseValue = 0f;
            [SerializeField] private float maxValue = -1f;
            [SerializeField] private float currentValue;

            private List<Upgrade> listUpgrade = new();

            public StatType StatType { get { return type; } }
            public StackingMode StackingMode { get { return stacking; } }
            public float BaseValue { get { return baseValue; } }
            public float MaxValue { get { return maxValue; } }
            public float CurrentValue { get { return maxValue < 0f ? currentValue : Mathf.Clamp(currentValue, baseValue, maxValue); } }

            public void ComputeValue()
            {
                float additive = 0f;
                float multiplicative = 1f;
                if (listUpgrade != null)
                    foreach (var upgrade in listUpgrade)
                    {
                        if (upgrade.Stacking == StackingMode.Additive)
                            additive += upgrade.UpgradeValue;
                        if (upgrade.Stacking == StackingMode.Multiplicative)
                            multiplicative += upgrade.UpgradeValue;
                    }
                currentValue = (baseValue + additive) * multiplicative;
            }

            public void AddUpgrade(Upgrade upgrade)
            {
                listUpgrade ??= new List<Upgrade>();
                listUpgrade.Add(upgrade);
                ComputeValue();
            }

            public void ClearStatUpgrades()
            {
                listUpgrade?.Clear();
                ComputeValue();
            }
        }

        [SerializeField] private Dictionary<StatType, Stat> stats = new();

        public Dictionary<StatType, Stat> Stats { get { return stats; } }

        public Stat GetStat(StatType type)
        {
            return stats[type];
        }

        public void ClearStatsUpgrades()
        {
            foreach (Stat stat in stats.Values)
            {
                stat.ClearStatUpgrades();
            }
        }
    }
}