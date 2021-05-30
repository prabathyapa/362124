using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using workwear_test.Repositories;

namespace workwear_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigRepository _configRepository;
        private readonly ILogger<ConfigController> _logger;

        public ConfigController(IConfigRepository configRepository, ILogger<ConfigController> logger)
        {
            _configRepository = configRepository;
            _logger = logger;
        }

        [HttpPost("{key}")]
        public IActionResult Post(string key, [FromForm] string value)
        {
            try
            {
                _configRepository.Add(key, value);
                return CreatedAtAction(nameof(Get), new { key }, value) ;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");
                throw;
            }
        }

        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            try
            {
                return Ok(_configRepository.Get(key));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");
                throw;
            }
        }

        [HttpPut("{key}")]
        public IActionResult Update(string key, [FromForm] string value)
        {
            try
            {
                _configRepository.Update(key, value);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");
                throw;
            }
        }
    }
}
