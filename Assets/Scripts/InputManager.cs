using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [Tooltip("The joystick for moving the spaceship.")]
    public VirtualJoystick steering;

    [Tooltip("Shoot interval.")]
    public float fireRate = 0.2f;

    // The actual ShipWeapons script for usage.
    private ShipWeapons _currentWeapons;

    // Will be true if we are firing right now.
    private bool _isFiring = false;

    public void SetWeapons(ShipWeapons weapons)
    {
        _currentWeapons = weapons;
    }

    public void RemoveWeapons(ShipWeapons weapons)
    {
        if (_currentWeapons == weapons)
        {
            _currentWeapons = null;
        }
    }

    public void StartFiring()
    {
        StartCoroutine(FireWeapons());
    }

    private IEnumerator FireWeapons()
    {
        _isFiring = true;

        while (_isFiring)
        {
            if (_currentWeapons != null)
            {
                _currentWeapons.Fire();
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    public void StopFiring()
    {
        _isFiring = false;
    }
}
