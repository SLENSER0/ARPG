using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class XPTranslationTableEntry
{
    public int Level;
    public int XPRequired;
}

[CreateAssetMenu(menuName = "XP Table", fileName = "XPTranslation_Table")]
public class XPTranslation_Table : BaseXPTranslation
{
    [SerializeField] private List<XPTranslationTableEntry> Table;

    public override bool AddXP(int amout)
    {
        if (AtLevelCap)
        {
            return false;
        }
        CurrentXp += amout;
        foreach (var entry in Table)
        {
            if (CurrentXp >= entry.XPRequired)
            {
                if (entry.Level != CurrentLevel)
                {
                    CurrentLevel = entry.Level;

                    AtLevelCap = Table[^1].Level == CurrentLevel;
                    return true;
                }
                break;
            }
                
        }

        return false;
    }

    public override void SetLevel(int level)
    {
        foreach (var entry in Table)
        {
            if (entry.Level == level)
            {
                AddXP(entry.XPRequired);
                return;
            }
        }

        throw new System.ArgumentOutOfRangeException($"Could not find any entry for level {level}");
    }

    protected override int GetXPRequiredForNextLevel()
    {
        if (AtLevelCap)
            return int.MaxValue;
        for (int index = 0; index < Table.Count; ++index)
        {
            var entry = Table[index];
            if (Table[index].Level == CurrentLevel)
            {
               return Table[index + 1].XPRequired - CurrentXp;
            }
        } 
        throw new System.ArgumentOutOfRangeException($"Could not find any entry for level {CurrentLevel}");
    }
}
