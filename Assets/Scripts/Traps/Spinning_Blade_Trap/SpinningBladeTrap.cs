using UnityEngine;

public class SpinningBladeTrap : MonoBehaviour
{
    [Header("Spinning Properties:")]
    [SerializeField] private float spinningSpeed = 100f;
    [SerializeField] private float rotationTime = 5f;
    [SerializeField] private float cooldownTime = 5f;
    [Header("Audio:")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip firstHitSound;
    [SerializeField] private AudioClip secondHitSound;
    [SerializeField] private float volume = 0.25f;

    private float rotationTimeLeft;
    private float cooldownTimeLeft;

    public bool shouldInstaKill;
    public float damage;
    private void Awake()
    {
        rotationTimeLeft = rotationTime;
        cooldownTimeLeft = cooldownTime;
    }

    private void Update()
    {
        RotateAndStop();
    }

    private void RotateAndStop()
    {
        if (rotationTimeLeft > 0)
        {
            GetComponent<BoxCollider>().enabled = true;
            RotateBlade();
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            RotationCooldown();
        }
    }

    private void RotateBlade()
    {
        transform.Rotate(0, spinningSpeed * Time.deltaTime, 0);
        rotationTimeLeft -= Time.deltaTime;
        cooldownTimeLeft = cooldownTime;
    }

    private void RotationCooldown()
    {
        if (cooldownTimeLeft > 0)
        {
            cooldownTimeLeft -= Time.deltaTime;
        }
        else
        {
            rotationTimeLeft = rotationTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int soundId = Random.Range(1, 3);
            if(soundId == 1) { audioSource.PlayOneShot(firstHitSound, volume); }
            else audioSource.PlayOneShot(secondHitSound, volume);

            if (shouldInstaKill)
            {
                collision.gameObject.GetComponent<PlayerHealth>().Kill(true);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerHealth>().Damage(damage);
            }
        }
    }
}
