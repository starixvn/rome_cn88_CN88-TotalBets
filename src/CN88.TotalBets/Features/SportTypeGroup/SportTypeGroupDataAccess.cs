namespace CN88.TotalBets.Features.SportTypeGroup
{
    using System.Collections.Generic;
    using System.Linq;
    using Criteria;
    using Fanex.Common;
    using Fanex.Data;
    using Models;

    public interface SportTypeGroupDataAccess
    {
        IEnumerable<SportGroup> GetSportGroups(GetSportGroupsCriteria criteria);
    }

    public class SportTypeGroupDataAccessImpl : SportTypeGroupDataAccess
    {
        public IEnumerable<SportGroup> GetSportGroups(GetSportGroupsCriteria criteria)
        {
            var groups = new List<SportGroup>();

            using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = objectDb.ExecuteReader(criteria))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            var group = new SportGroup(
                                id: Converter.ToInt(reader["SportTypeGroup"]),
                                displaySportIds: Converter.ToString2(reader["DisplaySport"]).Split(',').Select(int.Parse));

                            groups.Add(group);
                        }

                        reader.Close();
                    }
                }
            }

            return groups;
        }
    }
}