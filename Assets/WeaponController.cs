using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool isEquipped = false;
    public GameObject projectilePrefab;
    public Transform gunTip;
    public Camera mainCamera;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEquipped = true;
            transform.SetParent(mainCamera.transform);
            transform.localPosition = GetGunPosition();
            transform.localRotation = Quaternion.identity;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEquipped = false;
            transform.SetParent(null);
        }
    }

    public void Fire()
    {
        if (isEquipped && Input.GetMouseButtonDown(0))
        {
            Debug.Log("ad");
            GameObject projectile = Instantiate(projectilePrefab, gunTip.position, transform.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(transform.forward * 1000f);
            Destroy(projectile, 2f);
        }
    }

    Vector3 GetGunPosition()
    {
        Vector3 gunTarget = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1f));
        Vector3 gunPosition = gunTarget - (mainCamera.transform.forward * 0.25f);
        return gunPosition;
    }
}