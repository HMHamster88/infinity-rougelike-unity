using System;
using System.Collections.Generic;

[Serializable]
public class ContainersGenerationSettings
{
    public IntLevelValueRange count = new();
    public List<ContainerGenerationChance> chances = new();
}
