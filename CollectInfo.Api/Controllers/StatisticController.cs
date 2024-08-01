using CollectInfo.Domain.Abstractions;
using CollectInfo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollectInfo.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class StatisticController : ControllerBase
    {
        private readonly IGitHubRepositoryService _service;
        public StatisticController(IGitHubRepositoryService service)
        {
            _service = service;
        }

        
        /// <summary>
        /// Returns the quantity of letters in each file of a Git repository.
        /// </summary>
        /// <remarks>
        /// This method analyzes all files in the specified Git repository and calculates the total number of letters for each file.
        /// </remarks>
        /// <returns>A list of results, each containing the file name and the corresponding letter count.</returns>
        /// <response code="200">If has sucess, then display a list of request result</response>
        [HttpGet("total-letters-by-files")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<ResultCollectItem>> GetTotalLettersGroupingByFile()
        {
            var result = _service.GetResultQuantityLettersInFiles(isSeparateTotalByFiles:true);
            return Ok(result);
        }

        /// <summary>
        /// Returns the quantity of letters for all files from one git repository.
        /// </summary>
        /// <remarks>
        /// This method analyzes all files in the specified Git repository and calculates the total number of each letters.
        /// </remarks>
        /// <returns>A total letter exist in files.</returns>
        /// <response code="200">If has sucess, then display a list of request result</response>
        [HttpGet("total-letters-all-files")]
        public ActionResult<IList<ResultCollectItem>> GetTotalLettersAllFiles()
        {
            var result = _service.GetResultQuantityLettersInFiles(isSeparateTotalByFiles: false);
            return Ok(result);
        }

    }
}
