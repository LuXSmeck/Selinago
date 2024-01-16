using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlacableCard : Card{
    
    /// <summary> Called by the CardSlot if a card was legit to be placed on a given Field </summary>
    /// <param name="targetPositionX"></param>
    /// <param name="targetPositionY"></param>
    public abstract bool placeCard(CardSlot cardSlot);
    public abstract void removeCard(CardSlot cardSlot);
}
