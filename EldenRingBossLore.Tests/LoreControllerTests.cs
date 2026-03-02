// Created by Nicholas Maddox
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MvcEldenRingBossLore.Controllers;
using MvcEldenRingBossLore.Models;

namespace EldenRingBossLore.Tests;

/// <summary>
/// Unit tests for LoreController class using FakeLoreRepo
/// </summary>
public class LoreControllerTests
{
    private readonly LoreController _controller;
    private readonly FakeLoreRepo _fakeRepo;

    public LoreControllerTests()
    {
        _fakeRepo = new FakeLoreRepo();
        _controller = new LoreController(_fakeRepo);
    }

    #region Index Tests

    [Fact]
    public void IndexWithNoFilters()
    {
        // Arrange

        // Act
        var result = _controller.Index(null, null, null);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<LoreTypeViewModel>(viewResult.Model);
        Assert.Equal(3, model.Lores.Count());
    }

    [Fact]
    public void IndexWithSearchString()
    {
        // Arrange
        string searchString = "Malenia";

        // Act
        var result = _controller.Index(null, searchString, null);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<LoreTypeViewModel>(viewResult.Model);
        Assert.Single(model.Lores);
        Assert.Contains("Malenia", model.Lores.First().Title);
    }

    [Fact]
    public void IndexWithGodTypeFilter()
    {
        // Arrange
        string godType = "Demigod";

        // Act
        var result = _controller.Index(godType, null, null);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<LoreTypeViewModel>(viewResult.Model);
        Assert.Single(model.Lores);
        Assert.Equal("Demigod", model.Lores.First().GodType);
    }

    #endregion

    #region Details Tests

    [Fact]
    public void DetailsWithValidIdReturnsLore()
    {
        // Arrange
        int validId = 1;

        // Act
        var result = _controller.Details(validId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Lore>(viewResult.Model);
        Assert.Equal(validId, model.Id);
        Assert.Equal("Radagon's Golden Order", model.Title);
    }

    [Fact]
    public void DetailsWithNullIdReturnsNotFound()
    {
        // Arrange & Act
        var result = _controller.Details(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DetailsWithInvalidIdReturnsNotFound()
    {
        // Arrange
        int invalidId = 999;

        // Act
        var result = _controller.Details(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    #endregion

    #region Create Tests

    [Fact]
    public void CreateGetReturnsView()
    {
        // Act
        var result = _controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void CreatePostRedirectsToIndex()
    {
        // Arrange
        var newLore = new Lore
        {
            Id = 0,
            Title = "Ranni's Dark Moon",
            DiscoveryDate = new DateTime(2022, 3, 5),
            GodType = "Lunar God",
            Notes = "Ranni's questline"
        };

        // Act
        var result = _controller.Create(newLore);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        
        // Verify it was added to the repository
        var allLores = _fakeRepo.GetAllLores();
        Assert.Equal(4, allLores.Count());
    }

    #endregion

    #region Edit Tests

    [Fact]
    public void EditGetReturnsLore()
    {
        // Arrange
        int validId = 1;

        // Act
        var result = _controller.Edit(validId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Lore>(viewResult.Model);
        Assert.Equal(validId, model.Id);
    }

    #endregion

    #region Delete Tests

    [Fact]
    public void DeleteGetReturnsLore()
    {
        // Arrange
        int validId = 1;

        // Act
        var result = _controller.Delete(validId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Lore>(viewResult.Model);
        Assert.Equal(validId, model.Id);
    }

    #endregion
}
