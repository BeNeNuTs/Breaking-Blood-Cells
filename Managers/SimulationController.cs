using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class SimulationController : MonoBehaviour {

	[System.Serializable]
	public class ParamSlider {
		public string name;
		public Slider slider;
		public Text Label;
	}
	[System.Serializable]
	public class ParamInputField {
		public string name;
		public InputField input;
	}

	public LevelAgentManager levelAgentManager;
	
	public ParamSlider[] sliders;
	public ParamInputField[] inputFields;

	public Toggle toggleBacteria;
	public Toggle toggleVirus;

	Dictionary<string, ParamSlider> slidersDictionary;
	Dictionary<string, ParamInputField> inputFieldsDictionary;

	MovePanel movePanelScript;

	void Start(){
		slidersDictionary = new Dictionary<string, ParamSlider>();
		inputFieldsDictionary = new Dictionary<string, ParamInputField>();

		foreach(ParamSlider pSlider in sliders){
			slidersDictionary.Add(pSlider.name, pSlider);
		}

		foreach(ParamInputField pInputField in inputFields){
			inputFieldsDictionary.Add(pInputField.name, pInputField);
		}

		movePanelScript = GetComponent<MovePanel>();

		Init ();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			movePanelScript.SwitchPanel();
		}
	}

	void Init(){
		//INIT SLIDERS
		slidersDictionary["SpawnRateMacro"].slider.value = levelAgentManager.spawnRateMacrophage;
		slidersDictionary["SpawnRateMacro"].Label.text = slidersDictionary["SpawnRateMacro"].slider.value.ToString();

		slidersDictionary["SpawnRateLT"].slider.value = levelAgentManager.spawnRateLTAux;
		slidersDictionary["SpawnRateLT"].Label.text = slidersDictionary["SpawnRateLT"].slider.value.ToString();

		slidersDictionary["SpawnRateLB"].slider.value = levelAgentManager.spawnRateLB;
		slidersDictionary["SpawnRateLB"].Label.text = slidersDictionary["SpawnRateLB"].slider.value.ToString();

		slidersDictionary["SpawnRateEnemy"].slider.value = levelAgentManager.spawnRateEnemy;
		slidersDictionary["SpawnRateEnemy"].Label.text = slidersDictionary["SpawnRateEnemy"].slider.value.ToString();

		//INIT INPUT FIELDS
		inputFieldsDictionary["MaxMacro"].input.text = levelAgentManager.maxMacrophage.ToString();
		inputFieldsDictionary["MaxLT"].input.text = levelAgentManager.maxLT.ToString();
		inputFieldsDictionary["MaxLB"].input.text = levelAgentManager.maxLB.ToString();
		inputFieldsDictionary["MaxEnemy"].input.text = levelAgentManager.maxEnemy.ToString();
	}

	public void OnChangeTimeScale(){
		levelAgentManager.timeScale = slidersDictionary["TimeScale"].slider.value;
		slidersDictionary["TimeScale"].Label.text = slidersDictionary["TimeScale"].slider.value.ToString();
	}

	public void OnChangeEnemy(){
		if(toggleBacteria.isOn){
			Debug.Log("Bacteria");
		}else{
			Debug.Log("Virus");
		}
	}

	public void OnChangeInputField(string name){

		/*if(inputFieldsDictionary[name].input.text){
			max = int.Parse(inputFieldsDictionary[name].input.text);
		}*/

		int max = 0;

		if(inputFieldsDictionary[name].input.text != ""){
			int result = 0;
			bool res = int.TryParse(inputFieldsDictionary[name].input.text, out result);
			if(res)
				max = result;
			else
				return;
				              
		}

		if(max > 100){
			max = 100;
			inputFieldsDictionary[name].input.text = "100";
		}else if(max < 0){
			max = 0;
			inputFieldsDictionary[name].input.text = "0";
		}
	
		switch(name){
		case "MaxMacro":
			levelAgentManager.maxMacrophage = max;
			break;

		case "MaxLT":
			levelAgentManager.maxLT = max;
			break;

		case "MaxLB":
			levelAgentManager.maxLB = max;
			break;

		case "MaxEnemy":
			levelAgentManager.maxEnemy = max;
			break;

		default:
			break;
		}
	}

	public void OnChangeSpawnRate(string name){
		float spawnRate = slidersDictionary[name].slider.value;
		slidersDictionary[name].Label.text = slidersDictionary[name].slider.value.ToString();
		
		switch(name){
		case "SpawnRateMacro":
			levelAgentManager.spawnRateMacrophage = spawnRate;
			break;

		case "SpawnRateLT":
			levelAgentManager.spawnRateLTAux = spawnRate;
			levelAgentManager.spawnRateLTCyto = spawnRate;
			break;
			
		case "SpawnRateLB":
			levelAgentManager.spawnRateLB = spawnRate;
			break;
			
		case "SpawnRateEnemy":
			levelAgentManager.spawnRateEnemy = spawnRate;
			break;
			
		default:
			break;
		}
	}
}
