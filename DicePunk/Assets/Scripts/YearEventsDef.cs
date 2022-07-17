using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YearEventsDef", menuName = "ScriptableObjects/YearEventsDef", order = 1)]
public class YearEventsDef : ScriptableObject
{
	public List<YearEvent> Events;
	public int StartingFood;
	public int StartingPopulation;
	public int StartingArmy;
	public int StartingConfidence;
	public int ResourcesMax = 40;
}
