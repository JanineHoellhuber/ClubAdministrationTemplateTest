using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
  public class MemberSectionRepository : IMemberSectionRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public MemberSectionRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<MemberSection> memberSections) 
      => await _dbContext.AddRangeAsync(memberSections);


        public async Task<IEnumerable<MemberDto>> GetMembersBySection(int id)
        {
                 return await _dbContext.MemberSections
                .Where(m => m.Section.Id == id)
                .Select(ms => new MemberDto
                {
                    FirstName = ms.Member.FirstName,
                    LastName = ms.Member.LastName,
                    Id = ms.MemberId,
                    CountSections = _dbContext.MemberSections.Count(msec => msec.MemberId == msec.Id)

                })
                .ToListAsync();

          //  members.ForEach(m => m.CountSections)
        }

        public void Update(Member selectedMember)
        {
            _dbContext.Members
                .Update(selectedMember);
        }


        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _dbContext.Members
                .SingleAsync(m => m.Id == id);
        }

    }

}