using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAttackType : MonoBehaviour
{
 [SerializeField] public AnimatorOverrideController[] overrideControllers;

[SerializeField] public AnimatorOverrider overrider;


  public void Set(int value)
    {

overrider.SetAnimations(overrideControllers[value]);

    }


}
