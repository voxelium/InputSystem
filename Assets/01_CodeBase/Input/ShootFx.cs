using UnityEngine;

public class ShootFx : MonoBehaviour
{
    [SerializeField] GameObject Fx;
    internal void Shoot()
    {
        Instantiate(Fx, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }
}
