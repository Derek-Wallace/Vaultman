using System.ComponentModel.DataAnnotations;

namespace Vaultman.Models
{
  public class VaultKeep
  {
    public int Id { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public string CreatorId { get; set; }
  }

  public class VaultKeepView : Keep
  {
    public int VaultKeepId { get; set; } 
  }
}