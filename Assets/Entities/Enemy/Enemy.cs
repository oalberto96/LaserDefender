using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float life = 10f;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
