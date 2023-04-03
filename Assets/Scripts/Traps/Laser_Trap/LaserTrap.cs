using UnityEngine;

public class LaserTrap : MonoBehaviour, ITrap
{
    [Header("Movement Properties:")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;

    [Header("Laser Properties:")]
    [SerializeField] private float laserTime = 5f;
    [SerializeField] private float cooldownTime = 5f;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask whatIsSolid;

    [Header("Audio:")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float volume = 0.25f;

    private CapsuleCollider capsuleCol;

    private float laserTimeLeft;
    private float cooldownTimeLeft;

    public float damagePerPhysicsFrame;

    private void Awake()
    {
        SetCapsuleCollider();
        laserTimeLeft = laserTime;
        cooldownTimeLeft = cooldownTime;
    }

    private void Update()
    {
        Move();
        ShootAndStop();
    }

    private void ShootAndStop()
    {
        if (laserTimeLeft > 0) { Shoot(); }
        else LaserCooldown();
    }

    public void Shoot()
    {
        Vector3 laserTarget = FindTarget();
        if (laserTarget != Vector3.zero)
        {
            capsuleCol.transform.LookAt(shootingPoint.position);
            capsuleCol.height = (laserTarget - shootingPoint.position).magnitude * 3;

            lineRenderer.enabled = true;
            capsuleCol.enabled = true;
            lineRenderer.SetPosition(0, shootingPoint.transform.position);
            lineRenderer.SetPosition(1, laserTarget);

            laserTimeLeft -= Time.deltaTime;
            cooldownTimeLeft = cooldownTime;
        }
        else lineRenderer.enabled = false;
    }

    private Vector3 FindTarget()
    {
        Vector3 shootingDirection = transform.position - shootingPoint.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.transform.position, -shootingDirection, out hit, 1000f, whatIsSolid)) { return hit.point; }
        else return Vector3.zero;
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * movementSpeed, 1));
    }

    private void LaserCooldown()
    {
        lineRenderer.enabled = false;
        capsuleCol.enabled = false;
        if (cooldownTimeLeft > 0) { cooldownTimeLeft -= Time.deltaTime; }
        else laserTimeLeft = laserTime;
    }

    private void SetCapsuleCollider()
    {
        capsuleCol = gameObject.AddComponent<CapsuleCollider>();
        capsuleCol.radius = 1 / 2;
        capsuleCol.center = Vector3.zero;
        capsuleCol.direction = 2; 
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(hitSound, volume);
            collision.gameObject.GetComponent<PlayerHealth>().Damage(damagePerPhysicsFrame);
        }
    }

    public void SetVectorA(Vector3 vectorA)
    {
        pointA = vectorA;
    }

    public void SetVectorB(Vector3 vectorB)
    {
        pointB = vectorB;
    }

    public Vector3 GetVectorA()
    {
        return pointA;
    }

    public Vector3 GetVectorB()
    {
        return pointB;
    }
}
