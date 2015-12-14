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

	bool alreadyPlayed = false;
	bool hasPoped = false;

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

	void popPanel()
	{
		iTween.ScaleTo (gameObject, iTween.Hash ("scale", Vector3.one, "time", 0.5f, "easetype", iTween.EaseType.easeOutBack, "ignoretimescale",true));
	}

	void unpopPanel()
	{
		iTween.ScaleTo (gameObject, iTween.Hash ("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeInBack, "ignoretimescale",true));		
	}

	// Update is called once per frame
	void Update () 
	{
		if (alreadyPlayed)
			return;

		if (isPanelActive) 
		{
			if(!hasPoped)
			{

				continueText.gameObject.transform.parent = transform;
				transform.localScale = Vector3.zero;

				popPanel();
				hasPoped = true;
			}
			GameManager.PanelPause();
			if(Input.GetButtonDown("Submit"))
			{
				idText++;
				if(idText == panelTexts.Count)
				{
					unpopPanel();

					/*isPanelActive = false;
					GetComponent<Image>().enabled = false;
					foreach (Text text in panelTexts) 
						text.enabled = false;
					*/

					continueText.enabled = false;
					continueText.gameObject.transform.parent = transform.parent;
					GameManager.PanelUnpause();
					alreadyPlayed = true;
					GameManager.gameManager.GetComponent<ObjectifManager>().endLearningObjectif();
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
