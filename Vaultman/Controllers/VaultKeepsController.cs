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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep data)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          data.CreatorId = account.Id;
          var vaultkeep = _vks.Create(data);
          return Ok(vaultkeep);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Delete(int id)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          _vks.Delete(account.Id, id);
          return Ok("VaultKeep Deleted");
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}