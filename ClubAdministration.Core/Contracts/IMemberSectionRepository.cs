using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
  public interface IMemberSectionRepository
  {
    Task AddRangeAsync(IEnumerable<MemberSection> memberSections);
        Task<IEnumerable<MemberDto>> GetMembersBySection(int id);
        void Update(Member selectedMember);
        Task<Member> GetMemberByIdAsync(int id);
    }
}
