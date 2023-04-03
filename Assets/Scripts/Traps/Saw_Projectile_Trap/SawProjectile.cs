using UnityEngine;

public class SawProjectile : MonoBehaviour
{
    [Header("Bouncing Properties:")]
    [SerializeField] [Range(-1f, -0.1f)] private float minBounceForce = -0.4f;
    [SerializeField] [Range(0.1f, 1f)] private float maxBounceForce = 0.4f;
    [SerializeField] private float minVelocity;
    private Rigidbody rb;
    private Vector3 lastFrameVelocity;

    public float damageToPlayer;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
        RemoveSlowSaws();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(damageToPlayer);
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            BounceOff(collision.GetContact(0).normal);
        }
    }

    private void RemoveSlowSaws()
    {
        if (rb.velocity.magnitude <= 0.5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void BounceOff(Vector3 collisionNormal)
    {
        float randomNumber;
        do
        {
            randomNumber = Random.Range(minBounceForce, maxBounceForce);
        } while (randomNumber == 0);
        Vector3 randomVector = new Vector3(randomNumber, 0, randomNumber);

        float speed = lastFrameVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal + randomVector);
        rb.velocity = direction * Mathf.Max(speed, minVelocity) * 0.5f;
    }
}
