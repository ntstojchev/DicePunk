using UnityEngine;
using UnityEngine.UI;

public class EventsDisplayController : MonoBehaviour
{
	public GameObject ContentGroup;
	public Text Content;
	public Text PopulationPrediction;
	public Text FoodPrediction;
	public Text ArmyPrediction;
	public Text ConfidencePrediction;

	public Color PositivePredictionColor;
	public Color NegativePredictionColor;

	public void SetEvent(YearEvent yearEvent)
	{
		Content.text = yearEvent.Content;

		SetResourcePrediction(yearEvent.PopulationAlter, PopulationPrediction);
		SetResourcePrediction(yearEvent.FoodAlter, FoodPrediction);
		SetResourcePrediction(yearEvent.ArmyAlter, ArmyPrediction);
		SetResourcePrediction(yearEvent.ConfidenceAlter, ConfidencePrediction);
	}

	private void SetResourcePrediction(int resourceValue, Text content)
	{
		if (resourceValue > 0 && resourceValue <= 2) {
			content.text = "+";
		} else if (resourceValue > 2 && resourceValue <= 4) {
			content.text = "++";
		}
		else if (resourceValue > 4 && resourceValue <= 6) {
			content.text = "+++";
		}
		else if (resourceValue > 6 && resourceValue <= 8) {
			content.text = "++++";
		}
		else if (resourceValue > 8 && resourceValue <= 10) {
			content.text = "+++++";
		}

		if (resourceValue >= -2 && resourceValue < 0) {
			content.text = "-";
		}
		else if (resourceValue >= -4 && resourceValue < -2 ) {
			content.text = "--";
		}
		else if (resourceValue >= -6 && resourceValue < -4) {
			content.text = "---";
		}
		else if (resourceValue >= -8 && resourceValue < -6) {
			content.text = "----";
		}
		else if (resourceValue >= -10 && resourceValue <= -8) {
			content.text = "-----";
		}

		if (resourceValue == 0) {
			content.text = "";
		}

		if (resourceValue > 0) {
			content.color = PositivePredictionColor;
		} else {
			content.color = NegativePredictionColor;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
