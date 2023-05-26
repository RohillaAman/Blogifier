using AutoMapper;
using Blogifier.Blogs;
using Blogifier.Posts;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

[Route("category")]
public class CategoryController : Controller
{
  private readonly MainMamager _mainMamager;
  private readonly PostProvider _postProvider;

  public CategoryController(
    MainMamager mainMamager,
    PostProvider postProvider )
  {
    _mainMamager = mainMamager;
    _postProvider = postProvider;
  }

  [HttpGet("{category}")]
  public async Task<IActionResult> Category(string category, int page = 1)
  {
    var main = await _mainMamager.GetAsync();
    var posts = await _postProvider.CategoryAsync(category, page, main.ItemsPerPage);
    var model = new CategoryModel(category, posts, page, main);
    return View($"~/Views/Themes/{main.Theme}/category.cshtml", model);
  }
}
