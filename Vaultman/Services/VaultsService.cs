using System;
using System.Collections.Generic;
using Vaultman.Models;
using Vaultman.Repositories;

namespace Vaultman.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }

    internal Vault Create(Vault data)
    {
      var newVault = _vr.Create(data);
      return newVault;
    }

    internal Vault GetById(int id, string uid)
    {
      var vault = _vr.GetById(id);
      if (vault == null)
      {
        throw new Exception("No vault matches that id");
      }
      if (vault.IsPrivate == true && vault.CreatorId != uid)
      {
        throw new Exception("This vault is private");
      }
      return vault;
    }
    internal Vault GetById(int id)
    {
      var vault = _vr.GetById(id);
      if (vault == null)
      {
        throw new Exception("No vault matches that id");
      }
      if (vault.IsPrivate == true)
      {
        throw new Exception("This vault is private");
      }
      return vault;
    }

    internal List<VaultKeepView> GetVaultKeeps(int vid, string uid)
    {
      var vault = _vr.GetById(vid);
      var vaultkeeps = _vr.GetVaultKeeps(vid);
      if (vaultkeeps == null)
      {
        throw new Exception("Invalid Vault Id");
      }
      if (vault.IsPrivate == true && vault.CreatorId != uid)
      {
        throw new Exception("This vault is private");
      }
      return vaultkeeps;

    }
    internal List<VaultKeepView> GetVaultKeeps(int vid)
    {
      var vault = _vr.GetById(vid);
      var vaultkeeps = _vr.GetVaultKeeps(vid);
      if (vaultkeeps == null)
      {
        throw new Exception("Invalid Vault Id");
      }
      if (vault.IsPrivate == true)
      {
        throw new Exception("This vault is private");
      }
      return vaultkeeps;

    }

    internal Vault EditVault(Vault data)
    {
      var vault = _vr.GetById(data.Id);
      if (vault == null)
      {
        throw new Exception("No vault matches that id");
      }
      if (vault.CreatorId != data.CreatorId)
      {
        throw new Exception("You are not authorized to edit this vault");
      }
      vault.Name = data.Name ?? vault.Name;
      vault.Img = data.Img ?? vault.Img;
      vault.Description = data.Description ?? vault.Description;
      vault.IsPrivate = vault.IsPrivate;
      var editedVault = _vr.EditVault(vault);
      return editedVault;
    }

    internal void Delete(string userId, int id)
    {
      var vault = _vr.GetById(id);
      if (vault == null)
      {
        throw new Exception("There is no vault with that id");
      }
      if (vault.CreatorId != userId)
      {
        throw new Exception("You are not authorized to delete this vault");
      }
      _vr.Delete(id);
    }
  }
}