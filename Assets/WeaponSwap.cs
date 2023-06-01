using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public GameObject[] weapons; // Array of weapon game objects
    private int currentWeaponIndex; // Index of the currently equipped weapon

    private void Start()
    {
        // Disable all weapons except the first one
        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        currentWeaponIndex = 0; // Set the initial weapon index
    }

    private void Update()
    {
        // Check for input to swap weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapWeapon(2);
        }
        // Add more conditions for additional weapons if needed
    }

    private void SwapWeapon(int weaponIndex)
    {
        // Disable the currently equipped weapon
        weapons[currentWeaponIndex].SetActive(false);

        // Enable the new weapon
        weapons[weaponIndex].SetActive(true);

        currentWeaponIndex = weaponIndex; // Update the current weapon index
    }
}