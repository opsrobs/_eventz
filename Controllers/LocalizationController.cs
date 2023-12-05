using eventz.DTOs;
using eventz.Models;
using eventz.Repositories;
using eventz.Repositories.Interfaces;
using eventz.Utils;
using Microsoft.AspNetCore.Mvc;

namespace eventz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizationController: Controller
    {
        private readonly ILocalizationRepository _localization;

        public LocalizationController(ILocalizationRepository localization)
        {
            _localization = localization;
        }

        [HttpPost]
        [Route("Localization")]
        public async Task<ActionResult<LocalizationDto>> Create([FromBody] LocalizationDto localization)
        {
            if (localization == null)
            {
                return BadRequest(new { error = Messages.EmptyLocalization });
            }

            if (!IsValidLocalization(localization))
            {
                return BadRequest(new { error = Messages.InvalidLocalization });
            }

            try
            {
                var newLocalization = CreateLocalization(localization);
                await _localization.Create(newLocalization);
                return Ok(localization);
            }
            catch (Exception ex)
            {
                // Considere logar o erro ex para fins de depuração.
                return StatusCode(500, new { error = "Internal Server Error." });
            }
        }

        private bool IsValidLocalization(LocalizationDto localization)
        {
            return localization.Latitude != 0 && localization.Longitude != 0;
        }

        private Localization CreateLocalization(LocalizationDto localizationDto)
        {
            return new Localization( localizationDto.Latitude, localizationDto.Longitude);
        }


    }
}
