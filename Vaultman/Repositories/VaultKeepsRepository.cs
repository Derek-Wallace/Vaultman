using System.Data;
using Dapper;
using Vaultman.Models;

namespace Vaultman.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public VaultKeep Create(VaultKeep data)
    {
      var sql = @"
      INSERT INTO vaultkeeps(vaultId, keepId, creatorId)
      VALUES(@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Delete(int id)
    {
      var sql = "DELETE FROM vaultkeeps WHERE id =@id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    public VaultKeep GetById(int id)
    {
      var sql = @"
      SELECT * FROM vaultkeeps WHERE id = @id;
      ";
       return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}