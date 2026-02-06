// Created by Nicholas Maddox
using Xunit;
using MvcEldenRingBossLore.Controllers;
using MvcEldenRingBossLore.Models;

namespace EldenRingBossLore.Tests;

/// <summary>
/// Unit tests for the QuizController class only
/// </summary>

public class EldenRingBossLoreQuizTests
{
    private readonly QuizController _controller;

    public EldenRingBossLoreQuizTests()
    {
        _controller = new QuizController();
    }

    #region QuizController Tests

    [Fact]
    public void QuizController_Constructor_ShouldInitializeQuestionsAndAnswers()
    {
        // Arrange
        var expectedQuestions = new Dictionary<int, string>
        {
            { 1, "What is the name of the final boss in Elden Ring?" },
            { 2, "Which boss is known for wielding a giant hammer?" },
            { 3, "Who is the boss that guards the entrance to the Stormhills?" },
            { 4, "Which boss is fought in the Cathedral of Manus Celest?" },
            { 5, "What is the name of the dragon boss found in the Mountaintops of the Giants?" }
        };

        var expectedAnswers = new Dictionary<int, string>
        {
            { 1, "Radagon of the Golden Order" },
            { 2, "Godrick the Grafted" },
            { 3, "Margit, the Fell Omen" },
            { 4, "Rennala, Queen of the Full Moon" },
            { 5, "Ancient Dragon Lansseax" }
        };

        // Act
        var actualQuestions = _controller.Questions;
        var actualAnswers = _controller.Answers;

        // Assert
        Assert.True(expectedQuestions.SequenceEqual(actualQuestions));
        Assert.True(expectedAnswers.SequenceEqual(actualAnswers));
    }

    [Fact]
    public void QuizController_CheckAnswers_ShouldReturnCorrectResults()
    {
        // Arrange
        var model = new QuizQuestions
        {
            Questions = _controller.Questions,
            Answers = _controller.Answers,
            UserAnswers = new Dictionary<int, string>
            {
                { 1, "Radagon of the Golden Order" },
                { 2, "Godrick the Grafted" },
                { 3, "Margit, the Fell Omen" },
                { 4, "Rennala, Queen of the Full Moon" },
                { 5, "Ancient Dragon Lansseax" }
            },
            Results = new Dictionary<int, bool>()
        };

        // Act
        var result = _controller.CheckAnswers(model);

        // Assert
        Assert.True(result.Results[1]);
        Assert.True(result.Results[2]);
        Assert.True(result.Results[3]);
        Assert.True(result.Results[4]);
        Assert.True(result.Results[5]);
    }

    [Fact]
    // Test with incorrect answers
    public void QuizController_CheckAnswers_ShouldReturnIncorrectResults()
    {
        // Arrange
        var model = new QuizQuestions
        {
            Questions = _controller.Questions,
            Answers = _controller.Answers,
            UserAnswers = new Dictionary<int, string>
            {
                { 1, "Mickey Mouse" },
                { 2, "Jennifer Lopez" },
                { 3, "Arkansas" },
                { 4, "Pokemon" },
                { 5, "Real HouseWives of Salt Lake City specifically Lisa Barlow" }
            },
            Results = new Dictionary<int, bool>()
        };

        // Act
        var result = _controller.CheckAnswers(model);

        // Assert
        Assert.False(result.Results[1]);
        Assert.False(result.Results[2]);
        Assert.False(result.Results[3]);
        Assert.False(result.Results[4]);
        Assert.False(result.Results[5]);
    }
    #endregion
}