using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float maxDistance;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosionEffectPrefab;

    private TurretAttack turretAttack;
    private Vector3 direction;
    private Vector3 startPosition;

    public void Initialize(TurretAttack turretAttack, Vector3 direction)
    {
        this.turretAttack = turretAttack;
        this.direction = direction.normalized;
        startPosition = transform.position;
    }
    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.position -= direction * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Deactivate();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamagable enemy = collision.gameObject.GetComponent<IDamagable>();

            if (enemy != null)
            {
                enemy.OnTakeDamage(damage);
            }
            SpawnHitEffect(transform.position);
            Deactivate();
        }
    }
    private void SpawnHitEffect(Vector3 position)
    {
        if (explosionEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(explosionEffectPrefab, position, Quaternion.identity);
            Destroy(hitEffect, 1f);
        }
    }
    protected void Deactivate()
    {
        turretAttack.ReturnProjectileToPool(gameObject);
    }
}