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
        SwitchButtons�lickability(false);
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
    /// �������� �������� ���� � ����������� ���������
    /// </summary>
    /// <param name="pin"></param>
    /// <returns>���������� �������� ����</returns>
    private int StabilizePin(int pin)
    {
        if (pin < 0) return 0;
        if (pin > 10) return 10;

        return pin;
    }

    /// <summary>
    /// �������� �������� ���� ����� � ����������� ���������
    /// </summary>
    private void StabilizeAllPins()
    {
        pin_1 = StabilizePin(pin_1);
        pin_2 = StabilizePin(pin_2);
        pin_3 = StabilizePin(pin_3);
    }

    /// <summary>
    /// ��������� ����
    /// </summary>
    public void EndGame()
    {
        gameStarted = false;
        ExitButton.SetActive(false);
        SwitchButtons�lickability(false);
        if (userWin)
        {
            gameStatusText.text = "�� ��������!";
            startButtonText.text = "������";
        }
        else
        {
            gameStatusText.text = "�� ���������";
            startButtonText.text = "������!";
        }

        startTime = 60;
        timeText.text = "";

        menuScreen.SetActive(true);
    }

    /// <summary>
    /// ������ �������� ����� � ������������ �� ���������� �����
    /// </summary>
    public void UseDrill()
    {
        pin_1++;
        pin_2--;

        StabilizeAllPins();

        PrintAllPins();
    }

    /// <summary>
    /// ������ �������� ����� � ������������ �� ���������� �������
    /// </summary>
    public void UseHammer()
    {
        pin_2 += 2;
        pin_3--;

        StabilizeAllPins();

        PrintAllPins();
    }

    /// <summary>
    /// ������ �������� ����� � ������������ �� ���������� �������
    /// </summary>
    public void UseMasterKey()
    {
        pin_1--;
        pin_3++;

        StabilizeAllPins();

        PrintAllPins();
    }

    /// <summary>
    /// �������� ����
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

        SwitchButtons�lickability(true);

        gameStarted = true;
    }

    /// <summary>
    /// �������/��������� ������ ������������
    /// </summary>
    /// <param name="isEnable"></param>
    private void SwitchButtons�lickability(bool isEnable)
    {
        drillButton.enabled = isEnable;
        masterKeyButton.enabled = isEnable;
        hammerButton.enabled = isEnable;
    }

    /// <summary>
    /// ������� �������� ���� ����� �� �����
    /// </summary>
    private void PrintAllPins()
    {
        firstPinText.text = pin_1.ToString();
        secondPinText.text = pin_2.ToString();
        thirdPinText.text = pin_3.ToString();
    }
}
