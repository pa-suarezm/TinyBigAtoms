using System;
using System.Collections.Generic;
using UnityEngine;

public class AtomSpawnerManager : SingletonBehaviour<AtomSpawnerManager>
{
	[Header("Spawning helpers")]
	[SerializeField] private GameObject atomPrefab;
	[SerializeField] private Transform playArea;
	[SerializeField] private Transform spawnPoint;

	[Space(20)]

	[Header("All atoms data")]
	[SerializeField] public List<string> allAtomNames;
	[SerializeField] public List<Color> allAtomColors;

	[Space(20)]

	[Header("Spawning parameters")]
	[SerializeField] private float atomAutoSpawnIntervalSeconds = 3f;
	[SerializeField] private int maxNumberOfInteractableAtoms = 10;

	[NonSerialized] public int NumberOfInteracatbleAtoms = 0;
	private float nextAtomSpawnTime;

	private List<Atom> allSpawnedAtoms = new List<Atom>();

	public Atom FirstCollided;
	public Atom SecondCollided;
	private int auxAtomNumber;

	private void Awake()
	{
		SingletonInit(this);
	}

	private void Update()
	{
		if (!GameStateManager.Instance.Playing) return;

		if (Time.time > nextAtomSpawnTime)
		{
			if (NumberOfInteracatbleAtoms < maxNumberOfInteractableAtoms)
			{
				SpawnAtom(1);
				NumberOfInteracatbleAtoms++;
			}

			atomAutoSpawnIntervalSeconds = UnityEngine.Random.Range(1.5f, 3f);
			nextAtomSpawnTime = Time.time + atomAutoSpawnIntervalSeconds;
		}

		if (FirstCollided != null && SecondCollided != null)
		{
			auxAtomNumber = FirstCollided.atomicNumber + SecondCollided.atomicNumber;

			auxAtomNumber = auxAtomNumber > 118 ? 118 : auxAtomNumber;

            if (FirstCollided.atomicNumber > SecondCollided.atomicNumber)
            {
				FirstCollided.SetData(allAtomNames[auxAtomNumber], auxAtomNumber, allAtomColors[auxAtomNumber]);
				DeleteAtom(SecondCollided);
            }
			else
			{
				SecondCollided.SetData(allAtomNames[auxAtomNumber], auxAtomNumber, allAtomColors[auxAtomNumber]);
				DeleteAtom(FirstCollided);
			}

			FirstCollided = null;
			SecondCollided = null;
        }
	}

	public void ResetGame()
	{
		for (int i = 0; i < allSpawnedAtoms.Count; i++)
		{
			Destroy(allSpawnedAtoms[i].gameObject);
		}
		allSpawnedAtoms.Clear();

		FirstCollided = null;
		SecondCollided = null;

		NumberOfInteracatbleAtoms = 0;
	}

	public void ResetAllAtomArrows()
	{
		foreach (var atom in allSpawnedAtoms)
		{
			atom.PaintArrow(Vector2.zero);
		}
	}

	public void SpawnAtom(int atomicNumber, bool randomSpawnPoint = true, Vector2 pSpawnPoint = default, Vector2 startingForce = default)
	{
		if (randomSpawnPoint)
			MoveSpawnPoint();
		else
			spawnPoint.localPosition = pSpawnPoint;

		if (atomicNumber > allAtomNames.Count - 1)
		{
			atomicNumber = 118;
		}

		GameObject createdAtom = Instantiate(atomPrefab, playArea);

		Atom atomScript = createdAtom.GetComponent<Atom>();
		createdAtom.transform.localPosition = spawnPoint.localPosition;
		atomScript.SetData(allAtomNames[atomicNumber], atomicNumber, allAtomColors[atomicNumber]);
		atomScript.ApplyForce(startingForce);

		allSpawnedAtoms.Add(atomScript);
	}

	public void DeleteAtom(Atom atomToDelete)
	{
		if (atomToDelete.atomicNumber == 1)
			NumberOfInteracatbleAtoms--;

		allSpawnedAtoms.Remove(atomToDelete);

		Destroy(atomToDelete.gameObject);
	}

	private void MoveSpawnPoint()
	{
		float xPos = UnityEngine.Random.Range(-8.5f, 8.5f);
		float yPos = UnityEngine.Random.Range(-4.5f, 4.5f);

		spawnPoint.localPosition = new Vector2(xPos, yPos);

		while (IsSpawnPointInsideAnyAtom())
		{
			xPos = UnityEngine.Random.Range(-8.5f, 8.5f);
			yPos = UnityEngine.Random.Range(-4.5f, 4.5f);

			spawnPoint.localPosition = new Vector2(xPos, yPos);
		}
	}

	private bool IsSpawnPointInsideAnyAtom()
	{
		foreach (var atom in allSpawnedAtoms)
		{
			if (atom.Collider2d.OverlapPoint(spawnPoint.localPosition))
				return true;
		}

		return false;
	}
}
