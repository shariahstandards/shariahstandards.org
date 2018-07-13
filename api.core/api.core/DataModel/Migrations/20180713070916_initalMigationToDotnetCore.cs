using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataModel.Migrations
{
    public partial class initalMigationToDotnetCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Humans",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        FullName = table.Column<string>(maxLength: 500, nullable: false),
            //        RegistrationDateTimeUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Humans", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Prefix",
            //    columns: table => new
            //    {
            //        Text = table.Column<string>(maxLength: 20, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Prefix", x => x.Text);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Root",
            //    columns: table => new
            //    {
            //        Text = table.Column<string>(maxLength: 20, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Root", x => x.Text);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ShurahBasedOrganisation",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Name = table.Column<string>(maxLength: 250, nullable: false),
            //        Description = table.Column<string>(maxLength: 4000, nullable: true),
            //        LastUpdateDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        Closed = table.Column<bool>(nullable: false),
            //        UrlDomain = table.Column<string>(maxLength: 200, nullable: true),
            //        JoiningPolicy = table.Column<int>(nullable: false),
            //        RequiredNumbersOfAcceptingMembers = table.Column<int>(nullable: false),
            //        CountingInProgress = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ShurahBasedOrganisation", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Surah",
            //    columns: table => new
            //    {
            //        SurahNumber = table.Column<int>(nullable: false),
            //        ArabicName = table.Column<string>(maxLength: 100, nullable: false),
            //        EnglishName = table.Column<string>(maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Surah", x => x.SurahNumber);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WordPartForm",
            //    columns: table => new
            //    {
            //        Text = table.Column<string>(maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WordPartForm", x => x.Text);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WordPartPositionType",
            //    columns: table => new
            //    {
            //        Code = table.Column<string>(maxLength: 10, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WordPartPositionType", x => x.Code);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WordPartType",
            //    columns: table => new
            //    {
            //        Code = table.Column<string>(maxLength: 10, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WordPartType", x => x.Code);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Auth0User",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(maxLength: 200, nullable: false),
            //        Name = table.Column<string>(maxLength: 200, nullable: false),
            //        PictureUrl = table.Column<string>(maxLength: 1000, nullable: false),
            //        HumanId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Auth0User", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Auth0User_Humans_HumanId",
            //            column: x => x.HumanId,
            //            principalTable: "Humans",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Action",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Description = table.Column<string>(maxLength: 500, nullable: false),
            //        TimeAndDateOfDecisionUtc = table.Column<DateTime>(nullable: false),
            //        OrganisationId = table.Column<int>(nullable: false),
            //        Priority = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Action", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Action_ShurahBasedOrganisation_OrganisationId",
            //            column: x => x.OrganisationId,
            //            principalTable: "ShurahBasedOrganisation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Member",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        JoinedOnDateAndTimeUtc = table.Column<DateTime>(nullable: false),
            //        OrganisationId = table.Column<int>(nullable: false),
            //        Moderated = table.Column<bool>(nullable: false),
            //        Removed = table.Column<bool>(nullable: false),
            //        LastDateAndTimeUtcAgreedToMembershipRules = table.Column<DateTime>(nullable: false),
            //        Introduction = table.Column<string>(maxLength: 4000, nullable: true),
            //        PublicName = table.Column<string>(nullable: true),
            //        EmailAddress = table.Column<string>(nullable: true),
            //        SendNoEmailNotifications = table.Column<bool>(nullable: false),
            //        FollowerCount = table.Column<int>(nullable: false),
            //        HumanId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Member", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Member_Humans_HumanId",
            //            column: x => x.HumanId,
            //            principalTable: "Humans",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Member_ShurahBasedOrganisation_OrganisationId",
            //            column: x => x.OrganisationId,
            //            principalTable: "ShurahBasedOrganisation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleSection",
            //    columns: table => new
            //    {
            //        ShurahBasedOrganisationId = table.Column<int>(nullable: false),
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Title = table.Column<string>(maxLength: 250, nullable: false),
            //        UniqueInOrganisationName = table.Column<string>(maxLength: 100, nullable: false),
            //        Sequence = table.Column<int>(nullable: false),
            //        PublishedDateTimeUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleSection", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleSection_ShurahBasedOrganisation_ShurahBasedOrganisationId",
            //            column: x => x.ShurahBasedOrganisationId,
            //            principalTable: "ShurahBasedOrganisation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleTermDefinition",
            //    columns: table => new
            //    {
            //        OrganisationId = table.Column<int>(nullable: false),
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Term = table.Column<string>(maxLength: 100, nullable: false),
            //        Definition = table.Column<string>(maxLength: 5000, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleTermDefinition", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleTermDefinition_ShurahBasedOrganisation_OrganisationId",
            //            column: x => x.OrganisationId,
            //            principalTable: "ShurahBasedOrganisation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganisationRelationship",
            //    columns: table => new
            //    {
            //        ShurahBasedOrganisationId = table.Column<int>(nullable: false),
            //        ParentOrganisationId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganisationRelationship", x => x.ShurahBasedOrganisationId);
            //        table.ForeignKey(
            //            name: "FK_OrganisationRelationship_ShurahBasedOrganisation_ParentOrganisationId",
            //            column: x => x.ParentOrganisationId,
            //            principalTable: "ShurahBasedOrganisation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_OrganisationRelationship_ShurahBasedOrganisation_ShurahBasedOrganisationId",
            //            column: x => x.ShurahBasedOrganisationId,
            //            principalTable: "ShurahBasedOrganisation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Verse",
            //    columns: table => new
            //    {
            //        SurahNumber = table.Column<int>(nullable: false),
            //        VerseNumber = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Verse", x => new { x.SurahNumber, x.VerseNumber });
            //        table.ForeignKey(
            //            name: "FK_Verse_Surah_SurahNumber",
            //            column: x => x.SurahNumber,
            //            principalTable: "Surah",
            //            principalColumn: "SurahNumber",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ContactDetail",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ContactDetailType = table.Column<int>(nullable: false),
            //        Value = table.Column<string>(maxLength: 250, nullable: false),
            //        Auth0UserId = table.Column<string>(nullable: true),
            //        Verified = table.Column<bool>(nullable: false),
            //        HumanId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ContactDetail", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ContactDetail_Auth0User_Auth0UserId",
            //            column: x => x.Auth0UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_ContactDetail_Humans_HumanId",
            //            column: x => x.HumanId,
            //            principalTable: "Humans",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipApplication",
            //    columns: table => new
            //    {
            //        OrganisationId = table.Column<int>(nullable: false),
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Auth0UserId = table.Column<string>(maxLength: 200, nullable: false),
            //        SupportingStatement = table.Column<string>(maxLength: 2000, nullable: true),
            //        Email = table.Column<string>(maxLength: 250, nullable: false),
            //        PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
            //        PublicName = table.Column<string>(nullable: true),
            //        DateAppliedUtc = table.Column<DateTime>(nullable: false),
            //        HumanId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipApplication", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipApplication_Auth0User_Auth0UserId",
            //            column: x => x.Auth0UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MembershipApplication_Humans_HumanId",
            //            column: x => x.HumanId,
            //            principalTable: "Humans",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MembershipApplication_ShurahBasedOrganisation_OrganisationId",
            //            column: x => x.OrganisationId,
            //            principalTable: "ShurahBasedOrganisation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuranComment",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Surah = table.Column<int>(nullable: false),
            //        Verse = table.Column<int>(nullable: false),
            //        Word = table.Column<int>(nullable: false),
            //        CommentText = table.Column<string>(maxLength: 4000, nullable: false),
            //        Published = table.Column<bool>(nullable: false),
            //        Auth0UserId = table.Column<string>(nullable: true),
            //        HumanId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuranComment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_QuranComment_Auth0User_Auth0UserId",
            //            column: x => x.Auth0UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_QuranComment_Humans_HumanId",
            //            column: x => x.HumanId,
            //            principalTable: "Humans",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserBirthLocation",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        Longitude = table.Column<double>(nullable: false),
            //        Latitude = table.Column<double>(nullable: false),
            //        Town = table.Column<string>(maxLength: 50, nullable: false),
            //        Country = table.Column<string>(maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserBirthLocation", x => x.UserId);
            //        table.ForeignKey(
            //            name: "FK_UserBirthLocation_Auth0User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserEmail",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        Value = table.Column<string>(maxLength: 150, nullable: false),
            //        Verified = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserEmail", x => x.UserId);
            //        table.ForeignKey(
            //            name: "FK_UserEmail_Auth0User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserFathersFirstName",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        Value = table.Column<string>(maxLength: 50, nullable: false),
            //        AgeSequence = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserFathersFirstName", x => x.UserId);
            //        table.ForeignKey(
            //            name: "FK_UserFathersFirstName_Auth0User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserFirstName",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        Value = table.Column<string>(maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserFirstName", x => x.UserId);
            //        table.ForeignKey(
            //            name: "FK_UserFirstName_Auth0User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserMobilePhone",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        Value = table.Column<string>(maxLength: 150, nullable: false),
            //        Verified = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserMobilePhone", x => x.UserId);
            //        table.ForeignKey(
            //            name: "FK_UserMobilePhone_Auth0User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserPhotograph",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        Value = table.Column<byte[]>(maxLength: 500000, nullable: false),
            //        MimeType = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserPhotograph", x => x.UserId);
            //        table.ForeignKey(
            //            name: "FK_UserPhotograph_Auth0User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ActionUpdate",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ActionId = table.Column<int>(nullable: false),
            //        UpdateDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        UpdatedDescription = table.Column<string>(maxLength: 500, nullable: false),
            //        Status = table.Column<int>(nullable: false),
            //        ResponsibleMemberId = table.Column<int>(nullable: false),
            //        HoursWorkedSincePreviousUpdate = table.Column<decimal>(type: "decimal(4,1)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ActionUpdate", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ActionUpdate_Action_ActionId",
            //            column: x => x.ActionId,
            //            principalTable: "Action",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ActionUpdate_Member_ActionId",
            //            column: x => x.ActionId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DelegatedPermission",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ShurahOrganisationPermission = table.Column<int>(nullable: false),
            //        MemberId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DelegatedPermission", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_DelegatedPermission_Member_MemberId",
            //            column: x => x.MemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LeaderRecognition",
            //    columns: table => new
            //    {
            //        MemberId = table.Column<int>(nullable: false),
            //        RecognisedLeaderMemberId = table.Column<int>(nullable: false),
            //        LastUpdateDateTimeUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LeaderRecognition", x => x.MemberId);
            //        table.ForeignKey(
            //            name: "FK_LeaderRecognition_Member_MemberId",
            //            column: x => x.MemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LeaderRecognition_Member_RecognisedLeaderMemberId",
            //            column: x => x.RecognisedLeaderMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MemberAuth0User",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Auth0UserId = table.Column<string>(nullable: true),
            //        MemberId = table.Column<int>(nullable: false),
            //        Suspended = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MemberAuth0User", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MemberAuth0User_Auth0User_Auth0UserId",
            //            column: x => x.Auth0UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MemberAuth0User_Member_MemberId",
            //            column: x => x.MemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipInvitation",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        EmailAddressList = table.Column<string>(maxLength: 4000, nullable: true),
            //        DateTimeInvitationsSetUtc = table.Column<DateTime>(nullable: false),
            //        InviterMemberId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipInvitation", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipInvitation_Member_InviterMemberId",
            //            column: x => x.InviterMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganisationLeader",
            //    columns: table => new
            //    {
            //        OrganisationId = table.Column<int>(nullable: false),
            //        LeaderMemberId = table.Column<int>(nullable: false),
            //        LastUpdateDateTimeUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganisationLeader", x => x.OrganisationId);
            //        table.ForeignKey(
            //            name: "FK_OrganisationLeader_Member_LeaderMemberId",
            //            column: x => x.LeaderMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_OrganisationLeader_ShurahBasedOrganisation_OrganisationId",
            //            column: x => x.OrganisationId,
            //            principalTable: "ShurahBasedOrganisation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Suggestion",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ShortDescription = table.Column<string>(maxLength: 100, nullable: false),
            //        FullText = table.Column<string>(maxLength: 4000, nullable: false),
            //        AuthorMemberId = table.Column<int>(nullable: false),
            //        Removed = table.Column<bool>(nullable: false),
            //        PendingModeration = table.Column<bool>(nullable: false),
            //        CreatedDateUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Suggestion", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Suggestion_Member_AuthorMemberId",
            //            column: x => x.AuthorMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRule",
            //    columns: table => new
            //    {
            //        MembershipRuleSectionId = table.Column<int>(nullable: false),
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Sequence = table.Column<int>(nullable: false),
            //        RuleStatement = table.Column<string>(maxLength: 500, nullable: false),
            //        PublishedDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        NumberOfCorrectAnswersRequired = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRule", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipRule_MembershipRuleSection_MembershipRuleSectionId",
            //            column: x => x.MembershipRuleSectionId,
            //            principalTable: "MembershipRuleSection",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleSectionAcceptance",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Auth0UserId = table.Column<string>(nullable: true),
            //        MembershipRuleSectionId = table.Column<int>(nullable: false),
            //        AcceptedDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        HumanId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleSectionAcceptance", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleSectionAcceptance_Auth0User_Auth0UserId",
            //            column: x => x.Auth0UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleSectionAcceptance_Humans_HumanId",
            //            column: x => x.HumanId,
            //            principalTable: "Humans",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleSectionAcceptance_MembershipRuleSection_MembershipRuleSectionId",
            //            column: x => x.MembershipRuleSectionId,
            //            principalTable: "MembershipRuleSection",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleSectionRelationship",
            //    columns: table => new
            //    {
            //        MembershipRuleSectionId = table.Column<int>(nullable: false),
            //        ParentMembershipRuleSectionId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleSectionRelationship", x => x.MembershipRuleSectionId);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleSectionRelationship_MembershipRuleSection_MembershipRuleSectionId",
            //            column: x => x.MembershipRuleSectionId,
            //            principalTable: "MembershipRuleSection",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleSectionRelationship_MembershipRuleSection_ParentMembershipRuleSectionId",
            //            column: x => x.ParentMembershipRuleSectionId,
            //            principalTable: "MembershipRuleSection",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VerseTranslation",
            //    columns: table => new
            //    {
            //        SurahNumber = table.Column<int>(nullable: false),
            //        VerseNumber = table.Column<int>(nullable: false),
            //        Text = table.Column<string>(maxLength: 4000, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VerseTranslation", x => new { x.SurahNumber, x.VerseNumber });
            //        table.ForeignKey(
            //            name: "FK_VerseTranslation_Verse_SurahNumber_VerseNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber },
            //            principalTable: "Verse",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber" },
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Word",
            //    columns: table => new
            //    {
            //        SurahNumber = table.Column<int>(nullable: false),
            //        VerseNumber = table.Column<int>(nullable: false),
            //        WordNumber = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Word", x => new { x.SurahNumber, x.VerseNumber, x.WordNumber });
            //        table.ForeignKey(
            //            name: "FK_Word_Surah_SurahNumber",
            //            column: x => x.SurahNumber,
            //            principalTable: "Surah",
            //            principalColumn: "SurahNumber",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Word_Verse_SurahNumber_VerseNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber },
            //            principalTable: "Verse",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber" },
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipApplicationAcceptance",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        MembershipApplicationId = table.Column<int>(nullable: false),
            //        AcceptingMemberId = table.Column<int>(nullable: false),
            //        AcceptanceDateTimeUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipApplicationAcceptance", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipApplicationAcceptance_Member_AcceptingMemberId",
            //            column: x => x.AcceptingMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MembershipApplicationAcceptance_MembershipApplication_MembershipApplicationId",
            //            column: x => x.MembershipApplicationId,
            //            principalTable: "MembershipApplication",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "QuranCommentLink",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        QuranCommentId = table.Column<int>(nullable: false),
            //        Surah = table.Column<int>(nullable: false),
            //        Verse = table.Column<int>(nullable: false),
            //        Word = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_QuranCommentLink", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_QuranCommentLink_QuranComment_QuranCommentId",
            //            column: x => x.QuranCommentId,
            //            principalTable: "QuranComment",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SuggestionComment",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        SuggestionId = table.Column<int>(nullable: false),
            //        CommentingMemberId = table.Column<int>(nullable: false),
            //        CommentIsSupportingSuggestion = table.Column<bool>(nullable: true),
            //        LastUpdateDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        Comment = table.Column<string>(maxLength: 1000, nullable: false),
            //        IsCensored = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SuggestionComment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_SuggestionComment_Member_CommentingMemberId",
            //            column: x => x.CommentingMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SuggestionComment_Suggestion_SuggestionId",
            //            column: x => x.SuggestionId,
            //            principalTable: "Suggestion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SuggestionVote",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        SuggestionId = table.Column<int>(nullable: false),
            //        VoterMemberId = table.Column<int>(nullable: false),
            //        MemberIsSupportingSuggestion = table.Column<bool>(nullable: true),
            //        LastUpdateDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        VotingLeaderMemberId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SuggestionVote", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_SuggestionVote_Suggestion_SuggestionId",
            //            column: x => x.SuggestionId,
            //            principalTable: "Suggestion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_SuggestionVote_Member_VoterMemberId",
            //            column: x => x.VoterMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SuggestionVote_Member_VotingLeaderMemberId",
            //            column: x => x.VotingLeaderMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ArchivedMembershipRule",
            //    columns: table => new
            //    {
            //        MembershipRuleId = table.Column<int>(nullable: false),
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        RuleStatement = table.Column<string>(nullable: true),
            //        PublishedDateTimeUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ArchivedMembershipRule", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ArchivedMembershipRule_MembershipRule_MembershipRuleId",
            //            column: x => x.MembershipRuleId,
            //            principalTable: "MembershipRule",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleComprehensionQuestion",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        MembershipRuleId = table.Column<int>(nullable: false),
            //        Question = table.Column<string>(maxLength: 1000, nullable: true),
            //        LastUpdatedDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        RequiredCorrectAnswerMaximumTime = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleComprehensionQuestion", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleComprehensionQuestion_MembershipRule_MembershipRuleId",
            //            column: x => x.MembershipRuleId,
            //            principalTable: "MembershipRule",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleViolationAccusation",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        MembershipRuleId = table.Column<int>(nullable: false),
            //        ExplanationOfClaim = table.Column<string>(maxLength: 4000, nullable: false),
            //        ClaimingMemberId = table.Column<int>(nullable: false),
            //        AccusedMemberId = table.Column<int>(nullable: false),
            //        RequestedRemedy = table.Column<string>(maxLength: 4000, nullable: false),
            //        RecordeDateTimeUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleViolationAccusation", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleViolationAccusation_Member_AccusedMemberId",
            //            column: x => x.AccusedMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleViolationAccusation_Member_ClaimingMemberId",
            //            column: x => x.ClaimingMemberId,
            //            principalTable: "Member",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleViolationAccusation_MembershipRule_MembershipRuleId",
            //            column: x => x.MembershipRuleId,
            //            principalTable: "MembershipRule",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WordPart",
            //    columns: table => new
            //    {
            //        SurahNumber = table.Column<int>(nullable: false),
            //        VerseNumber = table.Column<int>(nullable: false),
            //        WordNumber = table.Column<int>(nullable: false),
            //        WordPartNumber = table.Column<int>(nullable: false),
            //        WordPartTypeCode = table.Column<string>(maxLength: 10, nullable: true),
            //        WordPartPositionTypeCode = table.Column<string>(nullable: true),
            //        Text = table.Column<string>(maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WordPart", x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
            //        table.ForeignKey(
            //            name: "FK_WordPart_Surah_SurahNumber",
            //            column: x => x.SurahNumber,
            //            principalTable: "Surah",
            //            principalColumn: "SurahNumber",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_WordPart_WordPartForm_Text",
            //            column: x => x.Text,
            //            principalTable: "WordPartForm",
            //            principalColumn: "Text",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_WordPart_WordPartPositionType_WordPartPositionTypeCode",
            //            column: x => x.WordPartPositionTypeCode,
            //            principalTable: "WordPartPositionType",
            //            principalColumn: "Code",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_WordPart_WordPartType_WordPartTypeCode",
            //            column: x => x.WordPartTypeCode,
            //            principalTable: "WordPartType",
            //            principalColumn: "Code",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_WordPart_Verse_SurahNumber_VerseNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber },
            //            principalTable: "Verse",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber" },
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_WordPart_Word_SurahNumber_VerseNumber_WordNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber, x.WordNumber },
            //            principalTable: "Word",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber", "WordNumber" },
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleComprehensionAnswer",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        MembershipRuleComprehensionQuestionId = table.Column<int>(nullable: false),
            //        Answer = table.Column<string>(maxLength: 1000, nullable: true),
            //        LastUpdatedDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        Correct = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleComprehensionAnswer", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleComprehensionAnswer_MembershipRuleComprehensionQuestion_MembershipRuleComprehensionQuestionId",
            //            column: x => x.MembershipRuleComprehensionQuestionId,
            //            principalTable: "MembershipRuleComprehensionQuestion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleComprehensionTestResult",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        MembershipRuleComprehensionQuestionId = table.Column<int>(nullable: false),
            //        Auuth0UserId = table.Column<string>(nullable: true),
            //        StartedDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        AnsweredDateTimeUtc = table.Column<DateTime>(nullable: false),
            //        CorrectlyAnswered = table.Column<bool>(nullable: false),
            //        HumanId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleComprehensionTestResult", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleComprehensionTestResult_Auth0User_Auuth0UserId",
            //            column: x => x.Auuth0UserId,
            //            principalTable: "Auth0User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleComprehensionTestResult_Humans_HumanId",
            //            column: x => x.HumanId,
            //            principalTable: "Humans",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleComprehensionTestResult_MembershipRuleComprehensionQuestion_MembershipRuleComprehensionQuestionId",
            //            column: x => x.MembershipRuleComprehensionQuestionId,
            //            principalTable: "MembershipRuleComprehensionQuestion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MembershipRuleViolationJudgement",
            //    columns: table => new
            //    {
            //        MembershipRuleViolationAccusationId = table.Column<int>(nullable: false),
            //        Remedy = table.Column<string>(maxLength: 4000, nullable: false),
            //        RulingExplanation = table.Column<string>(nullable: true),
            //        RemedyCompleted = table.Column<bool>(nullable: false),
            //        RecordeDateTimeUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembershipRuleViolationJudgement", x => x.MembershipRuleViolationAccusationId);
            //        table.ForeignKey(
            //            name: "FK_MembershipRuleViolationJudgement_MembershipRuleViolationAccusation_MembershipRuleViolationAccusationId",
            //            column: x => x.MembershipRuleViolationAccusationId,
            //            principalTable: "MembershipRuleViolationAccusation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PrefixUsage",
            //    columns: table => new
            //    {
            //        SurahNumber = table.Column<int>(nullable: false),
            //        VerseNumber = table.Column<int>(nullable: false),
            //        WordNumber = table.Column<int>(nullable: false),
            //        WordPartNumber = table.Column<int>(nullable: false),
            //        Text = table.Column<string>(maxLength: 20, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PrefixUsage", x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
            //        table.ForeignKey(
            //            name: "FK_PrefixUsage_Surah_SurahNumber",
            //            column: x => x.SurahNumber,
            //            principalTable: "Surah",
            //            principalColumn: "SurahNumber",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_PrefixUsage_Prefix_Text",
            //            column: x => x.Text,
            //            principalTable: "Prefix",
            //            principalColumn: "Text",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_PrefixUsage_Verse_SurahNumber_VerseNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber },
            //            principalTable: "Verse",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber" },
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_PrefixUsage_Word_SurahNumber_VerseNumber_WordNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber, x.WordNumber },
            //            principalTable: "Word",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber", "WordNumber" },
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_PrefixUsage_WordPart_SurahNumber_VerseNumber_WordNumber_WordPartNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber },
            //            principalTable: "WordPart",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber", "WordNumber", "WordPartNumber" },
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RootUsage",
            //    columns: table => new
            //    {
            //        SurahNumber = table.Column<int>(nullable: false),
            //        VerseNumber = table.Column<int>(nullable: false),
            //        WordNumber = table.Column<int>(nullable: false),
            //        WordPartNumber = table.Column<int>(nullable: false),
            //        RootText = table.Column<string>(maxLength: 20, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RootUsage", x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
            //        table.ForeignKey(
            //            name: "FK_RootUsage_Root_RootText",
            //            column: x => x.RootText,
            //            principalTable: "Root",
            //            principalColumn: "Text",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RootUsage_Surah_SurahNumber",
            //            column: x => x.SurahNumber,
            //            principalTable: "Surah",
            //            principalColumn: "SurahNumber",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RootUsage_Verse_SurahNumber_VerseNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber },
            //            principalTable: "Verse",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber" },
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RootUsage_Word_SurahNumber_VerseNumber_WordNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber, x.WordNumber },
            //            principalTable: "Word",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber", "WordNumber" },
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RootUsage_WordPart_SurahNumber_VerseNumber_WordNumber_WordPartNumber",
            //            columns: x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber },
            //            principalTable: "WordPart",
            //            principalColumns: new[] { "SurahNumber", "VerseNumber", "WordNumber", "WordPartNumber" },
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Action_OrganisationId",
            //    table: "Action",
            //    column: "OrganisationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ActionUpdate_ActionId",
            //    table: "ActionUpdate",
            //    column: "ActionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ArchivedMembershipRule_MembershipRuleId",
            //    table: "ArchivedMembershipRule",
            //    column: "MembershipRuleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Auth0User_HumanId",
            //    table: "Auth0User",
            //    column: "HumanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ContactDetail_Auth0UserId",
            //    table: "ContactDetail",
            //    column: "Auth0UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ContactDetail_HumanId",
            //    table: "ContactDetail",
            //    column: "HumanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DelegatedPermission_MemberId",
            //    table: "DelegatedPermission",
            //    column: "MemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LeaderRecognition_RecognisedLeaderMemberId",
            //    table: "LeaderRecognition",
            //    column: "RecognisedLeaderMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Member_HumanId",
            //    table: "Member",
            //    column: "HumanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Member_OrganisationId",
            //    table: "Member",
            //    column: "OrganisationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MemberAuth0User_Auth0UserId",
            //    table: "MemberAuth0User",
            //    column: "Auth0UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MemberAuth0User_MemberId",
            //    table: "MemberAuth0User",
            //    column: "MemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipApplication_Auth0UserId",
            //    table: "MembershipApplication",
            //    column: "Auth0UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipApplication_HumanId",
            //    table: "MembershipApplication",
            //    column: "HumanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipApplication_OrganisationId",
            //    table: "MembershipApplication",
            //    column: "OrganisationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipApplicationAcceptance_AcceptingMemberId",
            //    table: "MembershipApplicationAcceptance",
            //    column: "AcceptingMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipApplicationAcceptance_MembershipApplicationId",
            //    table: "MembershipApplicationAcceptance",
            //    column: "MembershipApplicationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipInvitation_InviterMemberId",
            //    table: "MembershipInvitation",
            //    column: "InviterMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRule_MembershipRuleSectionId",
            //    table: "MembershipRule",
            //    column: "MembershipRuleSectionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleComprehensionAnswer_MembershipRuleComprehensionQuestionId",
            //    table: "MembershipRuleComprehensionAnswer",
            //    column: "MembershipRuleComprehensionQuestionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleComprehensionQuestion_MembershipRuleId",
            //    table: "MembershipRuleComprehensionQuestion",
            //    column: "MembershipRuleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleComprehensionTestResult_Auuth0UserId",
            //    table: "MembershipRuleComprehensionTestResult",
            //    column: "Auuth0UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleComprehensionTestResult_HumanId",
            //    table: "MembershipRuleComprehensionTestResult",
            //    column: "HumanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleComprehensionTestResult_MembershipRuleComprehensionQuestionId",
            //    table: "MembershipRuleComprehensionTestResult",
            //    column: "MembershipRuleComprehensionQuestionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleSection_ShurahBasedOrganisationId",
            //    table: "MembershipRuleSection",
            //    column: "ShurahBasedOrganisationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleSectionAcceptance_Auth0UserId",
            //    table: "MembershipRuleSectionAcceptance",
            //    column: "Auth0UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleSectionAcceptance_HumanId",
            //    table: "MembershipRuleSectionAcceptance",
            //    column: "HumanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleSectionAcceptance_MembershipRuleSectionId",
            //    table: "MembershipRuleSectionAcceptance",
            //    column: "MembershipRuleSectionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleSectionRelationship_ParentMembershipRuleSectionId",
            //    table: "MembershipRuleSectionRelationship",
            //    column: "ParentMembershipRuleSectionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleTermDefinition_OrganisationId",
            //    table: "MembershipRuleTermDefinition",
            //    column: "OrganisationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleViolationAccusation_AccusedMemberId",
            //    table: "MembershipRuleViolationAccusation",
            //    column: "AccusedMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleViolationAccusation_ClaimingMemberId",
            //    table: "MembershipRuleViolationAccusation",
            //    column: "ClaimingMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembershipRuleViolationAccusation_MembershipRuleId",
            //    table: "MembershipRuleViolationAccusation",
            //    column: "MembershipRuleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganisationLeader_LeaderMemberId",
            //    table: "OrganisationLeader",
            //    column: "LeaderMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganisationRelationship_ParentOrganisationId",
            //    table: "OrganisationRelationship",
            //    column: "ParentOrganisationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PrefixUsage_Text",
            //    table: "PrefixUsage",
            //    column: "Text");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuranComment_Auth0UserId",
            //    table: "QuranComment",
            //    column: "Auth0UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuranComment_HumanId",
            //    table: "QuranComment",
            //    column: "HumanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_QuranCommentLink_QuranCommentId",
            //    table: "QuranCommentLink",
            //    column: "QuranCommentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RootUsage_RootText",
            //    table: "RootUsage",
            //    column: "RootText");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShurahBasedOrganisation_Name",
            //    table: "ShurahBasedOrganisation",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Suggestion_AuthorMemberId",
            //    table: "Suggestion",
            //    column: "AuthorMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SuggestionComment_CommentingMemberId",
            //    table: "SuggestionComment",
            //    column: "CommentingMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SuggestionComment_SuggestionId",
            //    table: "SuggestionComment",
            //    column: "SuggestionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SuggestionVote_SuggestionId",
            //    table: "SuggestionVote",
            //    column: "SuggestionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SuggestionVote_VoterMemberId",
            //    table: "SuggestionVote",
            //    column: "VoterMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SuggestionVote_VotingLeaderMemberId",
            //    table: "SuggestionVote",
            //    column: "VotingLeaderMemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WordPart_Text",
            //    table: "WordPart",
            //    column: "Text");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WordPart_WordPartPositionTypeCode",
            //    table: "WordPart",
            //    column: "WordPartPositionTypeCode");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WordPart_WordPartTypeCode",
            //    table: "WordPart",
            //    column: "WordPartTypeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropTable(
        //        name: "ActionUpdate");

        //    migrationBuilder.DropTable(
        //        name: "ArchivedMembershipRule");

        //    migrationBuilder.DropTable(
        //        name: "ContactDetail");

        //    migrationBuilder.DropTable(
        //        name: "DelegatedPermission");

        //    migrationBuilder.DropTable(
        //        name: "LeaderRecognition");

        //    migrationBuilder.DropTable(
        //        name: "MemberAuth0User");

        //    migrationBuilder.DropTable(
        //        name: "MembershipApplicationAcceptance");

        //    migrationBuilder.DropTable(
        //        name: "MembershipInvitation");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleComprehensionAnswer");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleComprehensionTestResult");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleSectionAcceptance");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleSectionRelationship");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleTermDefinition");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleViolationJudgement");

        //    migrationBuilder.DropTable(
        //        name: "OrganisationLeader");

        //    migrationBuilder.DropTable(
        //        name: "OrganisationRelationship");

        //    migrationBuilder.DropTable(
        //        name: "PrefixUsage");

        //    migrationBuilder.DropTable(
        //        name: "QuranCommentLink");

        //    migrationBuilder.DropTable(
        //        name: "RootUsage");

        //    migrationBuilder.DropTable(
        //        name: "SuggestionComment");

        //    migrationBuilder.DropTable(
        //        name: "SuggestionVote");

        //    migrationBuilder.DropTable(
        //        name: "UserBirthLocation");

        //    migrationBuilder.DropTable(
        //        name: "UserEmail");

        //    migrationBuilder.DropTable(
        //        name: "UserFathersFirstName");

        //    migrationBuilder.DropTable(
        //        name: "UserFirstName");

        //    migrationBuilder.DropTable(
        //        name: "UserMobilePhone");

        //    migrationBuilder.DropTable(
        //        name: "UserPhotograph");

        //    migrationBuilder.DropTable(
        //        name: "VerseTranslation");

        //    migrationBuilder.DropTable(
        //        name: "Action");

        //    migrationBuilder.DropTable(
        //        name: "MembershipApplication");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleComprehensionQuestion");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleViolationAccusation");

        //    migrationBuilder.DropTable(
        //        name: "Prefix");

        //    migrationBuilder.DropTable(
        //        name: "QuranComment");

        //    migrationBuilder.DropTable(
        //        name: "Root");

        //    migrationBuilder.DropTable(
        //        name: "WordPart");

        //    migrationBuilder.DropTable(
        //        name: "Suggestion");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRule");

        //    migrationBuilder.DropTable(
        //        name: "Auth0User");

        //    migrationBuilder.DropTable(
        //        name: "WordPartForm");

        //    migrationBuilder.DropTable(
        //        name: "WordPartPositionType");

        //    migrationBuilder.DropTable(
        //        name: "WordPartType");

        //    migrationBuilder.DropTable(
        //        name: "Word");

        //    migrationBuilder.DropTable(
        //        name: "Member");

        //    migrationBuilder.DropTable(
        //        name: "MembershipRuleSection");

        //    migrationBuilder.DropTable(
        //        name: "Verse");

        //    migrationBuilder.DropTable(
        //        name: "Humans");

        //    migrationBuilder.DropTable(
        //        name: "ShurahBasedOrganisation");

        //    migrationBuilder.DropTable(
        //        name: "Surah");
        }
    }
}
