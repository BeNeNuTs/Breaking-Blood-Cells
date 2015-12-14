using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharactersController : MonoBehaviour {
	
	public Sprite[] characters;
	int index = 0;

	Image image; 

	void Start(){
		image = GetComponent<Image>();

		UpdateImage();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			index--;
			if(index < 0){
				index = characters.Length - 1;
			}

			UpdateImage();
		}else if(Input.GetKeyDown(KeyCode.RightArrow)){
			index++;
			if(index > characters.Length - 1){
				index = 0;
			}

			UpdateImage();
		}
	}

	void UpdateImage(){
		image.sprite = characters[index];
	}
}
