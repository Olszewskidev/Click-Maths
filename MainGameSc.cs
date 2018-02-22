using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameSc : MonoBehaviour
{
    public GameObject[] MathTasks;
    public Canvas MainCanvas;
    public Text Score;
    public string CorrectScore;
    private GameObject Task;
    public int MainPoints = 0;
    public GameObject SCmenager;
    public AudioSource SoundSource;
    public AudioClip CorrectSound;
    public AudioClip IncorrectSound;
    public Image[] Chance;
    private int NextTasknumber = 0;
    public int LevelNumber;
    public int RandomNumber;
    private List<int> uniqueNumbers;
    private List<int> finishedList;
    private int ranNum;
    private int n = 0;
    public float ShortValue;


    private void Start() 
    {
        adMobcode.Instance.hide();
        uniqueNumbers = new List<int>();
        finishedList = new List<int>();
        GenerateRandomList();
        GiveTask();
    }
    //function called at the beginning of the level to create a list of unique numbers
    public void GenerateRandomList()
    {
        for (int i = 1; i < MathTasks.Length; i++)
        {
            uniqueNumbers.Add(i);
        }
        for (int i = 1; i < MathTasks.Length; i++)
        {
            ranNum = uniqueNumbers[Random.Range(0, uniqueNumbers.Count)];
            finishedList.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }
    }
    //The function retrieves one unique number from the list for randomness maths tasks
    public void RandomnumberGet() 
    {
        RandomNumber = finishedList[n];
        n++;
    }
    // function created to ask  maths questions
    public void GiveTask() 
    {
        RandomnumberGet();
        Task = MathTasks[RandomNumber];
        Task = Instantiate(Task, MainCanvas.transform); // make that GameObcject task is visible on canvas
        Task.GetComponent<Button>().onClick.AddListener(NextImp); // add to task fucncion OnClick if task is too dificult
        Task.GetComponent<TaskScript>();
        CorrectScore = Task.GetComponent<TaskScript>().CorectAnswer;// get from GameObject script with points for good answer
        ShortValue = Task.GetComponent<TaskScript>().HowShort;
        Task.GetComponent<Animator>().Play("start", -1, 0.0f);
    }
    //The function that is triggered by the player when the question is too difficult for him and player can do this only 3 times in level
    public void NextImp()
    {
        NextTasknumber++;
        if ((NextTasknumber >= 1) && (NextTasknumber <= 3))
        {
            SCmenager.GetComponent<LevelTime>().Next(); // function from other script to subtracting a certain time for passing the equation
            CorrectAnswerAnimation(); //play this anim but not give points for answer...
            Cleaning();
            Chance[NextTasknumber-1].GetComponent<Animator>().Play("next1", -1, 0.0f);//disappears image that indicates the number of chances
            StartCoroutine("Waiting");
        }
    }
    // function created to verify the correctness of the result
    public void Checkingaswer() 
    {
        if (Score.text == CorrectScore) // score is string (not int) becouse game have 2 buttons, first is to X. (first digit) and second is to .X (second digit) and finally string + string gives correct answer than int+int 
        {
            SoundSource.PlayOneShot(CorrectSound);
            CorrectAnswerAnimation();
            PointsAdd();
            Cleaning();
            StartCoroutine("Waiting");
        }
        else
        {
            SoundSource.PlayOneShot(IncorrectSound);
            IncorrectAnswer();
        }
    }
    // function created to play the whole animation
    IEnumerator Waiting() 
    {
        yield return new WaitForSeconds(1f);
        DestroyObject(Task);
        GiveTask();
    }
    //A function created to clear the given result by the player after checking the his answer
    private void Cleaning() 
    {
        SCmenager.GetComponent<MainButtonScript>().ButLeftClick = 0;
        SCmenager.GetComponent<MainButtonScript>().ButRightClick = 0;
    }
    public void CorrectAnswerAnimation()
    {
        Task.GetComponent<Animator>().Play("dobrze", -1, 0.0f);
    }
    public void IncorrectAnswer()
    {
        Task.GetComponent<Animator>().Play("zle", -1, 0.0f);
        Cleaning();
    }
    // function responsible for the player's result in the game
    public void PointsAdd()
    {
        MainPoints = MainPoints + Task.GetComponent<TaskScript>().Pkt;
    }
    
    
}
