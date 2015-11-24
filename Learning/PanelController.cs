using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class PanelController : MonoBehaviour 
{
	int idText = 0;
	public bool isPanelActive = false;
	public List<Text> panelTexts = new List<Text>();

	Text continueText;
	Image panelImage;

	// Use this for initialization
	void Start () 
	{
		foreach (Text text in GetComponentsInChildren<Text>()) 
		{
			panelTexts.Add(text);
		}

		continueText = GameObject.Find ("PressEnter").GetComponent<Text> ();
		panelImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPanelActive) 
		{
			GameManager.Pause();
			if(Input.GetButtonDown("Submit"))
			{
				idText++;
				if(idText == panelTexts.Count)
				{
					isPanelActive = false;
					GetComponent<Image>().enabled = false;
					foreach (Text text in panelTexts) 
						text.enabled = false;

					continueText.enabled = false;
					GameManager.Unpause();

					return;
				}
			}

			panelImage.enabled = true;
			continueText.enabled = true;
			foreach (Text text in panelTexts) 
				text.enabled = false;

			panelTexts [idText].enabled = true;
		} 
	
	}
}
