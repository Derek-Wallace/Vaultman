using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaultman.Models;
using Vaultman.Services;

namespace Vaultman.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep data)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          data.CreatorId = account.Id;
          var keep = _ks.Create(data);
          keep.Creator = account;
          return Ok(keep);
      }
      catch (System.Exception e)
      {
           return BadRequest(e.Message);
      }
    }
    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
          var keeps = _ks.GetAll();
          return Ok(keeps);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
          var keep = _ks.GetById(id);
          return Ok(keep);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> EditKeep(int id, [FromBody] Keep data)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          data.Id = id;
          data.CreatorId = account.Id;
          var keep = _ks.EditKeep(data);
          return Ok(keep);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Delete(int id)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          _ks.Delete(account.Id, id);
          return Ok("Keep Deleted");
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}