using System;
using System.Collections.Generic;
using System.Linq;
using Vaultman.Models;
using Vaultman.Repositories;

namespace Vaultman.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
    }

    internal Profile FindProfileById(string id)
    {
      var profile = _pr.FindProfileById(id);
      if (profile == null)
      {
        throw new Exception("No profile matches that id");
      }
      return profile;
    }

    internal List<Keep> GetKeepsByProfile(string id)
    {
      var keeps = _pr.GetKeepsByProfile(id);
      return keeps;
    }
    internal List<Vault> GetVaultsByProfile(string id, string uid)
    {
      var vaults = _pr.GetVaultsByProfile(id);
      if (vaults[0].CreatorId != uid)
      {
      List<Vault> publicVaults = vaults.Where(v => v.IsPrivate != true).ToList();
      return publicVaults;
      }
      return vaults;
    }
    internal List<Vault> GetVaultsByProfile(string id)
    {
      var vaults = _pr.GetVaultsByProfile(id);
      List<Vault> publicVaults = vaults.Where(v => v.IsPrivate != true).ToList();
      return publicVaults;
    }
  }
}