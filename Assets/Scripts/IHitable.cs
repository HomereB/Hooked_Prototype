using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    public abstract void Hit(bool downed, Vector2 ejectionForce);
}
