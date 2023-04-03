using UnityEngine;
public interface ITrap
{
    public void Move();
    public void Shoot();
    public void SetVectorA(Vector3 vectorA);
    public void SetVectorB(Vector3 vectorB);
}