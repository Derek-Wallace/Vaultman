using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Vaultman.Models;

namespace Vaultman.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    public Profile FindProfileById(string id)
    {
      var sql = @"
      SELECT * FROM accounts WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    public List<Keep> GetKeepsByProfile(string id)
    {
      var sql = @"
      SELECT * FROM keeps WHERE creatorId = @id;
      ";
      return _db.Query<Keep>(sql, new { id }).ToList();
    }
    public List<Vault> GetVaultsByProfile(string id)
    {
      var sql = @"
      SELECT * FROM vaults WHERE creatorId = @id;
      ";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }
  }
}