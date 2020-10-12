using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoPlatform.DAL.Migrations
{
    public partial class CreateUserRolesView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string script = @"
                IF OBJECT_ID ('[dbo].[UserRolesView]') IS NOT NULL
	                DROP VIEW [dbo].[UserRolesView]
                GO
                CREATE VIEW [dbo].[UserRolesView]
                AS
	                SELECT AspNetUsers.UserName, AspNetRoles.Name AS RoleName 
		                FROM AspNetUsers INNER JOIN
			                 AspNetUserRoles ON AspNetUsers.Id = AspNetUserRoles.UserId INNER JOIN
                             AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id
                GO";

            migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            const string script = @"
                IF OBJECT_ID ('[dbo].[UserRolesView]') IS NOT NULL
	                DROP VIEW [dbo].[UserRolesView]
                GO";

            migrationBuilder.Sql(script);
        }
    }
}