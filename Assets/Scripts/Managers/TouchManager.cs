using System;
using UnityEngine;

public class TouchManager : Singleton<TouchManager>
{
    #region Public Fields

    public Action<float> HorizontalAxisChanged;

    #endregion
    #region Private Fields

    private float horizontalAxis;
    private Vector3 pressedPosition;

    #endregion

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pressedPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaPosition = (pressedPosition - Input.mousePosition) / 250;

            horizontalAxis = -deltaPosition.x;
        }
        else
        {
            horizontalAxis = Mathf.Lerp(horizontalAxis, 0, Time.deltaTime * 5);
        }
        
        HorizontalAxisChanged?.Invoke(horizontalAxis);
    }
}
