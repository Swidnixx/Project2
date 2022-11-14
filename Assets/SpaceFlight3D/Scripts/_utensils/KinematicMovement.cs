using System.Collections;
using UnityEngine;

namespace Utils
{
	public static class KinematicMovement 
	{
		public static IEnumerator MoveToLineary(Transform obj, Vector3 targetPos, float speed)
        {
			while( Vector3.Distance(obj.position, targetPos) > 0 )
            {
				obj.position = Vector3.MoveTowards(obj.position, targetPos, Time.deltaTime * speed);
				yield return null;
            }
        }

		public static IEnumerator MoveToLineary(Rigidbody obj, Vector3 targetPos, float speed)
		{
			float distance = Vector3.Distance(obj.position, targetPos);
			float traveledDistance = 0;

			Vector3 direction = (targetPos - obj.position).normalized;

			while ( traveledDistance < distance )
			{
				Vector3 step = direction * Time.fixedDeltaTime * speed;
				obj.MovePosition( obj.position + step );
				traveledDistance += step.magnitude;
				yield return null;
			}
		}
	}
}