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
                        Levels = new List<Level>()
                        {
                            new Level()
                            {
                                LevelId = 1,
                                MaxPoints = 1000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=1, Name = "Starter"},
                                    new LevelTranslation{LanguageId=2, LevelId =1, Name = "مبتدئ"}
                                }
                            },
                            new Level()
                            {
                                LevelId = 2,
                                MaxPoints = 3000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=2, Name = "Intermediate"},
                                    new LevelTranslation{LanguageId=2, LevelId =2, Name = "هاوٍ"}
                                }
                            },
                            new Level()
                            {
                                LevelId = 3,
                                MaxPoints = 5000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=3, Name = "Expert"},
                                    new LevelTranslation{LanguageId=2, LevelId =3, Name = "متمرس"}
                                }
                            }
                        }
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
                            new() { LanguageId = 2, SportId = 2, Name = "تنس" }
                        ],
                        Levels = new List<Level>()
                        {
                            new Level()
                            {
                                LevelId = 4,
                                MaxPoints = 1000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=4, Name = "Starter"},
                                    new LevelTranslation{LanguageId=2, LevelId =4, Name = "مبتدئ"}
                                }
                            },
                            new Level()
                            {
                                LevelId = 5,
                                MaxPoints = 3000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=5, Name = "Intermediate"},
                                    new LevelTranslation{LanguageId=2, LevelId =5, Name = "هاوٍ"}
                                }
                            },
                            new Level()
                            {
                                LevelId = 6,
                                MaxPoints = 5000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=6, Name = "Expert"},
                                    new LevelTranslation{LanguageId=2, LevelId =6, Name = "متمرس"}
                                }
                            }
                        }
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
                            new() { LanguageId = 2, SportId = 2, Name = "بادل" }
                        ],
                        Levels = new List<Level>()
                        {
                            new Level()
                            {
                                LevelId = 7,
                                MaxPoints = 1000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=7, Name = "Starter"},
                                    new LevelTranslation{LanguageId=2, LevelId =7, Name = "مبتدئ"}
                                }
                            },
                            new Level()
                            {
                                LevelId = 8,
                                MaxPoints = 3000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=8, Name = "Intermediate"},
                                    new LevelTranslation{LanguageId=2, LevelId =8, Name = "هاوٍ"}
                                }
                            },
                            new Level()
                            {
                                LevelId = 9,
                                MaxPoints = 5000,
                                LevelTranslations = new List<LevelTranslation>()
                                {
                                    new LevelTranslation {LanguageId=1, LevelId=9, Name = "Expert"},
                                    new LevelTranslation{LanguageId=2, LevelId =9, Name = "متمرس"}
                                }
                            }
                        }
                    });
                context.SaveChanges();
            }
            #endregion
        }
    }
}
