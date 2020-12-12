using ClubAdministration.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace ClubAdministration.ImportConsole
{
  public class ImportController
  {
    const string FileName = "members.csv";

        public static async Task<MemberSection[]> ReadFromCsvAsync()
        {
            string[][] matrix = await MyFile.ReadStringMatrixFromCsvAsync(FileName, false);
            var member = matrix.Distinct()
                .Select(mem => new Member
                {
                    LastName = mem[0],
                    FirstName = mem[1]
                }).GroupBy(line => line.LastName + line.FirstName)
                .Select(s => s.First()).ToArray();

            var section = matrix
                .GroupBy(line => line[2])
                .Distinct()
                .Select(sec => new Section
                {
                    Name = sec.Key
                }).ToArray();

            var memberSection = matrix
                .Select(ms => new MemberSection
                {
                    Member = member.Single(m => m.LastName == ms[0] && m.FirstName == ms[1]),
                    Section = section.Single(s => s.Name == ms[2])
                }).Distinct().ToArray();

            return memberSection;
    }

  }
}
