using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonBehaviour<InputManager>
{
	public Atom GrabbedAtom;

	private Vector2 mouseFirstPos;
	private Vector2 mouseCurrPos;
	private Vector2 currDragArrow;

	private bool dragging = false;
	public bool Dragging => dragging;

	private void Awake()
	{
		SingletonInit(this);
	}

	private void Update()
	{
		if (GrabbedAtom != null)
		{
			if (Input.GetMouseButtonDown(0))
			{
				mouseFirstPos = Input.mousePosition;
				dragging = true;
			}

			if (Input.GetMouseButton(0) && dragging)
			{
				mouseCurrPos = Input.mousePosition;

				currDragArrow = mouseFirstPos - mouseCurrPos;
				GrabbedAtom.PaintArrow(currDragArrow);
			}

			if (Input.GetMouseButtonUp(0) && dragging)
			{
				//Apply force to grabbed atom
				GrabbedAtom.ApplyForce(currDragArrow);

				// Reset variables
				mouseFirstPos = Vector2.zero;
				mouseCurrPos = Vector2.zero;
				currDragArrow = Vector2.zero;

				GrabbedAtom.PaintDebugFrame(false);
				GrabbedAtom = null;
				dragging = false;
				AtomSpawnerManager.Instance.ResetAllAtomArrows();
			}
		}

		// Failsafe
		if (Input.GetMouseButtonUp(0))
		{
			if (GrabbedAtom != null)
				GrabbedAtom.PaintDebugFrame(false);
			GrabbedAtom = null;
			dragging = false;
			AtomSpawnerManager.Instance.ResetAllAtomArrows();
		}
	}
}
