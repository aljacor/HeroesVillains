using Microsoft.Extensions.Configuration;
using HeroesVillains.Controllers;
using HeroesVillains.Interfaces;
using HeroesVillains.Models;
using HeroesVillains.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace HeroesVillains.TestXUnit.Validation
{
    public class HeroVillainControllerTest
    {
        private readonly HeroVillainController _controller;

        public HeroVillainControllerTest() {

            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:HeroesApi" , "https://superheroapi.com/api/4430349780363511" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            _controller = new HeroVillainController(new HeroVillainService<HeroesResponse>(), configuration, new HeroVillainService<Character>());
        }


        [Theory]
        [InlineData("")]
        public void Index_ActionExecutes_ReturnsViewForIndex(string search)
        {
            var result = _controller.Index(search);
            
            Assert.IsType<ViewResult>(result.Result);
        }

        [Theory]
        [InlineData("dead")]
        [InlineData("test character")]
        public void Index_ActionExecutes_ReturnsExactNumberOfCharacters(string search)
        {
            var result = _controller.Index(search);

            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var characters = Assert.IsType<List<Character>>(viewResult.Model);

            if (search.Equals("dead"))
                Assert.Equal(4, characters.Count); 
            else
                Assert.True(0 == characters.Count);
        }

        [Theory]
        [InlineData("")]
        public void CharacterDetail_ActionExecutes_ReturnsViewForIndex(string search)
        {
            var result = _controller.CharacterDetail(search);

            Assert.IsType<RedirectToActionResult>(result.Result);
        }

        [Theory]
        [InlineData("213")]
        public void CharacterDetail_ActionExecutes_ReturnsExactCharacter(string search)
        {
            var result = _controller.CharacterDetail(search);

            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var character = Assert.IsType<Character>(viewResult.Model);

            Assert.Equal("Deadpool", character.Name);
        }

        [Theory]
        [InlineData("213000000")]
        public void CharacterDetail_ActionExecutes_NotExistCharacter(string search)
        {
            var result = _controller.CharacterDetail(search);

            Assert.IsType<RedirectToActionResult>(result.Result);
        }
    }
}
