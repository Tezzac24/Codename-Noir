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

    void OnEnable()
    {
        WeaponShooting.shootInput += Shoot;
        WeaponShooting.reloadInput += StartReload;

        timeSinceLastShot = 0f;
        weaponSO?.ResetRuntimeState();
    }

    void OnDisable()
    {
        WeaponShooting.shootInput -= Shoot;
        WeaponShooting.reloadInput -= StartReload;

        StopAllCoroutines();

        if (weaponSO != null)
        {
            weaponSO.reloading = false;
        }
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    bool CanShoot() => !weaponSO.reloading && timeSinceLastShot > 1f / (weaponSO.fireRate / 60f);

    void Shoot()
    {
        if (weaponSO == null || firepoint == null || gunEndPoint == null)
        {
            return;
        }

        if (weaponSO.currentAmmo > 0 && CanShoot())
        {
            if (ObjectPool.instance == null)
            {
                return;
            }

            GameObject bullet = ObjectPool.instance.GetPooledObject();//Instantiate(bulletPrefab, gunEndPoint.position, firepoint.rotation);
            if (bullet == null)
            {
                return;
            }

            bullet.transform.position = gunEndPoint.transform.position;
            bullet.transform.rotation = firepoint.rotation;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                return;
            }

            bullet.SetActive(true);
            rb.AddForce(firepoint.up * weaponSO.bulletForce, ForceMode2D.Impulse);
            weaponSO.currentAmmo--;
            timeSinceLastShot = 0;
        }
    }

    public void StartReload()
    {
        if (weaponSO != null && !weaponSO.reloading && gameObject.activeInHierarchy)
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
