using UnityEngine;

public class HealthController : MonoBehaviour, IDamagable, IHealth
{
    [SerializeField] private Collider carCollider;
    private int _health { get; set; }
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                OnDie();
            }
        }
    }
    [SerializeField] private int maxHealth;

    private void Start()
    {
        Health = maxHealth;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().
            OnTakeDamage(10);
        }
    }
    public void OnTakeDamage(int damage)
    {
        Health -= damage;
    }

    public void OnDie()
    {
        throw new System.NotImplementedException();
    }
}
