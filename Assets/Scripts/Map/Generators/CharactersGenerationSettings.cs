using System;
using System.Collections.Generic;

[Serializable]
public class CharactersGenerationSettings
{
    public IntLevelValueRange count = new();
    public List<CharacterGenerationChance> chances = new();
}
