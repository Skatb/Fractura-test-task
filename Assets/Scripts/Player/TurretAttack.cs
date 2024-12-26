using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileStartPosition;
    [SerializeField] private GameObject projectilePool;

    private float fireTimer;

    private void Start()
    {
        InitializePool();
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f && GameManager.Instance.isGameStarted)
        {
            FireProjectile();
            fireTimer = fireRate;
        }
    }

    private void InitializePool()
    {
        while (projectilePool.transform.childCount < poolSize)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectile.transform.SetParent(projectilePool.transform);
        }
    }

    private GameObject GetProjectileFromPool()
    {
        if (projectilePool.transform.childCount > 0)
        {
            Transform projectileTransform = projectilePool.transform.GetChild(0);
            GameObject projectile = projectileTransform.gameObject;

            projectileTransform.SetParent(null);
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            Debug.LogWarning("Projectile pool is empty!");
            return null;
        }
    }

    public void ReturnProjectileToPool(GameObject projectile)
    {
        projectile.SetActive(false);
        projectile.transform.SetParent(projectilePool.transform);
    }

    private void FireProjectile()
    {
        GameObject projectile = GetProjectileFromPool();
        if (projectile != null)
        {
            projectile.transform.position = projectileStartPosition.transform.position;
            projectile.transform.rotation = projectileStartPosition.transform.localRotation;
            Vector3 direction = transform.up;
            projectile.SetActive(true);

            ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
            if (projectileBehavior != null)
            {
                projectileBehavior.Initialize(this, direction);
            }
        }
    }
}
