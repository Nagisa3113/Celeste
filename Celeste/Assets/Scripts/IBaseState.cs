using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseState 
{
    void Enter();
    void Update();
    void FixedUpdate();
    void Finish();
}
