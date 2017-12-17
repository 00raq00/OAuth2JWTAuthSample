using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
  [Route("api/[controller]")]
  public class WebpagerankController : Controller
  {
    [HttpGet, Authorize]
    public IEnumerable<WebPageRank> Get()
    {
      var currentUser = HttpContext.User;
      WebPageRank[] resultBookList = GetFakeData(currentUser.Claims.Where(x=>x.Type.Equals(ClaimTypes.Webpage)).FirstOrDefault().Value);

      return resultBookList;
    }

    private static WebPageRank[] GetFakeData(string web) => new WebPageRank[] {
        new WebPageRank { Url = "http://test.test",Opinion="Fake page, viruses", Rank=RankEnum.Fake },
        new WebPageRank { Url = "http://test.test",Opinion="Great page", Rank=RankEnum.Great },
        new WebPageRank { Url = "http://test2.test2",Opinion="Great page", Rank=RankEnum.Great },
        new WebPageRank { Url = "http://test2.test2",Opinion="Ok", Rank=RankEnum.Standard},
        new WebPageRank { Url = "http://test2.test2",Opinion="Great page", Rank=RankEnum.Great}
      }.Where(x => x.Url.Equals(web, StringComparison.InvariantCultureIgnoreCase)).ToArray();
  }
}