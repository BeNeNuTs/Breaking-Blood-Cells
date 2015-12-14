using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogBubleController : Cutscene {

	int idText = 0;
	public bool isPanelActive = false;
	public List<Text> panelTexts = new List<Text>();

	Image panelImage;

	public float delayBetweenTexts = 1.0f;
	public float delayBeforeBeginning = 1.0f;
	public float delayBeforeNextObjective = 1.0f;

	// Use this for initialization
	void Start () 
	{
		foreach (Text text in GetComponentsInChildren<Text>()) 
		{
			panelTexts.Add(text);
		}

		panelImage = GetComponent<Image> ();

		//StopCoroutine (playBubble (delayBetweenTexts));
		StartCoroutine (playCutscene());


	
	}

	IEnumerator playCutscene()
	{
		yield return new WaitForSeconds (delayBeforeBeginning);
		StartCoroutine (playBubble (delayBetweenTexts));
		yield return new WaitForSeconds (delayBeforeNextObjective);
		GameManager.gameManager.GetComponent<ObjectifManager>().updateCutsceneGoal();
	}

	IEnumerator playBubble(float delayBetweenTexts)
	{

		while(idText != panelTexts.Count)
		{
			panelImage.enabled = true;
			foreach (Text text in panelTexts) 
				text.enabled = false;
			
			panelTexts [idText].enabled = true;

			idText++;
			yield return new WaitForSeconds(delayBetweenTexts);
		}

		isPanelActive = false;
		GetComponent<Image>().enabled = false;
		foreach (Text text in panelTexts) 
			text.enabled = false;

	} 


}
