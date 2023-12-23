using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AFieldCard : PlacableCard{
    
    public override bool placeCard(CardSlot cardSlot, bool forcePlace=false){
        Debug.Log("TODO FieldCard");
        throw new NotImplementedException();
    }
}
