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
            #region Preferences
            if (!context.SportPreferences.Any())
            {
                context.SportPreferences.AddRange(
                    new SportPreference()
                    {
                        SportPreferenceId = 1,
                        SportId = 1,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=1,Name="Favorite Leg"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=1,Name = "القدم المفضلة"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 1,
                                SportPreferenceId=1,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=1,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=1,Name = "غير مخصص"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 2,
                                SportPreferenceId=1,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=2,Name="Left"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=2,Name = "يسار"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 3,
                                SportPreferenceId=1,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=3,Name="Right"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=3,Name = "يمين"}
                                }
                            }
                        }
                    },
                    new SportPreference()
                    {
                        SportPreferenceId = 2,
                        SportId = 1,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=2,Name="Favorite Position"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=2,Name = "المركز المفضل"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 4,
                                SportPreferenceId=2,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=4,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=4,Name = "غير مخصص"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 5,
                                SportPreferenceId=2,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=5,Name="Goal keeper"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=5,Name = "حارس مرمى"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 6,
                                SportPreferenceId=2,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=6,Name="Deffender"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=6,Name = "مدافع"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 7,
                                SportPreferenceId=2,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=7,Name="Middle line"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=7,Name = "خط وسط"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 8,
                                SportPreferenceId=2,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=8,Name="Attacker"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=8,Name = "مهاجم"}
                                }
                            }
                        }
                    },
                    new SportPreference()
                    {
                        SportPreferenceId = 3,
                        SportId = 1,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=3,Name="Favorite time to play"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=3,Name = "الوقت المفضل للعب"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 9,
                                SportPreferenceId=3,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=9,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=9,Name = "غير مخصص"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 10,
                                SportPreferenceId=3,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=10,Name="Morning"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=10,Name = "صباحا"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 11,
                                SportPreferenceId=3,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=11,Name="After noon"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=11,Name = "ظهرا"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 12,
                                SportPreferenceId=3,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=12,Name="Evining"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=12,Name = "مساء"}
                                }
                            }
                        }
                    },
                    new SportPreference()
                    {
                        SportPreferenceId = 4,
                        SportId = 2,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=4,Name="Favorite Hand"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=4,Name = "اليد المفضلة"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 13,
                                SportPreferenceId=4,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=4,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=4,Name = "غير مخصص"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 14,
                                SportPreferenceId=4,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=14,Name="Left"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=14,Name = "يسار"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 15,
                                SportPreferenceId=4,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=15,Name="Right"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=15,Name = "يمين"}
                                }
                            }
                        }
                    },
                    new SportPreference()
                    {
                        SportPreferenceId = 5,
                        SportId = 2,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=5,Name="Favorite time to play"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=5,Name = "الوقت المفضل للعب"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 16,
                                SportPreferenceId=5,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=16,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=16,Name = "غير مخصص"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 17,
                                SportPreferenceId=5,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=17,Name="Morning"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=17,Name = "صباحا"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 18,
                                SportPreferenceId=5,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=11,Name="After noon"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=11,Name = "ظهرا"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 19,
                                SportPreferenceId=5,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=19,Name="Evining"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=19,Name = "مساء"}
                                }
                            }
                        }
                    },
                    new SportPreference()
                    {
                        SportPreferenceId = 6,
                        SportId = 2,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=6,Name="Favorite Tennis type"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=6,Name = "نوع التنس المفضل"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 20,
                                SportPreferenceId=6,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=20,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=20,Name = "غير مخصص"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 21,
                                SportPreferenceId=6,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=21,Name="Singles "},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=21,Name = "أفراد"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 22,
                                SportPreferenceId=6,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=22,Name="Doubles"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=22,Name = "ثنائيات"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 23,
                                SportPreferenceId=6,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=23,Name="Mixed doubles"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=23,Name = "مختلط"}
                                }
                            }
                        }
                    },
                    new SportPreference()
                    {
                        SportPreferenceId = 7,
                        SportId = 3,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=7,Name="Favorite Hand"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=7,Name = "اليد المفضلة"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 24,
                                SportPreferenceId=7,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=24,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=24,Name = "غير مخصص"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 25,
                                SportPreferenceId=7,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=25,Name="Left"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=25,Name = "يسار"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 26,
                                SportPreferenceId=7,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=26,Name="Right"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=26,Name = "يمين"}
                                }
                            }
                        }
                    },
                    new SportPreference()
                    {
                        SportPreferenceId = 8,
                        SportId = 3,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=8,Name="Favorite time to play"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=8,Name = "الوقت المفضل للعب"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 27,
                                SportPreferenceId=8,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=27,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=27,Name = "غير مخصص"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 28,
                                SportPreferenceId=8,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=28,Name="Morning"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=28,Name = "صباحا"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 29,
                                SportPreferenceId=8,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=29,Name="After noon"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=29,Name = "ظهرا"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 30,
                                SportPreferenceId=8,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=30,Name="Evining"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=30,Name = "مساء"}
                                }
                            }
                        }
                    },
                    new SportPreference()
                    {
                        SportPreferenceId = 9,
                        SportId = 3,
                        SportPreferenceTranslations = new List<SportPreferenceTranslation>()
                        {
                            new SportPreferenceTranslation {LanguageId = 1, SportPreferenceId=9,Name="Favorite position"},
                            new SportPreferenceTranslation { LanguageId=2, SportPreferenceId=9,Name = "الموقع المفضل"}
                        },
                        SportPreferenceValues = new List<SportPreferenceValue>()
                        {
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 31,
                                SportPreferenceId=9,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=31,Name="On the right"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=31,Name = "على اليمين"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 32,
                                SportPreferenceId=9,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=32,Name="On the left"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=32,Name = "على اليسار"}
                                }
                            },
                            new SportPreferenceValue
                            {
                                SportPreferenceValueId = 33,
                                SportPreferenceId=9,
                                IsNotAssigned = true,
                                SportPreferenceValueTranslations = new List<SportPreferenceValueTranslation>()
                                {
                                    new SportPreferenceValueTranslation {LanguageId = 1, SportPreferenceValueId=33,Name="Not assigned"},
                                    new SportPreferenceValueTranslation { LanguageId=2,SportPreferenceValueId=33,Name = "غير مخصص"}
                                }
                            }
                        }
                    }

                 );
                context.SaveChanges();
            }
            #endregion Preferences
        }
    }
}
