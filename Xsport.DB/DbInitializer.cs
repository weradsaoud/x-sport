using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Db;
using Xsport.DB.Entities;

namespace Xsport.DB
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            #region Languages
            if (!context.Languages.Any())
            {
                context.Languages.AddRange(
                    new Language { LanguageId = 1, Code = "en", Name = "English" },
                    new Language { LanguageId = 2, Code = "ar", Name = "العربية" }
                );
                context.SaveChanges();
            }
            #endregion
            #region Sports
            if (!context.Sports.Any())
            {
                context.Sports.AddRange(
                    new Sport
                    {
                        SportId = 1,
                        Name = "Football",
                        NumOfTeams = 2,
                        NumOfPlayers = 11,
                        NumOfRounds = 2,
                        RoundPeriod = 45,
                        NumOfBreaks = 2,
                        BreakPeriod = 15,
                        HasExtraRounds = true,
                        NumOfExtraRounds = 2,
                        NumOfReferees = 3,
                        SportTranslations = new List<SportTranslation>() {
                            new SportTranslation { LanguageId = 1, SportId = 1, Name = "Football"},
                            new SportTranslation { LanguageId = 2, SportId = 1, Name = "كرة القدم" }
                        },
                    },
                    new Sport
                    {
                        SportId = 2,
                        Name = "Tennis",
                        NumOfTeams = 2,
                        NumOfPlayers = 1,
                        NumOfRounds = 10,
                        RoundPeriod = 15,
                        NumOfBreaks = 9,
                        BreakPeriod = 3,
                        HasExtraRounds = true,
                        NumOfExtraRounds = 2,
                        NumOfReferees = 3,
                        SportTranslations = [
                            new() { LanguageId = 1, SportId = 2, Name = "Tennis"},
                            new() { LanguageId = 2, SportId = 2, Name = "التنس" }
                        ]
                    },
                    new Sport
                    {
                        SportId = 3,
                        Name = "Paddel",
                        NumOfTeams = 2,
                        NumOfPlayers = 2,
                        NumOfRounds = 10,
                        RoundPeriod = 15,
                        NumOfBreaks = 9,
                        BreakPeriod = 3,
                        HasExtraRounds = true,
                        NumOfExtraRounds = 2,
                        NumOfReferees = 3,
                        SportTranslations = [
                            new() { LanguageId = 1, SportId = 2, Name = "Paddel"},
                            new() { LanguageId = 2, SportId = 2, Name = "البادل" }
                        ]
                    });
                context.SaveChanges();
            }
            #endregion
        }
    }
}
