using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Void Event")]
public class VoidEvent : BaseGameEvent<Void>
{
    public void Raise() => Raise(new Void());
}
