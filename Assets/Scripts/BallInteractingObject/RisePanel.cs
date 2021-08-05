using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisePanel : MonoBehaviour
{
    public VerticalDirection to;
    public LayerMask whatToCatch;
    
    private void Update()
    {
        if (Physics2D.OverlapPoint(transform.position, whatToCatch))
        {
            if (Physics2D.OverlapPoint(transform.position, whatToCatch).TryGetComponent(out Ball ball))
            {
                switch (to)
                {
                    case VerticalDirection.Up:
                        ball.GoUp();
                        break;
                    case VerticalDirection.Down:
                        ball.GoDown();
                        break;
                }
            }
        }
    }
}
