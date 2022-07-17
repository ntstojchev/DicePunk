using System;
using UnityEngine;

[Serializable]
public class TownResourcesManager
{
	public int Food = 0;
	public int Population = 0;
	public int Army = 0;
	public int Confidence = 0;

	public int Max { get { return _max; } }

	public Action OnResourcesChanged;

	[SerializeField]
	private int _max;

	public void SetMax(int max)
	{
		_max = max;
	}

	public void ChangeResources(int f, int p, int a, int c)
	{
		Food += f;
		Population += p;
		Army += a;
		Confidence += c;

		OnResourcesChanged?.Invoke();
	}
}
