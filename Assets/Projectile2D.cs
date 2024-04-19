using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody2D bulletPrefab;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //shoot raycast to detect the mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);

            //get the position of mouse click
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                //move the target to the mouse clicked position
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log("hit: " + hit.point);
                
                //calculate velocity of the projectile motion
                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);
                
                //fire bullet using Rigidbody
                Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                //add velocity to bullet's rigidbody 
                firedBullet.velocity = projectileVelocity;
            }
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        //find velocity on x and y axis
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        
        //get new projectile vector
        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);
        
        return projectileVelocity;
    }
}
