using UnityEngine;

public class SawShootingTrap : MonoBehaviour, ITrap, IPooledProjectiles
{
    [Header("Movement Properties:")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;

    [Header("Shooting Properties:")]
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float minTimeBetweenShots = 1f;
    [SerializeField] private float maxTimeBetweenShots = 1.8f;
    [SerializeField] private float projectileSpeed = 10f;

    private ProjectilePooler projectilePooler;

    private void Start() => projectilePooler = ProjectilePooler.Instance;

    private void Update()
    {
        Move();
        CountDownAndShoot();
    }

    public void Shoot()
    {
        GameObject projectile = projectilePooler.SpawnFromPool("Sawblade", shootingPoint.transform.position, Quaternion.identity);
        Vector3 direction = (shootingPoint.transform.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
    }

    private void CountDownAndShoot()
    {
        timeBetweenShots -= Time.deltaTime;
        if (timeBetweenShots <= 0f)
        {
            OnProjectileSpawn();
            timeBetweenShots = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * movementSpeed, 1));
    }

    public void OnProjectileSpawn()
    {
        Shoot();
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
