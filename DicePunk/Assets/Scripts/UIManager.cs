using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Action EndOfYearButtonClicked;
	public Action RestartGameClicked;

	public GameObject GameContent;
	public Button QuitGame;
	public Button RestartGameButton;

	public List<Die> PlayDice;
	public List<DieSlot> SlotDice;

	public ResourcesUIController Resources;
	public EventsDisplayController EventsDisplay;

	public RectTransform CurrentlyDraggedObject;
	public RectTransform CurrentlyHoveredObject;

	public Button RerollButton;
	public Button EndYearButton;
	public Text RerollText;
	public Text YearText;

	public GameObject GameEndContent;

	[Multiline]
	public string EndGameTextContent;
	public Text EndGameText;

	public int GameYears;
	public int MaxAllowedRerolls = 3;
	public int CurrentAllowedRerolls = 0;

	public GameObject TutorialContent;
	public Button NextTutorialButton;
	public int TutorialIndex = 0;

	// Start is called before the first frame update
	void Start()
	{
		RerollButton.onClick.AddListener(OnRerollButtonClicked);
		EndYearButton.onClick.AddListener(OnEndYearButtonClicked);
		QuitGame.onClick.AddListener(OnQuitGameButtonClicked);
		RestartGameButton.onClick.AddListener(OnRestartButtonClicked);
		NextTutorialButton.onClick.AddListener(OnNextTutorialButtonClicked);

		foreach (Die die in PlayDice) {
			die.UI = this;
			die.RerollButtonClicked += OnDieRerollButtonClicked;
		}

		foreach (DieSlot slot in SlotDice) {
			slot.UI = this;
			slot.AssignedDie.SideValue = 0;
		}
	}

	private void OnNextTutorialButtonClicked()
	{
		if (TutorialIndex == TutorialContent.transform.childCount - 1) {
			NextTutorialButton.gameObject.SetActive(false);
			TutorialContent.gameObject.SetActive(false);

			GameContent.gameObject.SetActive(true);
		}
		else {
			TutorialContent.transform.GetChild(TutorialIndex).gameObject.SetActive(false);

			TutorialIndex++;

			TutorialContent.transform.GetChild(TutorialIndex).gameObject.SetActive(true);
		}
	}

	private void OnRestartButtonClicked()
	{
		RestartGameClicked?.Invoke();
	}

	private void OnQuitGameButtonClicked()
	{
		Application.Quit();
	}

	public void SetNewYear()
	{
		foreach (Die die in PlayDice) {
			die.ResetDie();
		}

		foreach (DieSlot slot in SlotDice) {
			slot.ResetSlot();
		}

		SetRerollText(MaxAllowedRerolls);
		CurrentAllowedRerolls = MaxAllowedRerolls;

		YearText.text = $"YEAR {GameYears}";
	}

	private void OnEndYearButtonClicked()
	{
		EndOfYearButtonClicked?.Invoke();
	}

	private void OnRerollButtonClicked()
	{
		if (CurrentAllowedRerolls > 0) {
			CurrentAllowedRerolls--;

			foreach (Die die in PlayDice) {
				if (die.gameObject.activeInHierarchy) {
					die.ResetDie();
				}
			}

			SetRerollText(CurrentAllowedRerolls);
		}
	}

	private void OnDieRerollButtonClicked(Die die)
	{
		if (CurrentAllowedRerolls > 0) {
			CurrentAllowedRerolls--;

			SetRerollText(CurrentAllowedRerolls);
		}

		if (CurrentAllowedRerolls == 0) {
			foreach (Die playDie in PlayDice) {
				playDie.ReRollButton?.gameObject.SetActive(false);
			}
		}
	}

	private void SetRerollText(int rerolls)
	{
		RerollText.text = rerolls.ToString();
	}

	// Update is called once per frame
	void Update()
    {
        if (CurrentlyDraggedObject != null) {
			CurrentlyDraggedObject.position = Input.mousePosition;
		}

		bool isPopulationPlaced = false;
		bool isArmyPlaced = false;
		bool isFoodPlaced = false;
		bool isConfidencePlaced = false;
		if (GameContent.activeInHierarchy) {
			foreach (DieSlot slot in SlotDice) {
				if (slot.AssignedDie.SideValue > 0 && slot.ResourceType == ResourceType.Army) {
					isArmyPlaced = true;
				}

				if (slot.AssignedDie.SideValue > 0 && slot.ResourceType == ResourceType.Food) {
					isFoodPlaced = true;
				}

				if (slot.AssignedDie.SideValue > 0 && slot.ResourceType == ResourceType.Confidence) {
					isConfidencePlaced = true;
				}

				if (slot.AssignedDie.SideValue > 0 && slot.ResourceType == ResourceType.Population) {
					isPopulationPlaced = true;
				}
			}

			EndYearButton.interactable = isArmyPlaced && isFoodPlaced && isConfidencePlaced && isPopulationPlaced;
		}
    }
}
