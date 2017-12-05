using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterFilteringScene : MonoBehaviour {

	public Sprite[] sprites;
	public SpriteRenderer water;

    public bool isDebugging;

    private Slider fireSlider;
	private Slider waterSlider;
	private Slider foodSlider;

	private Text counterText;
	private Text badWaterText;
	private Text coalText;
	private Text sandText;
	private Text timerText;
	private Text heartText;

    private Text fireText;
    private Text waterText;
    private Text foodText;

    private GameObject buttonGameObject;
    private GameObject debugger;

    private float timer = 0;
	private float lastCountedTime = 0;
	private int count = 0;
	private bool isStarted = false;

	// Use this for initialization
	void Start () {
		counterText = GameObject.Find ("CounterText").GetComponent<Text> ();
		water = GameObject.Find("WaterMain").GetComponent<SpriteRenderer>();
		badWaterText = GameObject.Find("BadWaterText").GetComponent<Text>();
		coalText = GameObject.Find("CoalText").GetComponent<Text>();
		sandText = GameObject.Find("SandText").GetComponent<Text>();

		fireSlider = GameObject.Find("FireSlider").GetComponent<Slider>();
		waterSlider = GameObject.Find("WaterSlider").GetComponent<Slider>();
		foodSlider = GameObject.Find("FoodSlider").GetComponent<Slider>();
		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		heartText = GameObject.Find ("HeartText").GetComponent<Text> ();

        // test
        // SManager.GetInstance ().heart = 5;
        heartText.text = SManager.GetInstance ().heart.ToString () + "/" + ValueTable.GlobalTable.heartMax;

        debugger = GameObject.Find("Debugger");

        // Test Codes
        // SManager.GetInstance ().badwater = 100;
        // SManager.GetInstance ().coal = 100;
        // SManager.GetInstance ().sand = 100;

        fireSlider.value = SManager.GetInstance().getFire();
        waterSlider.value = SManager.GetInstance().getWater();
        foodSlider.value = SManager.GetInstance().getFood();
        
        badWaterText.text = SManager.GetInstance().badwater.ToString();
        coalText.text = SManager.GetInstance().coal.ToString();
        sandText.text = SManager.GetInstance().sand.ToString();

        timerText.text = Mathf.CeilToInt((ValueTable.FireMakeScene.timeLimit / 1000) - timer).ToString();

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
            if (SManager.GetInstance().badwater >= ValueTable.WaterFilteringScene.clickPerBadwater &&
                SManager.GetInstance().coal >= ValueTable.WaterFilteringScene.clickPerCoal &&
                SManager.GetInstance().sand >= ValueTable.WaterFilteringScene.clickPerSand &&
                SManager.GetInstance().fire >= ValueTable.WaterFilteringScene.clickPerFire)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    count++;
                    counterText.text = count.ToString();
                    lastCountedTime = timer;
                    water.sprite = sprites[count % 3];
                }

                if (count != 0 && (count % ValueTable.WaterFilteringScene.countPerWater == 0) && lastCountedTime == timer)
                {
                    SManager.GetInstance().water++;

                    waterSlider.value = SManager.GetInstance().getWater();

                    SManager.GetInstance().badwater -= ValueTable.WaterFilteringScene.clickPerBadwater;
                    SManager.GetInstance().coal -= ValueTable.WaterFilteringScene.clickPerCoal;
                    SManager.GetInstance().sand -= ValueTable.WaterFilteringScene.clickPerSand;
                    SManager.GetInstance().fire -= ValueTable.WaterFilteringScene.clickPerFire;

                    badWaterText.text = SManager.GetInstance().badwater.ToString();
                    coalText.text = SManager.GetInstance().coal.ToString();
                    sandText.text = SManager.GetInstance().sand.ToString();
                    fireSlider.value = SManager.GetInstance().getFire();

                    if (isDebugging)
                    {
                        fireText.text = "Fire: " + fireSlider.value;
                        waterText.text = "Water: " + waterSlider.value;
                    }
                }
            } else { endGame(); }

            timer += Time.deltaTime;
            timerText.text = Mathf.CeilToInt((ValueTable.FireMakeScene.timeLimit / 1000) - timer).ToString();
        }

        if (timer >= (ValueTable.WaterFilteringScene.timeLimit / 1000)) {
            // TODO: End of Scene
            endGame();
		}
	}

	public void startGameButton() {
        Debug.Log(SManager.GetInstance().heart);
        if (SManager.GetInstance().heart <= 0 ||
            (SManager.GetInstance().badwater < ValueTable.WaterFilteringScene.clickPerBadwater &&
                SManager.GetInstance().coal < ValueTable.WaterFilteringScene.clickPerCoal &&
                SManager.GetInstance().sand < ValueTable.WaterFilteringScene.clickPerSand &&
                SManager.GetInstance().fire < ValueTable.WaterFilteringScene.clickPerFire))
        { return; }

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
	}

	public void backButtonPressed() {
        // TODO;
        SceneManager2.GetInstance().ChangeScene(0);
    }
}
