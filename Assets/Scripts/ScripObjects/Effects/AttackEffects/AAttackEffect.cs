using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttackEffect : Effect{
    
    [SerializeField] private AttackEffectTypeEnum type;
    public AttackEffectTypeEnum Type => type;
}
