using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoPlatform.DAL.Migrations
{
    public partial class SPGetUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string storedProcedure = @"
                IF OBJECT_ID ('[dbo].[GetUserRoles]') IS NOT NULL
                    DROP PROCEDURE [dbo].[GetUserRoles]
                GO
                CREATE PROCEDURE [dbo].[GetUserRoles]
                AS
                    BEGIN
                        SELECT AspNetUsers.UserName, AspNetRoles.Name AS RoleName 
	                        FROM AspNetUsers INNER JOIN
                                 AspNetUserRoles ON AspNetUsers.Id = AspNetUserRoles.UserId INNER JOIN
                                 AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id
                    END
                GO";

            migrationBuilder.Sql(storedProcedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            const string storedProcedure = @"
                IF OBJECT_ID ('[dbo].[GetUserRoles]') IS NOT NULL
                    DROP PROCEDURE [dbo].[GetUserRoles]
                GO";

            migrationBuilder.Sql(storedProcedure);
        }
    }
}