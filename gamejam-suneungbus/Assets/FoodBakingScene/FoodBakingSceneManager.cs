using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBakingSceneManager : MonoBehaviour {
	private float lastCountedTime = 0;

	private Slider fireSlider;
	private Slider waterSlider;
	private Slider foodSlider;

	private Text counterText;
	private Text beefText;
	private Text fishText;
	private Text potatoText;
	private Text timerText;
	private Text heartText;

	private GameObject buttonGameObject;

	private bool isStarted = false;

	private float timer = 0;
	private int falseCount = 0; //이 카운터가 falseCountTorealCount까지늘어나면
	private int realCount = 0; //이 카운터가 1늘어난다

	public int falseCountTorealCount = 5; //이 카운터는 false카운터가 real카운터로 변하는 한계값이다.

	public Animator anim; //애니메이션을 받아온다

	// Use this for initialization
	void Start () {
		GameObject content = GameObject.Find ("CounterText");
		counterText = content.GetComponent<Text> ();

		beefText = GameObject.Find("BeefText").GetComponent<Text>();
		fishText = GameObject.Find("FishText").GetComponent<Text>();
		potatoText = GameObject.Find("PotatoText").GetComponent<Text>();

		fireSlider = GameObject.Find("FireSlider").GetComponent<Slider>();
		waterSlider = GameObject.Find("WaterSlider").GetComponent<Slider>();
		foodSlider = GameObject.Find("FoodSlider").GetComponent<Slider>();

		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		heartText = GameObject.Find ("HeartText").GetComponent<Text> ();

		// test
		// SManager.GetInstance ().heart = 5;
		heartText.text = SManager.GetInstance ().Heart.ToString () + "/" + ValueTable.GlobalTable.heartMax;

		anim.enabled = false; //누르지 않을 시 애니메이션을 멈춘다

		SManager.GetInstance ().Beef = 100;
		SManager.GetInstance ().Fish = 100;
		SManager.GetInstance ().Potato = 100;
	}

	// Update is called once per frame
	void Update () {
		fireSlider.value = SManager.GetInstance ().getFire();
		waterSlider.value = SManager.GetInstance ().getWater();
		foodSlider.value = SManager.GetInstance ().getFood();

		if (!isStarted) {
			return;
		}

		anim.enabled = false; //누르지 않을 시 애니메이션을 멈춘다

		if (Input.GetMouseButton (0)) {
			if (SManager.GetInstance ().Beef >= ValueTable.FoodBakingScene.clickPerBeef &&
			     SManager.GetInstance ().Fish >= ValueTable.FoodBakingScene.clickPerFish &&
			     SManager.GetInstance ().Potato >= ValueTable.FoodBakingScene.clickPerPotato) {

				SManager.GetInstance ().Beef -= ValueTable.FoodBakingScene.clickPerBeef;
				SManager.GetInstance ().Fish -= ValueTable.FoodBakingScene.clickPerFish;
				SManager.GetInstance ().Potato -= ValueTable.FoodBakingScene.clickPerPotato;

				anim.enabled = true; //누를 시에 애니메이션을 재생시키고
				falseCount++;
				if (falseCount == falseCountTorealCount) {
					falseCount = 0;
					realCount += 1;
				}
				lastCountedTime = timer;
			}
		}
		counterText.text = realCount.ToString ();

		beefText.text = SManager.GetInstance ().Beef.ToString();
		fishText.text = SManager.GetInstance ().Fish.ToString();
		potatoText.text = SManager.GetInstance ().Potato.ToString();

		if (realCount != 0 && (realCount % ValueTable.FoodBakingScene.countPerFood == 0) && lastCountedTime == timer)
		{
			SManager.GetInstance().Food++;
		}

		timer += Time.deltaTime;
		if (timer >= (ValueTable.FoodBakingScene.timeLimit / 1000)) {
			// TODO: End of Scene
		}
	}

	public void startGameButton() {
		if (SManager.GetInstance ().Heart <= 0) {
			return;
		}

		SManager.GetInstance ().Heart--;
		heartText.text = SManager.GetInstance ().Heart.ToString () + "/" + ValueTable.GlobalTable.heartMax;

        if (SManager.GetInstance().Heart == 0)
            SceneManager2.GetInstance().ChangeScene(5);

        timerText.text = (ValueTable.FireMakeScene.timeLimit / 1000).ToString ();
		timer = 0;

		buttonGameObject = GameObject.Find ("StartGameButton");
		buttonGameObject.SetActive (false);
		isStarted = true;
	}

	public void endGame() {
		buttonGameObject.SetActive (true);
		isStarted = false;
	}

	public void backButtonPressed() {
        // TODO;
        SceneManager2.GetInstance().ChangeScene(0);
	}
}
