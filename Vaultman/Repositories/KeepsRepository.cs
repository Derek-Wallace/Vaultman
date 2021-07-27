using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Vaultman.Models;

namespace Vaultman.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Keep Create(Keep data)
    {
      var sql = @"
      INSERT INTO keeps(name, description, img, creatorId)
      VALUES(@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public List<Keep> GetAll()
    {
      var sql = @"
        SELECT 
            k.*,
            a.* 
          FROM keeps k
          JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, splitOn: "id").ToList();
    }

    public Keep GetById(int id)
    {
      var sql = @"
        SELECT 
            k.*,
            a.* 
          FROM keeps k
          JOIN accounts a ON k.creatorId = a.id
          WHERE k.id = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { id }).FirstOrDefault();
    }
    public Keep EditKeep(Keep data)
    {
      var sql = @"
        UPDATE keeps
          SET
          name = @Name,
          description = @Description
        WHERE id = @Id;
      ";
      _db.Execute(sql, data);
      return data;
    }

    public void UpdateCount(Keep update)
    {
      var sql = @"
      UPDATE keeps
      SET
      views = @Views,
      keeps = @Keeps,
      shares = @Shares
      WHERE id = @Id;
      ";
      _db.Execute(sql, update);
    }
    public void Delete(int id)
    {
      var sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}