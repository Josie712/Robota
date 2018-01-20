﻿using System;
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
    public GameObject answersObject;
    public GameObject answerPrefab;
    private List<GameObject> answers = new List<GameObject>();

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

    public void AnswerButtonClicked(GameObject self)
    {
        if (self.GetComponent<Toggle>().isOn) // switched on by user
        {
            UncheckOtherAnswers(self);
        }
        else if (OtherAnswersAreUnchecked(self)) // last chosen answer switched off by user
        {
            self.GetComponent<Toggle>().isOn = true; // switch it back on
        }
    }

    private void UncheckOtherAnswers(GameObject self)
    {
        answers.ForEach((answer) =>
        {
            if (answer != self)
            {
                answer.GetComponent<Toggle>().isOn = false;
            }
        });
    }

    private bool OtherAnswersAreUnchecked(GameObject self)
    {
        return answers.All((answer) => answer == self || answer.GetComponent<Toggle>().isOn == false);
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

        CreateAnswerObjects(currentQuestion);
    }

    private void CreateAnswerObjects(Question currentQuestion)
    {
        answers.ForEach<GameObject>(Destroy);
        answers.Clear();

        currentQuestion.answers.ForEach((answer) =>
        {
            GameObject answerObject = Instantiate(answerPrefab);
            answerObject.transform.SetParent(answerPrefab.transform.parent, false);
            answers.Add(answerObject);
            answerObject.SetActive(true);
            Position(answerObject, answers.Count);
        });
        answers[0].GetComponent<Toggle>().isOn = true;
    }

    /**
     * Correctly position the GameObject 'answerObject' which represents a possible Answer to a question.
     * The number of the answer is provided to help with the placement of the object., To modify the position,
     * modify the attributes of the Transform component using answerObject.GetComponent<Transform>()
     **/
    private void Position(GameObject answerObject, int answerNumber)
    {
        //answerObject.transform.position = new Vector3(0, 0, 0);
        //throw new NotImplementedException();
    }

    private void EvaluateAnswer()
    {
        AddStatsForAnswer();
        UpdateStatBars();
    }

    private void AddStatsForAnswer()
    {
        answers.ApplyWhileTrue((index, answer) =>
        {
            if (answer.GetComponent<Toggle>().isOn)
            {
                currentStats += GetStatsForAnswer(index);
                return false;
            }
            return true;
        });
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
