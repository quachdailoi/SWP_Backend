using CsvHelper.Configuration;
using SECapstoneEvaluation.Domain.Entities;

namespace API.Helpers.CsvMapper
{
    public sealed class CampusMap : ClassMap<Campus>
    {
        public CampusMap()
        {
            Map(x => x.Code).Name("Code");
            Map(x => x.Name).Name("Name");
        }
    }
}
