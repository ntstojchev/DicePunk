using UnityEngine;
using UnityEngine.EventSystems;

public class DieSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Components")]
	public UIManager UI;
	public Die AssignedDie;

	public ResourceType ResourceType;
	public GameObject PositiveAlignmentObject;
	public GameObject NegativeAlignmentObject;
	public bool IsPositiveAlignment = false;

	private void Start()
	{
		AssignedDie.gameObject.SetActive(false);
		PositiveAlignmentObject.SetActive(IsPositiveAlignment);
		NegativeAlignmentObject.SetActive(!IsPositiveAlignment);
	}

	public void AssignDie(Die die)
	{
		AssignedDie.gameObject.SetActive(true);

		AssignedDie.SetDie(die);

		if (IsPositiveAlignment) {
			AssignedDie.SetDieAlignmentAnimState(1);
		}
		else {
			AssignedDie.SetDieAlignmentAnimState(-1);
		}
	}

	public void ResetSlot()
	{
		AssignedDie.gameObject.SetActive(false);
		AssignedDie.SideValue = 0;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//throw new System.NotImplementedException();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		UI.CurrentlyHoveredObject = GetComponent<RectTransform>();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		UI.CurrentlyHoveredObject = null;
	}
}
