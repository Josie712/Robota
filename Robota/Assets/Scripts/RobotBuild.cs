using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RobotBuild : MonoBehaviour {

    public LevelSettings levelSettings;

    public GameObject headObject;
    public GameObject torsoObject;
    public GameObject armsObject;
    public GameObject legsObject;

    public GameObject nextButton;
    public GameObject decisivenessBar;
    public GameObject peopleSkillsBar;
    public GameObject compertmentalizationBar;
    public GameObject partButtonsObject;
    public GameObject questionsPanelObject;
    public GameObject questionHeaderObject;
    public GameObject questionTextObject;
    public GameObject answer1Object;
    public GameObject answer2Object;
    public GameObject answer3Object;

    public Sprite blankSprite;

    private int currentQuestionIndex;
    private Question currentQuestion;
    private Stats currentStats;

    public void UserClickPart() {
        RecalculateStats();

        if (RobotFullyBuilt())
        {
            nextButton.GetComponent<Button>().interactable = true;
        }
    }

    public void NextButtonClicked()
    {
        if (partButtonsObject.activeSelf)
        {
            partButtonsObject.SetActive(false);
            questionsPanelObject.SetActive(true);
        }
        else
        {
            EvaluateAnswer();
            DisplayNextQuestion();
        }
    }

    public void XButtonClicked()
    {
        ResetQuestions();
    }

    public void Answer1ButtonClicked()
    {
        if (answer1Object.GetComponent<Toggle>().isOn) // switched on by user
        {
            answer2Object.GetComponent<Toggle>().isOn = false;
            answer3Object.GetComponent<Toggle>().isOn = false;
        }
        else if (!answer2Object.GetComponent<Toggle>().isOn && !answer3Object.GetComponent<Toggle>().isOn) // last chosen answer switched off by user
        {
            answer1Object.GetComponent<Toggle>().isOn = true; // switch it back on
        }
    }

    public void Answer2ButtonClicked()
    {
        if (answer2Object.GetComponent<Toggle>().isOn)
        {
            answer1Object.GetComponent<Toggle>().isOn = false;
            answer3Object.GetComponent<Toggle>().isOn = false;
        }
        else if (!answer1Object.GetComponent<Toggle>().isOn && !answer3Object.GetComponent<Toggle>().isOn)
        {
            answer2Object.GetComponent<Toggle>().isOn = true;
        }
    }

    public void Answer3ButtonClicked()
    {
        if (answer3Object.GetComponent<Toggle>().isOn)
        {
            answer1Object.GetComponent<Toggle>().isOn = false;
            answer2Object.GetComponent<Toggle>().isOn = false;
        }
        else if (!answer1Object.GetComponent<Toggle>().isOn && !answer2Object.GetComponent<Toggle>().isOn)
        {
            answer3Object.GetComponent<Toggle>().isOn = true;
        }
    }

    private void ResetQuestions()
    {
        currentQuestionIndex = -1;
        DisplayNextQuestion();
        RecalculateStats();
    }

    private bool PartObjectSelected(GameObject part) {
        return part.GetComponent<Image>().sprite != blankSprite;
    }

    private bool RobotFullyBuilt() {
        GameObject[] partObjects = { headObject, torsoObject, armsObject, legsObject };
        return partObjects.All(PartObjectSelected);
    }

    private void RecalculateStats() {
        GameObject[] partObjects = { headObject, torsoObject, armsObject, legsObject };

        currentStats = levelSettings.baseRobotStats;
        (from part in partObjects
         select part.GetComponent<Image>().sprite)
            .ForEach(AddStatsOfSprite);

        UpdateStatBars();
    }

    private void AddStatsOfSprite(Sprite sprite) {
        Stats statsToAdd = (from part in levelSettings.robotParts where part.sprite == sprite select part.stats).FirstOrDefault();
        if (statsToAdd == default(Stats)) {
            statsToAdd = new Stats(0, 0, 0);
        }
        currentStats += statsToAdd;
    }

    private void UpdateStatBars() {
        Stats stats = currentStats.AtLeastZero();
        decisivenessBar.GetComponent<Slider>().value = stats.decisiveness;
        peopleSkillsBar.GetComponent<Slider>().value = stats.peopleSkills;
        compertmentalizationBar.GetComponent<Slider>().value = stats.compartmentalization;
    }

    private void DisplayNextQuestion() {
        currentQuestionIndex++;
        if (currentQuestionIndex >= levelSettings.questions.Length)
        {
            Debug.Log("Ran out of questions!");
            return;
        }
        currentQuestion = levelSettings.questions[currentQuestionIndex];

        questionHeaderObject.GetComponent<Text>().text = "Question " + (currentQuestionIndex + 1);
        questionTextObject.GetComponent<Text>().text = currentQuestion.text;

        answer3Object.SetActive(false);
        answer1Object.GetComponentInChildren<Text>().text = currentQuestion.answers[0].text;
        answer2Object.GetComponentInChildren<Text>().text = currentQuestion.answers[1].text;
        if (currentQuestion.answers.Length >= 3)
        {
            answer3Object.SetActive(true);
            answer3Object.GetComponentInChildren<Text>().text = currentQuestion.answers[2].text;
        }

        answer1Object.GetComponent<Toggle>().isOn = true;
        answer2Object.GetComponent<Toggle>().isOn = false;
        answer3Object.GetComponent<Toggle>().isOn = false;

    }

    private void EvaluateAnswer()
    {
        if (answer1Object.GetComponent<Toggle>().isOn)
        {
            currentStats += GetStatsForAnswer(0);
        }
        else if (answer2Object.GetComponent<Toggle>().isOn)
        {
            currentStats += GetStatsForAnswer(1);
        }
        else
        {
            currentStats += GetStatsForAnswer(2);
        }

        UpdateStatBars();
    }

    private Stats GetStatsForAnswer(int index)
    {
        return currentQuestion.answers[index].stats;
    }

    private void Start()
    {
        ResetQuestions();
    }

}
