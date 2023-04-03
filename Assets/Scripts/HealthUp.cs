using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    public SharedFloat health;
    public SharedFloat maxHealth;
    public SharedInt trapLevel;
    public GameObject healthPickUp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Player"))
            {
                health.Amount = maxHealth.Amount;
                trapLevel.Amount += 1;
                SpawnPickUp();
                Destroy(gameObject);
            }
    }

    private void SpawnPickUp()
    {
        var position = new Vector3(Random.Range(-6f, 14f), 1f, Random.Range(-2.5f, -14.7f));//lazy hardcoding
        Instantiate(healthPickUp, position, Quaternion.identity);
    }
}
