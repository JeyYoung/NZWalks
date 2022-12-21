﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    
    [ApiController]
    [Route("WalkDifficulty")]
    public class WalkDifficulty : Controller
    {

        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalkDifficulty(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDifficultyAsync()
        {
            var difficulties = await _walkDifficultyRepository.GetAllAsync();

            var difficultiesDTO = _mapper.Map<List<Models.DTO.WalkDifficulty>>(difficulties);

            return Ok(difficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName ("GetDifficultyAsync")]

        public async Task<IActionResult> GetDifficultyAsync(Guid id)
        {
           var walkDifficulty = await _walkDifficultyRepository.GetAsync(id);

            if(walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = _mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]

        public async Task<IActionResult> AddWalkDifficultyAsync(AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
           //Request to Domain Model
            
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };

            //Pass details to Repository
            walkDifficulty = await _walkDifficultyRepository.AddAsync(walkDifficulty);

            //Convert the data back to DTO
            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            return CreatedAtAction(nameof(GetDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateRegionRequest)
        {
            //Convet DTO to Domain Model
            var walkDifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updateRegionRequest.Code
            };

            //Update Walk Difficult using Repository
            walkDifficulty = await _walkDifficultyRepository.UpdateAsync(id, walkDifficulty);

            //If null then Not Found
            if(walkDifficulty != null)
            {
                return NotFound();
            }

            //convert domain back to DTO

            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code
            };

            //Return Ok response

            return Ok(walkDifficultyDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {

            //Get Walk Difficulty
            var walkDifficulty = await _walkDifficultyRepository.DeleteAsync(id);

            //If no walk difficulty is found
            if (walkDifficulty == null)
            {
                return NotFound();
            }

            //convert response to DTO

            var walkDifficultyDTO = new Models.DTO.WalkDifficulty()
            {
                Code = walkDifficulty.Code
            };

            //return ok response
            return Ok(walkDifficultyDTO);







        }
    }

}