﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NestLibraryNetCore.Api.Models;
using NestLibraryNetCore.Api.Repository;
using System.Collections.Immutable;

namespace NestLibraryNetCore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {

        private readonly ECommerceRepository _repository;

        public ECommerceController(ECommerceRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]

        public async Task<IActionResult> TermQuery(string customerFirstName)
        {
            var result = await _repository.TermQuery(customerFirstName);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> TermsQuery(List<string> customerFirstNameList)
        {
            var result = await _repository.TermsQuery(customerFirstNameList);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> PrefixQuery(string customerFullName)
        {
            var result = await _repository.PrefixQuery(customerFullName);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Range(double fromPrice, double toPrice)
        {
            var result = await _repository.Range(fromPrice, toPrice);   
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> MatchAll()
        {
            var result = await _repository.MatchAll();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> PaginationMatchAll(int page,int pageSize)
        {
            var result = await _repository.PaginationMatchAll(page,pageSize);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> WildCardQuery(string customerFullName)
        {
            var result = await _repository.WildCardQuery(customerFullName);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FuzzyQuery(string customerFullName)
        {
            var result = await _repository.FuzzyQuery(customerFullName);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> MatchQueryFullText(string categoryName)
        {
            var result = await _repository.MatchQueryFullText(categoryName);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> MatchBoolPrefixQueryFullText(string customerFullName)
        {
            var result = await _repository.MatchBoolPrefixQueryFullText(customerFullName);
            return Ok(result);
        }
    }
}
