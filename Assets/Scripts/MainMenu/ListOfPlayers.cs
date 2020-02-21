using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListOfPlayers
{
    public List<PlayerSerializable> players;
    public ListOfPlayers()
    {
        this.players= new List<PlayerSerializable>();
    }
}
