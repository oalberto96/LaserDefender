using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
	public float speed = 15.0f;
	public float padding = 1;
    public float life = 500;
	float xmin;
	float xmax;

    void Shoot()
    {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject bullet = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

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

    // Use this for initialization
    void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;	
		//Left bottom is (0,0), Right top us (1,1)
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		// Restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Shoot", 0.00001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Shoot");
        }

    }
}
