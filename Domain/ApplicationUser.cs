using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Web.Domain
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public long CurrenScore { get; set; }
        public long RecordScore { get; set; }
        public IEnumerable<UserBoost> UserBoosts { get; set; } = [];
    }
}
