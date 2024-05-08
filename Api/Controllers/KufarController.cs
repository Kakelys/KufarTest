using Api.Services.Interfaces;
using Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/kufar")]
    public class KufarController(IKufarService kufarService) : ControllerBase
    {
        [HttpGet]
        [Route("stats")]
        public async Task<IActionResult> GetStats()
        {
            return Ok(await kufarService.GetAllStats());
        }

        [HttpGet]
        [Route("floors")]
        public async Task<IActionResult> GetFloorStats()
        {
            return Ok(await kufarService.GetFloorStats());
        }

        [HttpGet]
        [Route("rooms")]
        public async Task<IActionResult> GetRoomsStats()
        {
            return Ok(await kufarService.GetRoomStats());
        }

        [HttpGet]
        [Route("metro")]
        public async Task<IActionResult> GetMetroStats()
        {
            return Ok(await kufarService.GetMetroStats());
        }

        [HttpGet]
        [Route("rent")]
        public async Task<IActionResult> GetRent(
            [FromQuery] string area, 
            [FromQuery] DateTime? startRent,
            [FromQuery] DateTime? endRent)
        {
            return Ok(await kufarService.GetRent(area, startRent, endRent));
        }

        [HttpPost]
        [Route("coords")]
        public async Task<IActionResult> GetByCoords([FromBody] List<Point> points)
        {
            return Ok(await kufarService.GetByCoords(points));
        }
    }
}