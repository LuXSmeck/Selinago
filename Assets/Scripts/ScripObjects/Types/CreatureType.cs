using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enums/CreatureType", fileName = "CreatureType")]
/// <summary>
/// Determines the SpeciesType of a Monster. f.ex.:
/// "Humonoid"; "Mechanoid"; "Beast"...
/// </summary>
public class CreatureType : Type{

    public ElementType[] elementalWeaknesses;
    public ElementType[] elementalResistances;
    public ElementType[] elementalImmunities;
    
}
