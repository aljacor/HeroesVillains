using HeroesVillains.Helpers;
using HeroesVillains.Interfaces;
using HeroesVillains.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesVillains.Controllers
{
    public class HeroVillainController : Controller
    {
        private IApi<HeroesResponse> apiCall;
        private IApi<Character> apiCallChar;
        private IApi<Character> apiCallCharacter;
        private IConfiguration config;
        public HeroVillainController(IApi<HeroesResponse> api, IConfiguration configuration, IApi<Character> apiChar)
        {
            apiCall = api;
            apiCallChar = apiChar;
            config = configuration;
        }

        public async Task<IActionResult> Index(string search)
        {
            try
            {
                List<Character> characters = new List<Character>();
                if (string.IsNullOrEmpty(search))
                {
                    return View(characters);
                }

                UsingApiHelper helper = new UsingApiHelper(config);

                HeroesResponse response = await apiCall.CallApi(helper.CompleteSearchUri(search));
                if (!string.IsNullOrEmpty(response.Response) && response.Response.Equals("error"))
                {
                    ViewBag.Error = response.Error;
                }
                else
                {
                    ViewBag.Search = search;
                    ViewBag.TotalCount = (int)(response.Results.Count() / 20);
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

        public IActionResult ShowSearchResult(List<Character> source, int pageIndex)
        {
            ViewBag.CurrentPage = pageIndex;
            var page = PaginationHelper<Character>.CreatePage(source, pageIndex, 20);
            return View(page);
        }

        public async Task<IActionResult> CharacterDetail(string search)
        {
            try
            {
                Character character = new Character();
                if (string.IsNullOrEmpty(search))
                {
                    return RedirectToAction("NotFound", "Error");
                }
                
                UsingApiHelper helper = new UsingApiHelper(config);

                Character response = await apiCallChar.CallApi(helper.SearchById(search));
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
