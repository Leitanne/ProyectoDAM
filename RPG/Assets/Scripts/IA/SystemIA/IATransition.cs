using System;
using UnityEngine;

[Serializable]
public class IATransition
{
    public IADecision Decision;
    public IAState TrueState;
    public IAState FalseState;
}
