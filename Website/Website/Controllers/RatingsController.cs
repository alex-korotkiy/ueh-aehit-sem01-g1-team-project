using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;
using Website.Models.DbDto;
using Website.Models.Requests;

namespace Website.Controllers
{
    public class RatingsController : BaseApiController
    {
        protected IUsersRepository usersRepository;
        protected IRatingsRepository ratingsRepository;

        public RatingsController(ILogger<RatingsController> logger, IUsersRepository usersRepo, IRatingsRepository ratingsRepo) : base(logger)
        {
            usersRepository = usersRepo;
            ratingsRepository = ratingsRepo;
        }

        [HttpPut]
        public IActionResult Set([FromCookie][Required] Guid? UserId, [FromBody] SetRatingRequest request)
        {

            var userInfo = new UserInfo { UniqueId = UserId.Value };
            usersRepository.Add(userInfo);

            ratingsRepository.SetRating(userInfo.Id.Value, request.ItemId, request.Rating);
            return NoContent();
        }
    }
}
