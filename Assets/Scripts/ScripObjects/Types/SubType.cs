using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enums/SubType", fileName = "SubType")]
/// <summary>
/// Defines a SubType or sometimes a Class of a Creature. f.ex.:
/// "Soldier"; "Fish"...
/// </summary>
public class SubType : Type {

   public ElementType[] elementalWeaknesses;
   public ElementType[] elementalResistances;
   public ElementType[] elementalImmunities;

}
