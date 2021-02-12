using UnityEngine;

public class Colba : MonoBehaviour, IAtackable
{
    private bool isFalling;
    [SerializeField] private float damage;
    [SerializeField]private float radius;

    private void OnTriggerEnter2D(Collider2D inner)
    {
        if(inner.tag == "CanWalk" || inner.tag == "CanAttack")
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var collider in colliders)
            {
                var atackable = collider.GetComponent<IAtackable>();
                if (atackable != null)
                {
                    atackable.GetDamage(damage);
                }
            }
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }

    public void GetDamage(float damage)
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
