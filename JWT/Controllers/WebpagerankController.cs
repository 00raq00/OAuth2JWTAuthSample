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
      WebPageRank[] resultBookList = GetOwnPageRank(currentUser);

      return resultBookList;
    }

    [HttpGet("{id}"), Authorize]
    public IEnumerable<WebPageRank> Get(string id)
    {
      var currentUser = HttpContext.User;
      WebPageRank[] resultBookList = GetPageRanks(id);

      return resultBookList;
    }

    private static WebPageRank[] GetOwnPageRank(ClaimsPrincipal claimsPrincipal) => GetFakeData().Where(x => x.Url.Equals(claimsPrincipal.Claims.Where(y => y.Type.Equals(ClaimTypes.Webpage)).FirstOrDefault().Value, StringComparison.InvariantCultureIgnoreCase)).ToArray();

    private static WebPageRank[] GetPageRanks(string web) => GetFakeData().Where(x => x.Url.Contains(web)).ToArray();

    private static WebPageRank[] GetFakeData() => new WebPageRank[] {
        new WebPageRank { Url = "http://test.test",Opinion="Fake page, viruses", Rank=RankEnum.Fake },
        new WebPageRank { Url = "http://test.test",Opinion="Great page", Rank=RankEnum.Great },
        new WebPageRank { Url = "http://test2.test2",Opinion="Great page", Rank=RankEnum.Great },
        new WebPageRank { Url = "http://test2.test2",Opinion="Ok", Rank=RankEnum.Standard},
        new WebPageRank { Url = "http://test2.test2",Opinion="Great page", Rank=RankEnum.Great}
      };
  }
}