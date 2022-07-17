using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Die : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,
	IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	[Header("Components")]
	public UIManager UI;
	public Animator Animator;
	public Button ReRollButton;
	public RectTransform VisualRoot;
	public Image RaycastHandler;

	[Header("Data")]
	public int SideValue = 0;

	public Action<Die> RerollButtonClicked;

	private Vector3 _lastKnownPosition;

	private void Start()
	{
		ReRollButton?.onClick.AddListener(OnReRollButtonClicked);
		ReRollButton?.gameObject.SetActive(false);

		_lastKnownPosition = Vector3.zero;
	}

	private void OnReRollButtonClicked()
	{
		ResetDie();

		RerollButtonClicked?.Invoke(this);
	}

	public void SetDie(Die die)
	{
		if (die.SideValue != 0) {
			SideValue = die.SideValue;

			SetDieSideAnimState(SideValue);

			if (UI?.CurrentAllowedRerolls > 0) {
				ReRollButton?.gameObject.SetActive(true);
			}
		}
	}

	public void SetDie()
	{
		if (SideValue == 0) {
			int pickSide = UnityEngine.Random.Range(1, 7);
			SideValue = pickSide;

			SetDieSideAnimState(SideValue);
			SetDieAlignmentAnimState(0);

			if (UI?.CurrentAllowedRerolls > 0) {
				ReRollButton?.gameObject.SetActive(true);
			}
		}
	}

	public void ResetDie()
	{
		SetDieSideAnimState(0);
		SideValue = 0;

		ReRollButton?.gameObject.SetActive(false);

		gameObject.SetActive(true);

		VisualRoot.anchoredPosition = Vector2.zero;
		RaycastHandler.raycastTarget = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (SideValue == 0) {
			int pickSide = UnityEngine.Random.Range(1, 7);
			SideValue = pickSide;

			SetDieSideAnimState(SideValue);

			if (UI?.CurrentAllowedRerolls > 0) {
				ReRollButton?.gameObject.SetActive(true);
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		SetDieHoverAnimState(true);

		UI.CurrentlyHoveredObject = VisualRoot;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		SetDieHoverAnimState(false);
		UI.CurrentlyHoveredObject = null;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (SideValue == 0) {
			return;
		}

		_lastKnownPosition = VisualRoot.position;
		ReRollButton?.gameObject.SetActive(false);

		RaycastHandler.raycastTarget = false;

		UI.CurrentlyDraggedObject = VisualRoot;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (SideValue == 0) {
			return;
		}

		UI.CurrentlyDraggedObject = null;

		if (UI.CurrentlyHoveredObject != null) {
			DieSlot dieSlot = UI.CurrentlyHoveredObject.gameObject.GetComponent<DieSlot>();
			if (dieSlot != null) {
				dieSlot.AssignDie(this);

				gameObject.SetActive(false);
			}
			else {
				VisualRoot.position = _lastKnownPosition;
				if (UI?.CurrentAllowedRerolls > 0) {
					ReRollButton?.gameObject.SetActive(true);
				}

				RaycastHandler.raycastTarget = true;
			}
		}
		else {
			VisualRoot.position = _lastKnownPosition;
			if (UI?.CurrentAllowedRerolls > 0) {
				ReRollButton?.gameObject.SetActive(true);
			}

			RaycastHandler.raycastTarget = true;
		}
	}

	public void SetDieSideAnimState(int sideValue)
	{
		Animator.SetInteger("DieState", sideValue);
	}

	public void SetDieHoverAnimState(bool isHover)
	{
		Animator.SetBool("IsHover", isHover);
	}

	public void SetDieAlignmentAnimState(int alignment)
	{
		Animator.SetInteger("Alignment", alignment);
	}
}
