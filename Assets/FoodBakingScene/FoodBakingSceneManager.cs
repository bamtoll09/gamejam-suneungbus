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

    private Text fireText;
    private Text waterText;
    private Text foodText;

    private GameObject buttonGameObject;
    private GameObject debugger;

    private SpriteRenderer foodSpriteRenderer;

    private BoxCollider2D touchableArea;

    private bool isStarted = false;
    public bool isDebugging;

    private float timer = 0;
	private int falseCount = 0; //이 카운터가 falseCountTorealCount까지늘어나면
	private int realCount = 0; //이 카운터가 1늘어난다

	public int falseCountTorealCount = 5; //이 카운터는 false카운터가 real카운터로 변하는 한계값이다.

	public Animator anim; //애니메이션을 받아온다

	// Use this for initialization
	void Start () {
		GameObject content = GameObject.Find ("CounterText");
		counterText = content.GetComponent<Text> ();

        content = GameObject.Find("food");
        foodSpriteRenderer = content.GetComponent<SpriteRenderer> ();
        touchableArea = content.GetComponent<BoxCollider2D> ();

		beefText = GameObject.Find("BeefText").GetComponent<Text>();
		fishText = GameObject.Find("FishText").GetComponent<Text>();
		potatoText = GameObject.Find("PotatoText").GetComponent<Text>();

		fireSlider = GameObject.Find("FireSlider").GetComponent<Slider>();
		waterSlider = GameObject.Find("WaterSlider").GetComponent<Slider>();
		foodSlider = GameObject.Find("FoodSlider").GetComponent<Slider>();

		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		heartText = GameObject.Find ("HeartText").GetComponent<Text> ();

        debugger = GameObject.Find("Debugger");

        // test
        // SManager.GetInstance ().heart = 5;
        heartText.text = SManager.GetInstance ().heart.ToString () + "/" + ValueTable.GlobalTable.heartMax;

        fireSlider.value = SManager.GetInstance().getFire();
        waterSlider.value = SManager.GetInstance().getWater();
        foodSlider.value = SManager.GetInstance().getFood();

        beefText.text = SManager.GetInstance().beef.ToString();
        fishText.text = SManager.GetInstance().fish.ToString();
        potatoText.text = SManager.GetInstance().potato.ToString();
        
        // anim.enabled = false; //누르지 않을 시 애니메이션을 멈춘다

        timerText.text = (ValueTable.FireMakeScene.timeLimit / 1000).ToString();

        if (isDebugging)
        {
            fireText = GameObject.Find("Fire Value").GetComponent<Text>();
            waterText = GameObject.Find("Water Value").GetComponent<Text>();
            foodText = GameObject.Find("Food Value").GetComponent<Text>();

            fireText.text = "Fire: " + fireSlider.value;
            waterText.text = "Water: " + waterSlider.value;
            foodText.text = "Food: " + foodSlider.value;
        }
        else { debugger.SetActive(false); }
    }

	// Update is called once per frame
	void Update () {
		if (!isStarted) {
			return;
		} else {
            if (SManager.GetInstance().beef >= ValueTable.FoodBakingScene.clickPerBeef &&
                SManager.GetInstance().potato >= ValueTable.FoodBakingScene.clickPerPotato &&
                SManager.GetInstance().fish >= ValueTable.FoodBakingScene.clickPerFish &&
                SManager.GetInstance().water >= ValueTable.FoodBakingScene.clickPerWater &&
                SManager.GetInstance().food < 100)
            {
                if (touchableArea.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!anim.GetBool("making"))
                        {
                            anim.SetBool("making", true);

                            SManager.GetInstance().beef -= ValueTable.FoodBakingScene.clickPerBeef;
                            SManager.GetInstance().potato -= ValueTable.FoodBakingScene.clickPerPotato;
                            SManager.GetInstance().fish -= ValueTable.FoodBakingScene.clickPerFish;
                            SManager.GetInstance().water -= ValueTable.FoodBakingScene.clickPerWater;

                            beefText.text = SManager.GetInstance().beef.ToString();
                            fishText.text = SManager.GetInstance().fish.ToString();
                            potatoText.text = SManager.GetInstance().potato.ToString();
                            waterSlider.value = SManager.GetInstance().getWater();

                            if (isDebugging)
                                waterText.text = "Water: " + waterSlider.value;
                        }
                    }
                    else if (Input.GetMouseButton(0)) { anim.enabled = true; }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        switch (foodSpriteRenderer.sprite.name)
                        {
                            case "•ÿ_1":
                            case "•ÿ_2":
                                anim.enabled = false;
                                break;

                            case "•ÿ_3":
                                anim.SetBool("making", false);

                                SManager.GetInstance().food++;
                                foodSlider.value = SManager.GetInstance().getFood();

                                if (isDebugging)
                                    foodText.text = "Food: " + foodSlider.value;
                                break;

                            case "•ÿ_4":
                                anim.SetBool("making", false);
                                break;

                            default:
                                break;
                        }
                    }
                }
            } else { endGame(); }

            timer += Time.deltaTime;
            timerText.text = Mathf.CeilToInt((ValueTable.FireMakeScene.timeLimit / 1000) - timer).ToString();
        }

        /*
		anim.enabled = false; //누르지 않을 시 애니메이션을 멈춘다

		if (Input.GetMouseButton (0)) {
			if (SManager.GetInstance ().beef >= ValueTable.FoodBakingScene.clickPerBeef &&
			     SManager.GetInstance ().fish >= ValueTable.FoodBakingScene.clickPerFish &&
			     SManager.GetInstance ().potato >= ValueTable.FoodBakingScene.clickPerPotato) {

				SManager.GetInstance ().beef -= ValueTable.FoodBakingScene.clickPerBeef;
				SManager.GetInstance ().fish -= ValueTable.FoodBakingScene.clickPerFish;
				SManager.GetInstance ().potato -= ValueTable.FoodBakingScene.clickPerPotato;

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

		beefText.text = SManager.GetInstance ().beef.ToString();
		fishText.text = SManager.GetInstance ().fish.ToString();
		potatoText.text = SManager.GetInstance ().potato.ToString();

		if (realCount != 0 && (realCount % ValueTable.FoodBakingScene.countPerFood == 0) && lastCountedTime == timer)
		{
			SManager.GetInstance().food++;
		}
        */

		if (timer >= (ValueTable.FoodBakingScene.timeLimit / 1000)) {
            // TODO: End of Scene
            endGame();
		}
	}

	public void startGameButton() {
		if (SManager.GetInstance ().heart <= 0 || (
                SManager.GetInstance().beef < ValueTable.FoodBakingScene.clickPerBeef ||
                SManager.GetInstance().potato < ValueTable.FoodBakingScene.clickPerPotato ||
                SManager.GetInstance().fish < ValueTable.FoodBakingScene.clickPerFish ||
                SManager.GetInstance().water < ValueTable.FoodBakingScene.clickPerWater ||
                SManager.GetInstance().food >= 100)) {
			return;
		}

		SManager.GetInstance ().heart--;
		heartText.text = SManager.GetInstance ().heart.ToString () + "/" + ValueTable.GlobalTable.heartMax;

        timerText.text = (ValueTable.FireMakeScene.timeLimit / 1000).ToString ();
		timer = 0;

		buttonGameObject = GameObject.Find ("StartGameButton");
		buttonGameObject.SetActive (false);
		isStarted = true;
	}

	public void endGame() {
		buttonGameObject.SetActive (true);
		isStarted = false;
        anim.SetBool("making", false);
    }

	public void backButtonPressed() {
        // TODO;
        SceneManager2.GetInstance().ChangeScene(0);
	}
}
