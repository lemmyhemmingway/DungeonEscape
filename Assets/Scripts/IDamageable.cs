using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    int health { get; set; }

    void Damage();
}
