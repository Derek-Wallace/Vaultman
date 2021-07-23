using System;
using System.Collections.Generic;
using Vaultman.Models;
using Vaultman.Repositories;

namespace Vaultman.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;

    public KeepsService(KeepsRepository kr)
    {
      _kr = kr;
    }

    internal Keep Create(Keep data)
    {
      var newKeep = _kr.Create(data);
      newKeep.Keeps = 0;
      newKeep.Shares = 0;
      newKeep.Views = 0;
      return newKeep;
    }

    internal List<Keep> GetAll()
    {
      return _kr.GetAll();
    }

    internal Keep GetById(int id)
    {
      var keep = _kr.GetById(id);
      if (keep == null)
      {
          throw new Exception("No keep matches that id");
      }
      return keep;
    }

    internal Keep EditKeep(Keep data)
    {
      var keep = _kr.GetById(data.Id);
      if (keep == null)
      {
          throw new Exception("No keep matches that id");
      }
      if (keep.CreatorId != data.CreatorId)
      {
          throw new Exception("You are not authorized to edit this keep");
      }
      keep.Name = data.Name ?? keep.Name;
      keep.Img = data.Img ?? keep.Img;
      keep.Description = data.Description ?? keep.Description;
      keep.Keeps = keep.Keeps;
      keep.Shares = keep.Shares;
      keep.Views = keep.Views;
      var editedKeep = _kr.EditKeep(keep);
      return editedKeep;
    }

    internal void Delete(string userId, int id)
    {
      var keep = _kr.GetById(id);
      if (keep == null)
      {
          throw new Exception("There is no Keep with that id");
      }
      if (keep.CreatorId != userId)
      {
          throw new Exception("You are not authorized to delete this keep");
      }
      _kr.Delete(id);
    }
  }
}