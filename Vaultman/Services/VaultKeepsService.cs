using System;
using Vaultman.Models;
using Vaultman.Repositories;

namespace Vaultman.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;
    private readonly VaultsRepository _vr;
    private readonly KeepsRepository _kr;

    public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr, KeepsRepository kr)
    {
      _vkr = vkr;
      _vr = vr;
      _kr = kr;
    }

    internal VaultKeep Create(VaultKeep data)
    {
      var newVaultKeep = _vkr.Create(data);
      var vault = _vr.GetById(newVaultKeep.VaultId);
      if (vault.CreatorId != newVaultKeep.CreatorId)
      {
        throw new Exception("You are not authorized to create this");
      }
      var keep = _kr.GetById(newVaultKeep.KeepId);
      keep.Keeps++;
      _kr.UpdateCount(keep);
      return newVaultKeep;
    }

    internal void Delete(string userId, int id)
    {
      var vaultKeep = _vkr.GetById(id);
      if (vaultKeep.CreatorId != userId)
      {
        throw new Exception("You are not authorized to delete this");
      }
      var keep = _kr.GetById(vaultKeep.KeepId);
      keep.Keeps--;
      _kr.UpdateCount(keep);
      _vkr.Delete(id);
    }
  }
}