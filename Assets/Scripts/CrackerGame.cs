using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CrackerGame : MonoBehaviour
{
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject gameScreen;

    [SerializeField] Button drillButton;
    [SerializeField] Button masterKeyButton;
    [SerializeField] Button hammerButton;
    [SerializeField] GameObject ExitButton;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI firstPinText;
    [SerializeField] TextMeshProUGUI secondPinText;
    [SerializeField] TextMeshProUGUI thirdPinText;

    [SerializeField] TextMeshProUGUI gameStatusText;
    [SerializeField] TextMeshProUGUI startButtonText;

    private float startTime = 60;
    private int pin_1, pin_2, pin_3;
    private bool gameStarted = false;
    private float previousTime = 0;
    private bool userWin;

    void Start()
    {
        gameScreen.SetActive(true);
        menuScreen.SetActive(true);
        SwitchButtonsСlickability(false);
    }

    void Update()
    {
       if (gameStarted)
       {
            double timeLeft = Mathf.Round(startTime - Time.time + previousTime);
            timeText.text = (timeLeft).ToString() + " c";

            if (pin_1 == 5 && pin_2 == 5 && pin_3 == 5)
            {
                userWin = true;
                EndGame();
            }
            else if (timeLeft <= 0)
            {
                userWin = false;
                EndGame();
            }
       }
    }

    /// <summary>
    /// Приводит значение пина к корректному состоянию
    /// </summary>
    /// <param name="pin"></param>
    /// <returns>Корректное значение пина</returns>
    private int StabilizePin(int pin)
    {
        if (pin < 0) return 0;
        if (pin > 10) return 10;

        return pin;
    }

    /// <summary>
    /// Приводит значение всех пинов к корректному состоянию
    /// </summary>
    private void StabilizeAllPins()
    {
        pin_1 = StabilizePin(pin_1);
        pin_2 = StabilizePin(pin_2);
        pin_3 = StabilizePin(pin_3);
    }

    /// <summary>
    /// Завершает игру
    /// </summary>
    public void EndGame()
    {
        gameStarted = false;
        ExitButton.SetActive(false);
        SwitchButtonsСlickability(false);
        if (userWin)
        {
            gameStatusText.text = "Вы победили!";
            startButtonText.text = "Заново";
        }
        else
        {
            gameStatusText.text = "Вы проиграли";
            startButtonText.text = "Реванш!";
        }

        startTime = 60;
        timeText.text = "";

        menuScreen.SetActive(true);
    }

    /// <summary>
    /// Меняет значение пинов в соответствии со значениями дрели
    /// </summary>
    public void UseDrill()
    {
        pin_1++;
        pin_2--;

        StabilizeAllPins();

        PrintAllPins();
    }

    /// <summary>
    /// Меняет значение пинов в соответствии со значениями молотка
    /// </summary>
    public void UseHammer()
    {
        pin_2 += 2;
        pin_3--;

        StabilizeAllPins();

        PrintAllPins();
    }

    /// <summary>
    /// Меняет значение пинов в соответствии со значениями отмычки
    /// </summary>
    public void UseMasterKey()
    {
        pin_1--;
        pin_3++;

        StabilizeAllPins();

        PrintAllPins();
    }

    /// <summary>
    /// Начинает игру
    /// </summary>
    public void StartGame()
    {
        previousTime = Time.time;

        System.Random randomizer = new System.Random();
        
        pin_1 = randomizer.Next(11);
        pin_2 = randomizer.Next(5);
        pin_3 = randomizer.Next(11);

        PrintAllPins();

        menuScreen.SetActive(false);
        userWin = false;

        SwitchButtonsСlickability(true);

        gameStarted = true;
    }

    /// <summary>
    /// Вкючает/выключает кнопки инструментов
    /// </summary>
    /// <param name="isEnable"></param>
    private void SwitchButtonsСlickability(bool isEnable)
    {
        drillButton.enabled = isEnable;
        masterKeyButton.enabled = isEnable;
        hammerButton.enabled = isEnable;
    }

    /// <summary>
    /// Выводит значение всех пинов на экран
    /// </summary>
    private void PrintAllPins()
    {
        firstPinText.text = pin_1.ToString();
        secondPinText.text = pin_2.ToString();
        thirdPinText.text = pin_3.ToString();
    }
}
