using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Vaultman.Models;

namespace Vaultman.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Vault Create(Vault data)
    {
      var sql = @"
      INSERT INTO vaults(name, description, img, creatorId, isPrivate)
      VALUES(@Name, @Description, @Img, @CreatorId, @IsPrivate);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public Vault GetById(int id)
    {
      var sql = @"
      SELECT
        v.*,
        a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) => 
      {
        v.Creator = p;
        return v;
      }, new { id }).FirstOrDefault();
    }

    public List<VaultKeepView> GetVaultKeeps(int vid)
    {
      var sql = @"
      SELECT
        k.*,
        v.id as VaultKeepId,
        a.*
        FROM vaultkeeps v
        JOIN keeps k ON k.id = v.keepId
        JOIN accounts a ON a.id = k.creatorId
        WHERE vaultId = @vid;
      ";
      return _db.Query<VaultKeepView, Profile, VaultKeepView>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new { vid }, splitOn: "id" ).ToList();
    }

    public Vault EditVault(Vault data)
    {
      var sql = @"
      UPDATE vaults
        SET
        name = @Name,
        description = @Description
      WHERE id = @Id;
      ";
      _db.Execute(sql, data);
      return data;
    }

    public void Delete(int id)
    {
      var sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}