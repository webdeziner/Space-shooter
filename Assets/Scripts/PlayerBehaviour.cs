using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float playerSpeed = 4.0f;
    private float currentSpeed = 0.0f;
    private Vector3 lastMovement = new Vector3();
    // The laser
    public Transform laser;
    // Distance from ship center
    public float laserDistance = .2f;
    // Fire wait time
    public float timeBetweenFires = .3f;
    // Fire countdown
    private float timeTilNextFire = 0.0f;
    // Shoot buttons
    public List<KeyCode> shootButton;
    // Shoot sound
    public AudioClip shootSound;
    // Source
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update ()
    {
        Rotation();
        Movement();
        // Loop shoot buttons
        foreach (KeyCode element in shootButton)
        {
            if (Input.GetKey(element) && timeTilNextFire < 0)
            {
                timeTilNextFire = timeBetweenFires;
                ShootLaser();
                break;
            }
        }
        timeTilNextFire -= Time.deltaTime;
	}

    void ShootLaser()
    {
        audioSource.PlayOneShot(shootSound);
        Vector3 laserPos = this.transform.position;
        // Angle away from the center
        float rotationAngle = transform.localEulerAngles.z - 90;
        // Get the ships front position
        laserPos.x += (Mathf.Cos((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
        laserPos.y += (Mathf.Sin((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
        Instantiate(laser, laserPos, this.transform.rotation);
    }

    void Rotation()
    {
        // Get mouse position
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;
        // Get angle between them
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        this.transform.rotation = rot;
    }

    void Movement()
    {
        Vector3 movement = new Vector3();
        // Check for input
        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");
        movement.Normalize();
        // Pressed anything
        if(movement.magnitude > 0)
        {
            // Move in that direction
            currentSpeed = playerSpeed;
            this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = movement;
        }
        else
        {
            this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
            currentSpeed *= .9f;
        }
    }
}
