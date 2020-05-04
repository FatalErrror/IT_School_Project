﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGrass : InventoryObject
{
    public override void Initialize()
    {
        Characteristics = new Characteristic[1] { new Characteristic("Поднимает здоровье",10) };
        Using += OnUse;
    }

    public void OnUse(){
        Destroy(gameObject);
    }
}