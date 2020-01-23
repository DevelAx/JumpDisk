using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(HumanFlip))]
public class HumanJump : MyMonoBehaviour
{
	[SerializeField]
	private HumanSounds _sounds = default;

	[SerializeField]
	private HumanFlip _flip = default;

	private Vector3 _velocity;
	private float _groundY;
	private float _jumpStrength;
	private float _targetX;

#if UNITY_EDITOR
	private Vector3 _lastTargetPosition;
	private float _lastTargetRadius;
#endif

	private float BackSpeed => Settings.Human.JumpBackSpeed;

	public bool IsGrounded { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		Debug.Assert(_flip, nameof(_flip));
		Debug.Assert(_sounds, nameof(_sounds));

		IsGrounded = true;
		_groundY = transform.position.y;
	}

	public void JumpTo(Vector3 targetPosition)
	{
#if UNITY_EDITOR
		_lastTargetPosition = targetPosition;
		_lastTargetRadius = 1.5f;
#endif
		_targetX = targetPosition.x;
		_jumpStrength = CalcJumpStrength(targetPosition);
		float distance = Vector3.Distance(transform.position, targetPosition);
		float t = distance / BackSpeed; // Calc time required to reach the target poistion with a given speed.
		float startJumpSpeed = Constants.Physics.G * t / 2; // Calc initial jump speed required to be long time (t) enough in the air to reach the target position.
		Vector3 upVelocity = Vector3.up * startJumpSpeed;
		Vector3 backVelocity = Vector3.left * BackSpeed;
		_velocity = upVelocity + backVelocity;

		StartCoroutine(PlayJump(1 / t));
	}

	private IEnumerator PlayJump(float animationSpeedRatio)
	{
		IsGrounded = false;
		_flip.Play(animationSpeedRatio);
		_sounds.PlayJump(_jumpStrength);

		while (_groundY <= transform.position.y)
		{
			transform.position += _velocity * Time.smoothDeltaTime;
			_velocity += Vector3.down * Constants.Physics.G * Time.smoothDeltaTime;
			yield return null;
		}

		IsGrounded = true;
		transform.position = new Vector3(_targetX, _groundY, transform.position.z);
		_sounds.PlayLanding(_jumpStrength);
	}

	protected override void OnEditorDrawGizmos()
	{
		base.OnEditorDrawGizmos();

		if (_lastTargetRadius <= 0 || _lastTargetPosition == Vector3.zero)
			return;

		_lastTargetRadius -= 0.2f * Time.deltaTime;
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(_lastTargetPosition, _lastTargetRadius);
	}

	#region Helpers

	private float CalcJumpStrength(Vector3 targetPosition)
	{
		Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(targetPosition);
		Vector3 humanScreenPos = Camera.main.WorldToScreenPoint(transform.position);
		var xDistance = Mathf.Abs(humanScreenPos.x - targetScreenPos.x);
		return xDistance / Screen.width;
	}

	protected override void OnEditorValidate()
	{
		base.OnEditorValidate();
		_flip = _flip ?? this.RequireComponent<HumanFlip>();
		_sounds = _sounds ?? this.RequireComponentInChildren<HumanSounds>();
	}

	#endregion
}
