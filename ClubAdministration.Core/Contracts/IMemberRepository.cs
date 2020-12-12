using ClubAdministration.Core.Entities;

namespace ClubAdministration.Core.Contracts
{
    public interface IMemberRepository
    {
        bool CheckDuplicate(Member member);
    }
}
