using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokerApi.Models;
using PokerApi.Services.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PokerApi.Controllers.V1_0
{
    /// <summary>
    /// Rank Assignment Controller
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class RankAssignmentController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRankAssignmentService _rankAssignmentService;

        public RankAssignmentController(
            ILogger logger,
            IRankAssignmentService rankAssignmentService)
        {
            _logger = logger.ForContext("SourceContext", nameof(RankAssignmentController));
            _rankAssignmentService = rankAssignmentService;
        }

        /// <summary>
        /// Assigns ranks to poker hands submitted in the request.
        /// </summary>
        /// <param name="pokerHandDtos">List of poker hands submitted in the request. Each card must be in valid format of "XY" where "X" must be the card number 2 thru 10, J, Q, K or A and "Y" must be the card suit of D, H, C or S. </param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<Ranking>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Ranking>>> AssignRanks(
            [FromBody]IEnumerable<PokerHandDto> pokerHandDtos)
        {
            var result = await _rankAssignmentService.AssignRanks(pokerHandDtos);
            return Ok(result);
        }
    }
}
