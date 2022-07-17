using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("Data")]
	public YearEventsDef Events;
	public int GameYears = 0;

	[Header("Controllers")]
	public UIManager UI;
	public TownResourcesManager Resources = new TownResourcesManager();

	public YearEvent CurrentEvent;
	[SerializeField]
	private List<YearEvent> _currentEventQueue = new List<YearEvent>();

	public void Start()
	{
#if UNITY_WEBGL
		UI.QuitGame.gameObject.SetActive(false);
#endif

		Resources.ChangeResources(Events.StartingFood, Events.StartingPopulation, Events.StartingArmy, Events.StartingConfidence);
		Resources.SetMax(Events.ResourcesMax);

		UI.EndOfYearButtonClicked += OnEndOfYearButtonClicked;
		UI.RestartGameClicked += OnRestarGameClicked;

		UI.Resources.TownResources = Resources;
		UI.Resources.UpdateDisplay();

		StartNewYear();
	}

	private void OnEndOfYearButtonClicked()
	{
		AggregateResources();

		if (Resources.Army < 0 || Resources.Army > Events.ResourcesMax) {
			GameEnd();
		}
		else if (Resources.Food < 0 || Resources.Food > Events.ResourcesMax) {
			GameEnd();
		}
		else if (Resources.Confidence < 0 || Resources.Confidence > Events.ResourcesMax) {
			GameEnd();
		}
		else if (Resources.Population < 0 || Resources.Population > Events.ResourcesMax) {
			GameEnd();
		}
		else {
			StartNewYear();
		}
	}

	private void AggregateResources()
	{
		foreach (DieSlot slot in UI.SlotDice) {
			int multiplier = slot.IsPositiveAlignment ? 1 : -1;

			if (slot.ResourceType == ResourceType.Army) {
				Resources.Army += multiplier * slot.AssignedDie.SideValue;
			}
			else if (slot.ResourceType == ResourceType.Confidence) {
				Resources.Confidence += multiplier * slot.AssignedDie.SideValue;
			}
			else if (slot.ResourceType == ResourceType.Food) {
				Resources.Food += multiplier * slot.AssignedDie.SideValue;
			}
			else if (slot.ResourceType == ResourceType.Population) {
				Resources.Population += multiplier * slot.AssignedDie.SideValue;
			}
		}

		Resources.ChangeResources(CurrentEvent.FoodAlter, CurrentEvent.PopulationAlter, CurrentEvent.ArmyAlter, CurrentEvent.ConfidenceAlter);
	}

	private void StartNewYear()
	{
		GameYears++;

		UI.Resources.UpdateDisplay();

		UI.GameYears = GameYears;
		UI.SetNewYear();

		if (_currentEventQueue.Count == 0) {
			_currentEventQueue.AddRange(Events.Events);
		}

		int index = UnityEngine.Random.Range(0, _currentEventQueue.Count);
		YearEvent yearEvent = _currentEventQueue[index];

		_currentEventQueue.Remove(yearEvent);

		CurrentEvent = yearEvent;

		UI.EventsDisplay.SetEvent(CurrentEvent);
	}

	private void GameEnd()
	{
		UI.GameContent.SetActive(false);
		UI.GameEndContent.SetActive(true);

		UI.EndGameText.text = string.Format(UI.EndGameTextContent, GameYears);
	}

	private void OnRestarGameClicked()
	{
		Resources.Army = 0;
		Resources.Food= 0;
		Resources.Confidence = 0;
		Resources.Population = 0;

		Resources.ChangeResources(Events.StartingFood, Events.StartingPopulation, Events.StartingArmy, Events.StartingConfidence);
		Resources.SetMax(Events.ResourcesMax);

		GameYears = 0;

		UI.GameContent.SetActive(true);
		UI.GameEndContent.SetActive(false);

		StartNewYear();
	}
}
