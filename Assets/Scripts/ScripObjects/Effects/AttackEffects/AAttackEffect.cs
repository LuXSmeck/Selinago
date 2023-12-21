using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttackEffect : Effect, IComparable<AAttackEffect>{
    
    /// <summary> value [1-5] The Effect with the highest priority will be excecuted first </summary>
    [SerializeField] protected int priority = 3;
    [SerializeField] protected AttackEffectTypeEnum type;
    public AttackEffectTypeEnum Type => type;
    
    public int CompareTo(AAttackEffect other){
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return priority.CompareTo(other.priority) * -1;
    }
}
