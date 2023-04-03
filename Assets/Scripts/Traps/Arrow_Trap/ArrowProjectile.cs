using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float slowestArrowSpeed = 0.5f;
    [SerializeField] private float damage;
    private Rigidbody rb;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        RemoveSlowArrows();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(damage);
            gameObject.SetActive(false);
        }
    }

    private void RemoveSlowArrows()
    {
        if (rb.velocity.magnitude <= slowestArrowSpeed)
        {
            gameObject.SetActive(false);
        }
    }
}
