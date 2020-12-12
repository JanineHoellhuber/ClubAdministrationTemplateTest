using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
    public interface IMemberRepository
    {
        bool CheckDuplicate(Member member);
    }
}
