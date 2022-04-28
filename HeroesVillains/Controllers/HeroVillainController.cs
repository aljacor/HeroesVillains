using HeroesVillains.Helpers;
using HeroesVillains.Interfaces;
using HeroesVillains.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesVillains.Controllers
{
    public class HeroVillainController : Controller
    {
        private IApi<HeroesResponse> _apiCall;
        private IApi<Character> _apiCallChar;
        private IConfiguration _config;
        public HeroVillainController(IApi<HeroesResponse> api, IConfiguration configuration, IApi<Character> apiChar)
        {
            _apiCall = api;
            _apiCallChar = apiChar;
            _config = configuration;
        }
        //Get the characters searching by the name
        public async Task<IActionResult> Index(string search)
        {
            try
            {
                List<Character> characters = new List<Character>();
                if (string.IsNullOrEmpty(search))
                {
                    return View(characters);
                }

                UsingApiHelper helper = new UsingApiHelper(_config);

                HeroesResponse response = await _apiCall.CallApi(helper.CompleteSearchUri(search));
                if (!string.IsNullOrEmpty(response.Response) && response.Response.Equals("error"))
                {
                    ViewBag.Error = response.Error;
                }
                else
                {
                    ViewBag.Search = search;
                    ViewBag.FirstResults = response.Results;

                    characters = response.Results;
                }


                return View(characters);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", ex.Message);
            }
        }

        //Get the information of a specific character
        public async Task<IActionResult> CharacterDetail(string search)
        {
            try
            {
                Character character = new Character();
                if (string.IsNullOrEmpty(search))
                {
                    return RedirectToAction("NotFound", "Error");
                }
                
                UsingApiHelper helper = new UsingApiHelper(_config);

                Character response = await _apiCallChar.CallApi(helper.SearchById(search));
                if (!string.IsNullOrEmpty(response.Response) && response.Response.Equals("error"))
                {
                    return RedirectToAction("NotFound", "Error");
                }
                else
                {
                    character = response;
                }

                return View(character);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error", ex.Message);
            }
        }
    }
}
