using System;
using UnityEngine;

public class Atom : MonoBehaviour
{
	[Header ("Atom data")]
	public string atomName;
	public int atomicNumber;
	public Color atomColor;

	[Space(20)]

	[Header("Atom renderers")]
	[SerializeField] private SpriteRenderer _bodyRenderer;
	[SerializeField] private SpriteRenderer _eyesRenderer;
	[SerializeField] private SpriteRenderer _mouthRenderer;
	[SerializeField] private Transform _trajectoryRenderer;

	[Space(20)]

	[Header("Face animators")]
	[SerializeField] private AnimBlink _eyesAnimator;
	[SerializeField] private AnimMouth _mouthAnimator;

	[Space(20)]

	[Header("Physics")]
	[SerializeField] private Rigidbody2D _rigidbody2d;
	public Collider2D Collider2d;

	[Space(20)]

	[Header("Input")]
	[SerializeField] private AtomMouseDetector _mouseDetector;

	[Space(20)]

	[Header("Debug")]
	[SerializeField] private GameObject _debugFrame;
	[SerializeField] private bool _debugGrabbedAtom;

	[NonSerialized] public bool Interactable;

	public void SetData(string name, int number, Color color)
	{
		if (atomicNumber == 1 && number != 1)
			AtomSpawnerManager.Instance.NumberOfInteracatbleAtoms--;

		atomName = name;
		atomicNumber = number;
		atomColor = color;

		float baseNumber = atomicNumber == 1 ? atomicNumber : Mathf.Floor(atomicNumber / 2);
		float scale = baseNumber / 100f;
		transform.localScale = new Vector3(scale, scale, scale);

		_bodyRenderer.color = atomColor;
		_eyesRenderer.color = atomColor;
		_mouthRenderer.color = atomColor;

		// Only H can be launched
		Interactable = atomicNumber == 1;
		_mouseDetector.gameObject.SetActive(Interactable);

		GameStateManager.Instance.OnAtomDataSet(atomicNumber);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.CompareTag("Atom"))
		{
			AudioManager.Instance.AtomFusion.Play();

			if (AtomSpawnerManager.Instance.FirstCollided == null)
				AtomSpawnerManager.Instance.FirstCollided = this;
			else if (AtomSpawnerManager.Instance.SecondCollided == null)
				AtomSpawnerManager.Instance.SecondCollided = this;
		}
	}

	public void ApplyForce(Vector2 force)
	{
		if (force == Vector2.zero) return;

		_rigidbody2d.AddForce(force);

		float torque = UnityEngine.Random.Range(0, 3f);
		float sign = Mathf.RoundToInt(UnityEngine.Random.Range(-1f, 1f));
		if (sign == 0) sign = 1;
		torque *= sign;
		_rigidbody2d.AddTorque(torque);
	}

	private void FixedUpdate()
	{
		if (Mathf.Floor(_rigidbody2d.velocity.magnitude) < 0.5f)
		{
			_mouthAnimator.MouthSmile();
		}
        else
        {
			_mouthAnimator.MouthFear();
        }
    }

	public void PaintArrow(Vector2 currArrow)
	{
        if (currArrow == Vector2.zero)
        {
			_mouthAnimator.MouthSmile();
        }
		else
		{
			_mouthAnimator.MouthSurprise();
		}

		Vector3 arrowScale = _trajectoryRenderer.localScale;
		arrowScale.x = Mathf.Min(currArrow.magnitude, 200f);
		_trajectoryRenderer.localScale = arrowScale;

		if (currArrow.magnitude == 0) return;

		Quaternion globalRotation = _trajectoryRenderer.rotation;
		Vector3 eulerRotation = globalRotation.eulerAngles;
		eulerRotation.z = Mathf.Rad2Deg * Mathf.Atan2(currArrow.y, currArrow.x);
		globalRotation.eulerAngles = eulerRotation;
		_trajectoryRenderer.rotation = globalRotation;
    }

	public void PaintDebugFrame(bool paint)
	{
		if (!_debugGrabbedAtom) return;

		_debugFrame.SetActive(paint);
	}
}
