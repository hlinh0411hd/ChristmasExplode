using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IControlable
{
    public void OnPressedMove();
    public void OnPressedFire();
    public void OnKeyPressed();
    public void OnKeyReleased();
}
