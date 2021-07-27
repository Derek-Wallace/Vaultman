using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;
using Vaultman.Models;
using Vaultman.Services;

namespace Vaultman.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }
    [HttpGet("{id}")]
    public ActionResult<Profile> FindProfileById(string id)
    {
      try
      {
          var profile = _ps.FindProfileById(id);
          return Ok(profile);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(string id)
    {
      try
      {
          var keeps = _ps.GetKeepsByProfile(id);
          return Ok(keeps);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfile(string id)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          if (account == null)
          {
            var publicVaults = _ps.GetVaultsByProfile(id);
            return Ok(publicVaults);
          }
          var vaults = _ps.GetVaultsByProfile(id, account.Id);
          return Ok(vaults);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}