using UnityEngine;
using System.Collections;

public class HeroFollower : MonoBehaviour
{
    public Vector3 target_point;  // Actual Target Point being followed
		public float linearSpeed = 2.0f;
		public float followDistance = 3.0f;
    public bool demo = false; // when set to true, the quad will follow demo hero
    public bool startFollowing = false;

    Transform hero;               // Using a Hero for moving the target_point.
    float rotationRate = 0.5f;

    void Awake()
    {
        // Get Hero transform for follow me demo
        hero = GameObject.FindGameObjectWithTag ("Player").transform;
    }

		void Update()
    {
      if (demo)
      {
        target_point.x = hero.position.x;
        target_point.y = hero.position.y + 2.5f;
        target_point.z = hero.position.z;
      }

      if (startFollowing)
      {
        Move();
  			Turn();
      }
    }

		// Motion Methods
		void Move()
		{
			//transform.position += transform.forward * speed * Time.deltaTime;
      //Vector3 diff = transform.position - hero.position;
      Vector3 diff = transform.position - target_point;

      if (diff.magnitude > followDistance)
      {
        transform.position -= diff.normalized * Time.deltaTime * linearSpeed;
      }

      else
      {
        transform.position = transform.position;
      }

      //transform.position = hero.position + diff.normalized * followDistance;
		}

		void Turn()
		{
      //transform.LookAt(hero.TransformPoint(Vector3(0,0,2)));
			Vector3 pos = target_point - transform.position;
			Quaternion rot = Quaternion.LookRotation(pos);

			//Turn quat to euler and add the 45deg adjustment
			//Vector3 euler_angles = rot.eulerAngles;
			//euler_angles[2] += 0;
			//euler_angles[1] += 45;
			//euler_angles[0] += 0;
			//rot = Quaternion.Euler(euler_angles);

			transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationRate * Time.deltaTime);
		}

    // DEBUG
    void OnDrawGizmos()
    {
      Gizmos.color = Color.yellow;
      Gizmos.DrawSphere(target_point, 0.2f); //Draws a sphere around the target_point
    }
}
