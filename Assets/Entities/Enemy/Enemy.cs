using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float life = 10f;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public float shotsPerSeconds = 0.5f;

    private float firingRate;
    private bool isShooting;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile missile = other.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            missile.Hit();
            life -= missile.damage;
            if (life < 0)
            {
                Destroy(this.gameObject);
            }
        }
        
    }

    void Shoot()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject bullet = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
    }
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        float probability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < probability)
        {
            Shoot();
        }
    }
}
