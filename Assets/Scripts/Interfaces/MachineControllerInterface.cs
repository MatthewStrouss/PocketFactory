using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMachineController
{
    void CollisionEnter(Collider2D col);
    void AddToInventory(Resource resourceToAdd);
    void ActionToPerformOnTimer();
    void OnClick();
    void SetControllerValues(IMachineController other);
}
