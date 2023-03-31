using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;
using Website.Models.DbDto;

namespace Website.Controllers
{
    public class RecommendationsController : BaseApiController
    {
        protected IUsersRepository usersRepository;
        protected IRecommendationsRepository recommendationsRepository;

        public RecommendationsController(IUsersRepository urepo, IRecommendationsRepository recrepo)
        {
            this.usersRepository = urepo;
            this.recommendationsRepository = recrepo;
        }
        public IActionResult Index([FromCookie][Required] Guid? UserId)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 422;
                return ShowModelErrors(ModelState);
            }

            var userInfo = new UserInfo { UniqueId = UserId.Value };
            usersRepository.Add(userInfo);

            return Json(recommendationsRepository.GetForUser(userInfo.Id.Value));
        }
    }
}
