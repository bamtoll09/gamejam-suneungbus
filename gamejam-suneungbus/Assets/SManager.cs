using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SManager : MonoBehaviour
{
    private static SManager instance;

	// Datas
	private int heart;

    private int badwater;
    private int beef;
    private int coal;
    private int fish;
    private int potato;
    private int sand;
    private int tree;

    private int fire;
    private int fireMax;
    private int water;
    private int waterMax;
    private int food;
    private int foodMax;

    private int survivingDays;

    public int Heart
    {
        get
        {
            return heart;
        }

        set
        {
            heart = value;
        }
    }

    public int Badwater
    {
        get
        {
            return badwater;
        }

        set
        {
            badwater = value;
        }
    }

    public int Beef
    {
        get
        {
            return beef;
        }

        set
        {
            beef = value;
        }
    }

    public int Coal
    {
        get
        {
            return coal;
        }

        set
        {
            coal = value;
        }
    }

    public int Fish
    {
        get
        {
            return fish;
        }

        set
        {
            fish = value;
        }
    }

    public int Potato
    {
        get
        {
            return potato;
        }

        set
        {
            potato = value;
        }
    }

    public int Sand
    {
        get
        {
            return sand;
        }

        set
        {
            sand = value;
        }
    }

    public int Tree
    {
        get
        {
            return tree;
        }

        set
        {
            tree = value;
        }
    }

    public int Fire
    {
        get
        {
            return fire;
        }

        set
        {
            fire = value;
        }
    }

    public int FireMax
    {
        get
        {
            return fireMax;
        }

        set
        {
            fireMax = value;
        }
    }

    public int Water
    {
        get
        {
            return water;
        }

        set
        {
            water = value;
        }
    }

    public int WaterMax
    {
        get
        {
            return waterMax;
        }

        set
        {
            waterMax = value;
        }
    }

    public int Food
    {
        get
        {
            return food;
        }

        set
        {
            food = value;
        }
    }

    public int FoodMax
    {
        get
        {
            return foodMax;
        }

        set
        {
            foodMax = value;
        }
    }

    public int SurvivingDays
    {
        get
        {
            return survivingDays;
        }

        set
        {
            survivingDays = value;
        }
    }

    public static SManager GetInstance()
    {
        if (!instance)
        {
            instance = (SManager) FindObjectOfType(typeof(SManager));
            if (!instance)
                Debug.LogError("There needs to be one active SManager script on a SManager in your scene.");
        }

        return instance;
    }

    void Awake()
    {
        DontDestroyOnLoad(instance);

        if (!ValueTable.GlobalTable.onlyOnce)
        {
            FireMax = ValueTable.GlobalTable.fireMax;
            WaterMax = ValueTable.GlobalTable.waterMax;
            FoodMax = ValueTable.GlobalTable.foodMax;
            Heart = ValueTable.GlobalTable.heartMax;
            Fire = FireMax / 2;
            Water = WaterMax / 2;
            Food = FoodMax / 2;
            SurvivingDays = 1;

            Debug.Log("RESET");

            ValueTable.GlobalTable.onlyOnce = true;
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float getFire() {
		return (float)Fire / FireMax;
	}

	public float getWater() {
		return (float)Water / WaterMax;
	}

	public float getFood() {
		return (float)Food / FoodMax;
	}

	public int[] getArrayedParams() {
		return new int[] {Tree, Badwater, Sand, Coal, Potato, Fish, Beef};
	}
}
