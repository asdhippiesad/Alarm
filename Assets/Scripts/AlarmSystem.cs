using System;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    public event Action Registered;
    public event Action Disarmed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Movement mover))
        {
            Registered?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Movement mover))
        {
            Disarmed?.Invoke();
        }
    }

}
