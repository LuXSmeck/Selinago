using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Weapon", fileName = "Weapon")]

public class WeaponCard : EquipableCard{
    
    [Header("Weapon Attributes")]
    [SerializeField] private WeaponType type;
    
}
