using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeFire : MonoBehaviour {

	private Slider fireSlider;
	private Slider waterSlider;
	private Slider foodSlider;

	private Text treeText;
	private Text timerText;
	private Text heartText;

    private Text fireText;
    private Text waterText;
    private Text foodText;

    private GameObject buttonGameObject;
    private GameObject debugger;

    public Animator anim;
    public float beforePosX;
    public bool isMakeFire;
    public bool isDebugging;
//    public float timeLimit;
    float movingTime;
    float calculatingPosition;

    private int count;
	private float timer;
	private bool isStarted = false;

    // Use this for initialization
    void Start () {
        anim.enabled = true;
        anim.speed = 0;
        isMakeFire = false;
        beforePosX = 0.0f;

		fireSlider = GameObject.Find("FireSlider").GetComponent<Slider>();
		waterSlider = GameObject.Find("WaterSlider").GetComponent<Slider>();
		foodSlider = GameObject.Find("FoodSlider").GetComponent<Slider>();

		treeText = GameObject.Find ("TreeText").GetComponent<Text> ();
		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
		heartText = GameObject.Find ("HeartText").GetComponent<Text> ();

        debugger = GameObject.Find("Debugger");

		// SManager.GetInstance ().tree = 1000;
		timerText.text = (ValueTable.FireMakeScene.timeLimit / 1000).ToString ();
		timer = 0;

        // test
        // SManager.GetInstance ().heart = 5;
        
        fireSlider.value = SManager.GetInstance().getFire();
        waterSlider.value = SManager.GetInstance().getWater();
        foodSlider.value = SManager.GetInstance().getFood();

        heartText.text = SManager.GetInstance ().heart.ToString () + "/" + ValueTable.GlobalTable.heartMax;

        SoundManager.GetInstance().PlayFireBgm();
        SoundManager.GetInstance().Pause();

        if (isDebugging)
        {
            fireText = GameObject.Find("Fire Value").GetComponent<Text>();
            waterText = GameObject.Find("Water Value").GetComponent<Text>();
            foodText = GameObject.Find("Food Value").GetComponent<Text>();

            fireText.text = "Fire: " + fireSlider.value;
            waterText.text = "Water: " + waterSlider.value;
            foodText.text = "Food: " + foodSlider.value;
        } else { debugger.SetActive(false); }
    }
	
	// Update is called once per frame
	void Update () {
		fireSlider.value = SManager.GetInstance ().getFire();
		waterSlider.value = SManager.GetInstance ().getWater();
		foodSlider.value = SManager.GetInstance ().getFood();
		treeText.text = SManager.GetInstance ().tree.ToString();

        if (Input.GetKeyDown("1"))
        {
            SManager.GetInstance().heart--;
            heartText.text = SManager.GetInstance().heart.ToString() + "/" + ValueTable.GlobalTable.heartMax;
        }

        if (!isStarted) {
			return;
		} else {
            if (SManager.GetInstance().tree >= ValueTable.FireMakeScene.clickPerTree)
            {
                if (Input.GetMouseButton(0))
                {
                    calculatingPosition = Mathf.Clamp01(Mathf.Abs(beforePosX - Input.mousePosition.x) / 20.0f);
                    if (calculatingPosition > 0.0f && calculatingPosition < 0.4f)
                        SoundManager.GetInstance().SetPitch(0.4f);
                    else
                        SoundManager.GetInstance().SetPitch(calculatingPosition);
                    SoundManager.GetInstance().UnPause();
                    anim.speed = calculatingPosition;

                    if (Mathf.Abs(beforePosX - Input.mousePosition.x) >= 20.0f)
                        isMakeFire = true;
                    else
                        isMakeFire = false;
                } else {
                    anim.speed = 0;
                    SoundManager.GetInstance().Pause();
                    isMakeFire = false;
                }
                beforePosX = Input.mousePosition.x;

                if (isMakeFire)
                {
                    movingTime += Time.deltaTime;

                    if (movingTime >= ValueTable.FireMakeScene.countPerFire)
                    {
                        SManager.GetInstance().fire++;
                        SManager.GetInstance().tree -= ValueTable.FireMakeScene.clickPerTree;
                        movingTime = 0.0f;

                        if (isDebugging)
                            fireText.text = "Fire: " + fireSlider.value;
                    }
                }
            } else {
                endGame();
            }

            // Debug.Log(ValueTable.FireMakeScene.timeLimit + "," + timer);
            if (timer >= (ValueTable.FireMakeScene.timeLimit / 1000))
            {
                endGame();
                // TODO: End of Scene
            }

            timer += Time.deltaTime;
            timerText.text = Mathf.CeilToInt((ValueTable.FireMakeScene.timeLimit / 1000) - timer).ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

	public void startGameButton() {
		if (SManager.GetInstance ().heart <= 0 || SManager.GetInstance().tree < ValueTable.FireMakeScene.clickPerTree) {
			return;
		}

		SManager.GetInstance().heart--;
		heartText.text = SManager.GetInstance ().heart.ToString () + "/" + ValueTable.GlobalTable.heartMax;

        if (SManager.GetInstance().heart == 0)
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
        anim.speed = 0;
        SoundManager.GetInstance().Pause();
        isMakeFire = false;
    }

	public void backButtonPressed() {
        // TODO;
        SceneManager2.GetInstance().ChangeScene(0);
	}
}
