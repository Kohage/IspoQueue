﻿using IspoQueue.App.Features.Queue;
using IspoQueue.App.Features.Queue.DTO;
using IspoQueue.App.Repositories;
using IspoQueue.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IspoQueue.App.Features.Cabinets;

[ApiController]
[Route("api/[controller]")]
public class CabinetController : ControllerBase
{
    private readonly IGenericRepo<Cabinet> _cabinetRepo;

    public CabinetController(IGenericRepo<Cabinet> cabinetRepo)
    {
        _cabinetRepo = cabinetRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CabinetDTO>>> GetCabinets()
    {
        try
        {
            var cabinets = await _cabinetRepo.Get();
            List<CabinetDTO> cabinetDtos = new();
            foreach (var cabinet in cabinets)
            {
                var cabDto = new CabinetDTO()
                {
                    Id = cabinet.Id,
                    Name = cabinet.Name,
                    Windows = cabinet.Windows
                        .Select(w => new WindowDTO() { Id = w.Id, Name = w.Name, IsActive = w.IsActive })
                        .OrderBy(w => w.Name)
                        .ToList(),
                };
                cabinetDtos.Add(cabDto);
            }

            return Ok(cabinetDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new Response() { Status = "Ошибка", Message = $"Сервер выдал ошибку: {ex.Message}" });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCabinet([FromBody] CabinetDTO cabinetDto)
    {
        try
        {
            var cabs = await _cabinetRepo.Get();
            var uniqueCab = cabs.Where(c => c.Name == cabinetDto.Name);
        
            if (uniqueCab.Count() > 0)
            {
                return BadRequest(new Response { Status = "Ошибка", Message = "Кабинет уже существует", });
            }

            var cabinet = new Cabinet
            {
                Id = new Guid(),
                Name = cabinetDto.Name,
            };

            await _cabinetRepo.Create(cabinet);
            return Ok(new Response { Status = "Успех", Message = "Кабинет добавлен", });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response { Status = "Ошибка", Message = $"Кабинет не создан. Error: {ex}" });
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCabinet(Guid id, [FromBody] CabinetDTO cabinetDto)
    {
        try
        {
            if (cabinetDto == null || id == Guid.Empty)
            {
                return BadRequest(new Response { Status = "Ошибка", Message = "Данные не валидны", });
            }

            var cabinet = await _cabinetRepo.FindById(id);
            if (cabinet == null)
            {
                return NotFound(new Response { Status = "Ошибка", Message = "Кабинет не найден", });
            }
            
            var allCabinets = await _cabinetRepo.Get();
            foreach (var cabinets in allCabinets)
            {
                if (cabinets.Name == cabinetDto.Name)
                {
                    return BadRequest(new Response { Status = "Ошибка", Message = "Кабинет уже существует", });
                }
            }

            cabinet.Name = cabinetDto.Name;

            await _cabinetRepo.Update(cabinet);
            return Ok(new Response { Status = "Успех", Message = "Кабинет изменен", });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response { Status = "Ошибка", Message = $"Кабинет не изменен. Error: {ex}" });
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCabinet(Guid id)
    {
        try
        {
            var cabinet = await _cabinetRepo.FindById(id);
            if (cabinet == null)
            {
                return NotFound(new Response { Status = "Ошибка", Message = "Кабинет не найден", });
            }

            _cabinetRepo.Remove(cabinet);
            return Ok(new Response { Status = "Успех", Message = "Кабинет удален", });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response { Status = "Ошибка", Message = $"Кабинет не удален. Error: {ex}" });
        }
    }
}