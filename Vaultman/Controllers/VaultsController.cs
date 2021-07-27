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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;

    public VaultsController(VaultsService vs)
    {
      _vs = vs;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault data)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          data.CreatorId = account.Id;
          var vault = _vs.Create(data);
          vault.Creator = account;
          return Ok(vault);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetById(int id)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          if (account == null)
          {
            var publicVault = _vs.GetById(id);
            return Ok(publicVault);
          }
          var vault = _vs.GetById(id, account.Id);
          return Ok(vault);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{vid}/keeps")]
    public async Task<ActionResult<List<VaultKeepView>>> GetVaultKeeps(int vid)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          if (account == null)
          {
          var publicVaultkeeps = _vs.GetVaultKeeps(vid);
          return Ok(publicVaultkeeps);
          }
          var vaultKeeps = _vs.GetVaultKeeps(vid, account.Id);
          return Ok(vaultKeeps);
          
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault(int id, [FromBody] Vault data)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          data.Id = id;
          data.CreatorId = account.Id;
          var vault = _vs.EditVault(data);
          return Ok(vault);
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
          var account = await HttpContext.GetUserInfoAsync<Account>();
          _vs.Delete(account.Id, id);
          return Ok("Vault Deleted");
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}