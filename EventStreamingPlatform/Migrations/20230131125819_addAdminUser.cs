using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStreamingPlatform.Migrations
{
    public partial class addAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'ae49e209-fb7b-4f85-8bcd-a4fd83ea2b84', N'admin', N'ADMIN', N'admin@test.com', N'ADMIN@TEST.COM', 0, N'AQAAAAEAACcQAAAAEAWH/eLXv3ucFRs/Tpb1+bsXh5NHCidhn+QQotrYOmaUUnI72vKLagO4ojuwg5Dkng==', N'ZKHQLKZMOM3JJXOJ2ELXDLOPYBPLXGI5', N'4b72312a-29db-40a9-9eb6-58f5e39c9a84', NULL, 0, 0, NULL, 1, 0, N'Admin', N'Admin', NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id = 'ae49e209-fb7b-4f85-8bcd-a4fd83ea2b84'");
        }
    }
}
