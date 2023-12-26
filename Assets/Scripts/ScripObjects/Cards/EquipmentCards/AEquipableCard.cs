using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableCard : Card{


    /// <summary> ATM an EQCard can only equip a creaturecard </summary>
    /// <param name="cardSlot"></param>
    public abstract bool equipCard(CardSlot cardSlot);


    public abstract void removeEffects(CardSlot cardSlot);


}
