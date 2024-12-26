using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IHealth, IDamage
{
    [SerializeField] private EnemyHealthBarUI healthBarUI;
    [SerializeField] private Collider aggroCollider;
    private SkinnedMeshRenderer meshRenderer;
    private Collider hitCollider;
    private EnemyMove enemyMove;

    public bool isAggroed = false;

    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    [SerializeField] private int _health;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            
            healthBarUI.UpdateHealthBar(_health, maxHealth);
        }
    }
    [SerializeField] private int _damage;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    [SerializeField] private int maxHealth;

    private void Start()
    {
        Health = maxHealth;
        enemyMove = GetComponent<EnemyMove>();
        hitCollider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        meshRenderer.material.color = Color.white * intensity;
    }
    public void OnTakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnDie();
        }

        blinkTimer = blinkDuration;
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }

    public void SetAggro(Transform target)
    {
        isAggroed = true;
        enemyMove.target = target;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<IDamagable>() != null)
        {
            collision.gameObject.GetComponent<IDamagable>().OnTakeDamage(Damage);
            OnDie();
        }
    }
}
