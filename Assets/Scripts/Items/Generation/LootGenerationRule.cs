using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class LootGenerationRule
{
    public IntLevelValueRange ItemsCount;
    public List<ItemDropChance> SpecialItems = new List<ItemDropChance>();
}

