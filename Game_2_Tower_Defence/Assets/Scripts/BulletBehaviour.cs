/*  Filename: BulletBehaviour.cs
 *   Purpose: Controls bullets shooting from monsters (speed, direction, damage)
 *            When an enemy is hit, decrease health and/or increase gold if killed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private Vector3 normalizeDirection;
    private GameManagerBehaviour gameManager;

    // When a new bullet is instantiated, normalize the difference between targetPosition and startPosition to get a standard 'direction' vector
    void Start()
    {
        normalizeDirection = (targetPosition - startPosition).normalized;
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehaviour>();
    }

    // Updates the bullet's position along the normalized vector, according to the speed
    void Update()
    {
        transform.position += normalizeDirection * speed * Time.deltaTime;
    }

    //Checks for collision with an enemy
    void OnTriggerEnter2D (Collider2D other)
    {
        target = other.gameObject;
        if (target.tag.Equals("Enemy"))
        {
            Transform healthBarTransform = target.transform.Find("HealthBar");
            HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
            healthBar.currentHealth -= damage;

            if (healthBar.currentHealth <= 0)
            {
                Destroy(target);
                AudioSource audioSource = target.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                gameManager.Gold += 50;
            }
            Destroy(gameObject);
        }
    }
}
