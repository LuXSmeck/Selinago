using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enums/WeaponType", fileName = "EquipmentType")]
public class WeaponType : Type{
    
   [Header("Equip Attributes")]
   [SerializeField] private List<Type> creatureWhitelist;
   [SerializeField] private List<Type> creatureBlacklist;

   public List<Type> CreatureWhitelist => creatureWhitelist;
   public List<Type> CreatureBlacklist => creatureBlacklist;
}
