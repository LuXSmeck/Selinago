using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AUpgradeEffect : Effect{
    
    [SerializeField] protected UpgradeEffectTypeEnum type;
    public UpgradeEffectTypeEnum Type => type;
    
}
