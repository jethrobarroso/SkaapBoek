using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class EnableCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_grouped_herd_member_group_GroupId",
                table: "grouped_herd_member");

            migrationBuilder.DropForeignKey(
                name: "FK_grouped_herd_member_sheep_SheepId",
                table: "grouped_herd_member");

            migrationBuilder.DropForeignKey(
                name: "FK_mils_task_mils_phase_MilsPhaseId",
                table: "mils_task");

            migrationBuilder.DropForeignKey(
                name: "FK_sheep_color_ColorId",
                table: "sheep");

            migrationBuilder.DropForeignKey(
                name: "FK_sheep_gender_GenderId",
                table: "sheep");

            migrationBuilder.DropForeignKey(
                name: "FK_sheep_sheep_category_SheepCategoryId",
                table: "sheep");

            migrationBuilder.DropForeignKey(
                name: "FK_sheep_state_SheepStatusId",
                table: "sheep");

            migrationBuilder.DropForeignKey(
                name: "FK_task_instance_priority_PriorityId",
                table: "task_instance");

            migrationBuilder.DropForeignKey(
                name: "FK_task_instance_status_StatusId",
                table: "task_instance");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_grouped_herd_member_group_GroupId",
                table: "grouped_herd_member",
                column: "GroupId",
                principalTable: "group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_grouped_herd_member_sheep_SheepId",
                table: "grouped_herd_member",
                column: "SheepId",
                principalTable: "sheep",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mils_task_mils_phase_MilsPhaseId",
                table: "mils_task",
                column: "MilsPhaseId",
                principalTable: "mils_phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sheep_color_ColorId",
                table: "sheep",
                column: "ColorId",
                principalTable: "color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sheep_gender_GenderId",
                table: "sheep",
                column: "GenderId",
                principalTable: "gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sheep_sheep_category_SheepCategoryId",
                table: "sheep",
                column: "SheepCategoryId",
                principalTable: "sheep_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sheep_state_SheepStatusId",
                table: "sheep",
                column: "SheepStatusId",
                principalTable: "state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_instance_priority_PriorityId",
                table: "task_instance",
                column: "PriorityId",
                principalTable: "priority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_instance_status_StatusId",
                table: "task_instance",
                column: "StatusId",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_grouped_herd_member_group_GroupId",
                table: "grouped_herd_member");

            migrationBuilder.DropForeignKey(
                name: "FK_grouped_herd_member_sheep_SheepId",
                table: "grouped_herd_member");

            migrationBuilder.DropForeignKey(
                name: "FK_mils_task_mils_phase_MilsPhaseId",
                table: "mils_task");

            migrationBuilder.DropForeignKey(
                name: "FK_sheep_color_ColorId",
                table: "sheep");

            migrationBuilder.DropForeignKey(
                name: "FK_sheep_gender_GenderId",
                table: "sheep");

            migrationBuilder.DropForeignKey(
                name: "FK_sheep_sheep_category_SheepCategoryId",
                table: "sheep");

            migrationBuilder.DropForeignKey(
                name: "FK_sheep_state_SheepStatusId",
                table: "sheep");

            migrationBuilder.DropForeignKey(
                name: "FK_task_instance_priority_PriorityId",
                table: "task_instance");

            migrationBuilder.DropForeignKey(
                name: "FK_task_instance_status_StatusId",
                table: "task_instance");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_grouped_herd_member_group_GroupId",
                table: "grouped_herd_member",
                column: "GroupId",
                principalTable: "group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_grouped_herd_member_sheep_SheepId",
                table: "grouped_herd_member",
                column: "SheepId",
                principalTable: "sheep",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_mils_task_mils_phase_MilsPhaseId",
                table: "mils_task",
                column: "MilsPhaseId",
                principalTable: "mils_phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sheep_color_ColorId",
                table: "sheep",
                column: "ColorId",
                principalTable: "color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sheep_gender_GenderId",
                table: "sheep",
                column: "GenderId",
                principalTable: "gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sheep_sheep_category_SheepCategoryId",
                table: "sheep",
                column: "SheepCategoryId",
                principalTable: "sheep_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sheep_state_SheepStatusId",
                table: "sheep",
                column: "SheepStatusId",
                principalTable: "state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_task_instance_priority_PriorityId",
                table: "task_instance",
                column: "PriorityId",
                principalTable: "priority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_task_instance_status_StatusId",
                table: "task_instance",
                column: "StatusId",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
