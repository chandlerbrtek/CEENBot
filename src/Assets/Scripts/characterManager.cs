using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterManager : MonoBehaviour
{

    public Text speedText;
    public Text agilityText;
    public Text sizeText;
    public Text moneyText;
    public Text highscoreText;
    public bool isMainMenu = false;
    static int speedLevel = 1;
    static int speedPrice = 200;

    static int agilityLevel = 1;
    static int agilityPrice = 200;

    static int sizeLevel = 1;
    static int sizePrice = 200;

    static int money = 0;
    static float highscore = 0;

    private void Start()
    {
        Time.timeScale = 1f;
        updateSpeed();
        updateSize();
        updateAgility();
        updateMoney();
        updateHighscore();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

    }


    public void setSpeed()
    {
        if(money - speedPrice > 0)
        {
            money = money - speedPrice;
            moneyText.text = "Money: " + money.ToString();
            speedLevel++;
            speedPrice = speedPrice + 200;
            speedText.text = "Speed Level: " + speedLevel.ToString() + "\nPrice: " + speedPrice.ToString();
        }
        
        return;
    }

    public void setAgility()
    {
        if (money - agilityPrice > 0)
        {
            money = money - agilityPrice;
            moneyText.text = "Money: " + money.ToString();
            agilityLevel++;
            agilityPrice = agilityPrice + 200;
            agilityText.text = "Agility Level: " + agilityLevel.ToString() + "\nPrice: " + agilityPrice.ToString();
        }
    }

    public void setSize()
    {
        if (money - sizePrice > 0)
        {
            money = money - sizePrice;
            moneyText.text = "Money: " + money.ToString();
            sizeLevel++;
            sizePrice = sizePrice + 200;
            sizeText.text = "Size Level: " + sizeLevel.ToString() + "\nPrice: " + sizePrice.ToString();
        }
    }

    public void setMoney(int score)
    {
        money = money + score;

    }

    public void setHighscore(float temp)
    {
        highscore = temp;
    }


    public int getSpeed()
    {
        return speedLevel;
    }

    public int getAgility()
    {
        return agilityLevel;
    }
    public float getsize()
    {
        return sizeLevel;
    }

    public void updateSpeed()
    {
        if (isMainMenu)
        {
            speedText.text = "Speed Level: " + speedLevel.ToString() + "\nPrice: " + speedPrice.ToString();
        }
    }

    public void updateAgility()
    {
        if (isMainMenu)
        {
            agilityText.text = "Agility Level: " + agilityLevel.ToString() + "\nPrice: " + agilityPrice.ToString();
        }
    }

    public void updateSize()
    {
        if (isMainMenu)
        {
            sizeText.text = "Size Level: " + sizeLevel.ToString() + "\nPrice: " + sizePrice.ToString();
        }
    }

    public void updateMoney()
    {
        if (isMainMenu)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }

    public void updateHighscore()
    {
        if (isMainMenu)
        {
            highscoreText.text = "Distance: " + highscore.ToString();
        }
    }
}
