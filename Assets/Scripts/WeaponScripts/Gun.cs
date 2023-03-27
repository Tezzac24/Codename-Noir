using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponScriptableObject weaponSO;
    float timeSinceLastShot;

    public Transform firepoint;
    public Transform gunEndPoint;
    [SerializeField] GameObject bulletPrefab;

    void Start()
    {
        WeaponShooting.shootInput += Shoot;
        WeaponShooting.reloadInput += StartReload;
        weaponSO.currentAmmo = weaponSO.magSize;
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    bool CanShoot() => !weaponSO.reloading && timeSinceLastShot > 1f / (weaponSO.fireRate / 60f);
    
    void Shoot()
    {
        if (weaponSO.currentAmmo > 0 && CanShoot())
        {
            GameObject bullet = ObjectPool.instance.GetPooledObject();//Instantiate(bulletPrefab, gunEndPoint.position, firepoint.rotation);
            bullet.transform.position = gunEndPoint.transform.position;
            bullet.transform.rotation = firepoint.rotation;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.SetActive(true);
            rb.AddForce(firepoint.up * weaponSO.bulletForce, ForceMode2D.Impulse);
            weaponSO.currentAmmo--;
            timeSinceLastShot = 0;
        }
    }

    public void StartReload()
    {
        if(!weaponSO.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        weaponSO.reloading = true;
        yield return new WaitForSeconds(weaponSO.reloadTime);
        weaponSO.currentAmmo = weaponSO.magSize;
        weaponSO.reloading = false;
    }

}
