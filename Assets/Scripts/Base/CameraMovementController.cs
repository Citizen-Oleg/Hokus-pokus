using UnityEngine;

namespace CameraComponent
{
	public class CameraMovementController: MonoBehaviour
	{
		[SerializeField]
		private Transform _target;
		[SerializeField]
		private float _followSpeed;
		[SerializeField]
		private Vector3 _offset;

		private void LateUpdate()
		{
			transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _followSpeed * Time.deltaTime);
		}

		private void OnDrawGizmosSelected()
		{
			transform.position = _target.position + _offset;
		}
	}
}