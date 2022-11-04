using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetGun : MonoBehaviour
{
    public Transform bulletPrefab;

    public Transform aimTarget;

    public Vector3 offset;
    public float speed = 1;

    private void Start()
    {
        StartCoroutine(ShootRepeating());
    }

    void Update()
    {
        transform.position +=  (aimTarget.position + offset - transform.position) * Time.deltaTime * speed ;
    }

    IEnumerator ShootRepeating()
    {
        while(true)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);//Quaternion.LookRotation(aimTarget.position+offset));
            yield return new WaitForSeconds(1f);
        }
    }
}
