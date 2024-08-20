using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomMouseDetector : MonoBehaviour
{
	[SerializeField] private Atom atom;

	private void OnMouseEnter()
	{
		if (atom.Interactable && InputManager.Instance.GrabbedAtom == null && !InputManager.Instance.Dragging)
		{
			InputManager.Instance.GrabbedAtom = atom;
			atom.PaintDebugFrame(true);
		}
	}

	private void OnMouseExit()
	{
		if (atom.Interactable && !InputManager.Instance.Dragging)
		{
			InputManager.Instance.GrabbedAtom = null;
			atom.PaintDebugFrame(false);
		}
	}
}
