using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
  public class MemberRepository : IMemberRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public MemberRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }


        public bool CheckDuplicate(Member member)
        {
            var mem = _dbContext.Members
                .Where(m => m.FirstName == member.FirstName && m.LastName == member.LastName);
            if (mem == null)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }


       

    }
}