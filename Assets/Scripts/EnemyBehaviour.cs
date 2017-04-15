using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    // Health
    public int health = 2;
    // Explode!
    public Transform explosion;
    // We need sound
    public AudioClip hitSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Find my laser
        if(collision.gameObject.name.Contains("Laser"))
        {
            LaserBehaviour laser = collision.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
            health -= laser.damage;
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().PlayOneShot(hitSound);
            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            controller.KilledEnemy();
            controller.IncreaseScore(10);
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (explosion)
        {
            GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
            Destroy(exploder, 2.0f);
        }
    }

}
