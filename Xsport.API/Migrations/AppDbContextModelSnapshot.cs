﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Xsport.Db;

#nullable disable

namespace Xsport.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("character varying(34)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<long>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Xsport.DB.Entities.Language", b =>
                {
                    b.Property<long>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("LanguageId"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Xsport.DB.Entities.Level", b =>
                {
                    b.Property<long>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("LevelId"));

                    b.Property<int>("MaxPoints")
                        .HasColumnType("integer");

                    b.Property<long>("SportId")
                        .HasColumnType("bigint");

                    b.HasKey("LevelId");

                    b.HasIndex("SportId");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("Xsport.DB.Entities.LevelTranslation", b =>
                {
                    b.Property<long>("LevelTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("LevelTranslationId"));

                    b.Property<long>("LanguageId")
                        .HasColumnType("bigint");

                    b.Property<long>("LevelId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("LevelTranslationId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LevelId");

                    b.ToTable("LevelTranslations");
                });

            modelBuilder.Entity("Xsport.DB.Entities.Match", b =>
                {
                    b.Property<long>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("MatchId"));

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("SportId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("MatchId");

                    b.HasIndex("SportId");

                    b.ToTable("Matchs");
                });

            modelBuilder.Entity("Xsport.DB.Entities.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Xsport.DB.Entities.Sport", b =>
                {
                    b.Property<long>("SportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SportId"));

                    b.Property<int>("BreakPeriod")
                        .HasColumnType("integer");

                    b.Property<bool>("HasExtraRounds")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("NumOfBreaks")
                        .HasColumnType("integer");

                    b.Property<int>("NumOfExtraRounds")
                        .HasColumnType("integer");

                    b.Property<int>("NumOfPlayers")
                        .HasColumnType("integer");

                    b.Property<int>("NumOfReferees")
                        .HasColumnType("integer");

                    b.Property<int>("NumOfRounds")
                        .HasColumnType("integer");

                    b.Property<int>("NumOfTeams")
                        .HasColumnType("integer");

                    b.Property<int>("RoundPeriod")
                        .HasColumnType("integer");

                    b.HasKey("SportId");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreference", b =>
                {
                    b.Property<long>("SportPreferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SportPreferenceId"));

                    b.Property<long>("SportId")
                        .HasColumnType("bigint");

                    b.HasKey("SportPreferenceId");

                    b.HasIndex("SportId");

                    b.ToTable("SportPreferences");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreferenceTranslation", b =>
                {
                    b.Property<long>("SportPreferenceTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SportPreferenceTranslationId"));

                    b.Property<long>("LanguageId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("SportPreferenceId")
                        .HasColumnType("bigint");

                    b.HasKey("SportPreferenceTranslationId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("SportPreferenceId");

                    b.ToTable("SportPreferenceTranslations");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreferenceValue", b =>
                {
                    b.Property<long>("SportPreferenceValueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SportPreferenceValueId"));

                    b.Property<bool?>("IsNotAssigned")
                        .HasColumnType("boolean");

                    b.Property<long>("SportPreferenceId")
                        .HasColumnType("bigint");

                    b.HasKey("SportPreferenceValueId");

                    b.HasIndex("SportPreferenceId");

                    b.ToTable("SportPreferenceValues");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreferenceValueTranslation", b =>
                {
                    b.Property<long>("SportPreferenceValueTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SportPreferenceValueTranslationId"));

                    b.Property<long>("LanguageId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SportPreferenceValueId")
                        .HasColumnType("bigint");

                    b.HasKey("SportPreferenceValueTranslationId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("SportPreferenceValueId");

                    b.ToTable("SportPreferenceValueTranslations");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportTranslation", b =>
                {
                    b.Property<long>("SportTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SportTranslationId"));

                    b.Property<long>("LanguageId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("SportId")
                        .HasColumnType("bigint");

                    b.HasKey("SportTranslationId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("SportId");

                    b.ToTable("SportTranslations");
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserMatch", b =>
                {
                    b.Property<long>("UserMatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("UserMatchId"));

                    b.Property<bool>("IsOrganizer")
                        .HasColumnType("boolean");

                    b.Property<long>("MatchId")
                        .HasColumnType("bigint");

                    b.Property<long>("XsportUserId")
                        .HasColumnType("bigint");

                    b.HasKey("UserMatchId");

                    b.HasIndex("MatchId");

                    b.HasIndex("XsportUserId");

                    b.ToTable("UserMatchs");
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserSport", b =>
                {
                    b.Property<long>("UserSportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("UserSportId"));

                    b.Property<bool>("IsCurrentState")
                        .HasColumnType("boolean");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<long>("SportId")
                        .HasColumnType("bigint");

                    b.Property<long>("XsportUserId")
                        .HasColumnType("bigint");

                    b.HasKey("UserSportId");

                    b.HasIndex("SportId");

                    b.HasIndex("XsportUserId");

                    b.ToTable("UserSports");
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserSportPreferenceValue", b =>
                {
                    b.Property<long>("UserSportPreferenceValueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("UserSportPreferenceValueId"));

                    b.Property<long>("SportPreferenceValueId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserSportId")
                        .HasColumnType("bigint");

                    b.HasKey("UserSportPreferenceValueId");

                    b.HasIndex("SportPreferenceValueId");

                    b.HasIndex("UserSportId");

                    b.ToTable("UserSportPreferenceValues");
                });

            modelBuilder.Entity("Xsport.DB.Entities.XsportRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Xsport.DB.Entities.XsportRoleTranslation", b =>
                {
                    b.Property<long>("XsportRoleTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("XsportRoleTranslationId"));

                    b.Property<long>("LanguageId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("XsportRoleId")
                        .HasColumnType("bigint");

                    b.HasKey("XsportRoleTranslationId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("XsportRoleId");

                    b.ToTable("XsportRoleTranslations");
                });

            modelBuilder.Entity("Xsport.DB.Entities.XsportUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("EmailConfirmationCode")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<int>("LoyaltyPoints")
                        .HasColumnType("integer");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Uid")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("XsportName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<long>");

                    b.Property<long>("SportId")
                        .HasColumnType("bigint");

                    b.HasIndex("SportId");

                    b.HasDiscriminator().HasValue("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Xsport.DB.Entities.XsportRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Xsport.DB.Entities.XsportUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Xsport.DB.Entities.XsportUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Xsport.DB.Entities.XsportRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.XsportUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Xsport.DB.Entities.XsportUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Xsport.DB.Entities.Level", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Sport", "Sport")
                        .WithMany("Levels")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("Xsport.DB.Entities.LevelTranslation", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Language", "Language")
                        .WithMany("LevelTranslations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.Level", "Level")
                        .WithMany("LevelTranslations")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Level");
                });

            modelBuilder.Entity("Xsport.DB.Entities.Match", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Sport", "Sport")
                        .WithMany("Matches")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("Xsport.DB.Entities.RefreshToken", b =>
                {
                    b.HasOne("Xsport.DB.Entities.XsportUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreference", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Sport", "Sport")
                        .WithMany("SportPreferences")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreferenceTranslation", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Language", "Language")
                        .WithMany("SportPreferenceTranslations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.SportPreference", "SportPreference")
                        .WithMany("SportPreferenceTranslations")
                        .HasForeignKey("SportPreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("SportPreference");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreferenceValue", b =>
                {
                    b.HasOne("Xsport.DB.Entities.SportPreference", "SportPreference")
                        .WithMany("SportPreferenceValues")
                        .HasForeignKey("SportPreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SportPreference");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreferenceValueTranslation", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Language", "Language")
                        .WithMany("SportPreferenceValueTranslations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.SportPreferenceValue", "SportPreferenceValue")
                        .WithMany("SportPreferenceValueTranslations")
                        .HasForeignKey("SportPreferenceValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("SportPreferenceValue");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportTranslation", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Language", "Language")
                        .WithMany("SportTranslations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.Sport", "Sport")
                        .WithMany("SportTranslations")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserMatch", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Match", "Match")
                        .WithMany("UserMatchs")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.XsportUser", "User")
                        .WithMany("UserMatchs")
                        .HasForeignKey("XsportUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserSport", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.XsportUser", "XsportUser")
                        .WithMany("UserSports")
                        .HasForeignKey("XsportUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");

                    b.Navigation("XsportUser");
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserSportPreferenceValue", b =>
                {
                    b.HasOne("Xsport.DB.Entities.SportPreferenceValue", "SportPreferenceValue")
                        .WithMany("UserSportPreferenceValues")
                        .HasForeignKey("SportPreferenceValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.UserSport", "UserSport")
                        .WithMany("UserSportPreferenceValues")
                        .HasForeignKey("UserSportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SportPreferenceValue");

                    b.Navigation("UserSport");
                });

            modelBuilder.Entity("Xsport.DB.Entities.XsportRoleTranslation", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Language", "Language")
                        .WithMany("XsportRoleTranslations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xsport.DB.Entities.XsportRole", "XsportRole")
                        .WithMany("XsportRoleTranslations")
                        .HasForeignKey("XsportRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("XsportRole");
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserRole", b =>
                {
                    b.HasOne("Xsport.DB.Entities.Sport", "Sport")
                        .WithMany("UserRoles")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("Xsport.DB.Entities.Language", b =>
                {
                    b.Navigation("LevelTranslations");

                    b.Navigation("SportPreferenceTranslations");

                    b.Navigation("SportPreferenceValueTranslations");

                    b.Navigation("SportTranslations");

                    b.Navigation("XsportRoleTranslations");
                });

            modelBuilder.Entity("Xsport.DB.Entities.Level", b =>
                {
                    b.Navigation("LevelTranslations");
                });

            modelBuilder.Entity("Xsport.DB.Entities.Match", b =>
                {
                    b.Navigation("UserMatchs");
                });

            modelBuilder.Entity("Xsport.DB.Entities.Sport", b =>
                {
                    b.Navigation("Levels");

                    b.Navigation("Matches");

                    b.Navigation("SportPreferences");

                    b.Navigation("SportTranslations");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreference", b =>
                {
                    b.Navigation("SportPreferenceTranslations");

                    b.Navigation("SportPreferenceValues");
                });

            modelBuilder.Entity("Xsport.DB.Entities.SportPreferenceValue", b =>
                {
                    b.Navigation("SportPreferenceValueTranslations");

                    b.Navigation("UserSportPreferenceValues");
                });

            modelBuilder.Entity("Xsport.DB.Entities.UserSport", b =>
                {
                    b.Navigation("UserSportPreferenceValues");
                });

            modelBuilder.Entity("Xsport.DB.Entities.XsportRole", b =>
                {
                    b.Navigation("XsportRoleTranslations");
                });

            modelBuilder.Entity("Xsport.DB.Entities.XsportUser", b =>
                {
                    b.Navigation("UserMatchs");

                    b.Navigation("UserSports");
                });
#pragma warning restore 612, 618
        }
    }
}
