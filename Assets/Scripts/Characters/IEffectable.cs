using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectable 
{
    public void ApplyEffect();
    public void RemoveEffect();

    public void HandleEffect();
}
