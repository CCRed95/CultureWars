using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CultureWars.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wp_commentmeta",
                columns: table => new
                {
                    meta_id = table.Column<decimal>(nullable: false),
                    comment_id = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    meta_key = table.Column<string>(type: "varchar(255)", nullable: true),
                    meta_value = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_commentmeta", x => x.meta_id);
                });

            migrationBuilder.CreateTable(
                name: "wp_comments",
                columns: table => new
                {
                    comment_ID = table.Column<decimal>(nullable: false),
                    comment_post_ID = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    comment_author = table.Column<string>(type: "varchar(255)", nullable: false),
                    comment_author_email = table.Column<string>(type: "varchar(100)", nullable: false, defaultValueSql: "''"),
                    comment_author_url = table.Column<string>(type: "varchar(200)", nullable: false, defaultValueSql: "''"),
                    comment_author_IP = table.Column<string>(type: "varchar(100)", nullable: false, defaultValueSql: "''"),
                    comment_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'0000-00-00 00:00:00'"),
                    comment_date_gmt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'0000-00-00 00:00:00'"),
                    comment_content = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    comment_karma = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'0'"),
                    comment_approved = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "'1'"),
                    comment_agent = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    comment_type = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "''"),
                    comment_parent = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    user_id = table.Column<decimal>(nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_comments", x => x.comment_ID);
                });

            migrationBuilder.CreateTable(
                name: "wp_links",
                columns: table => new
                {
                    link_id = table.Column<decimal>(nullable: false),
                    link_url = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    link_name = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    link_image = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    link_target = table.Column<string>(type: "varchar(25)", nullable: false, defaultValueSql: "''"),
                    link_description = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    link_visible = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "'Y'"),
                    link_owner = table.Column<decimal>(nullable: false, defaultValueSql: "'1'"),
                    link_rating = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'0'"),
                    link_updated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'0000-00-00 00:00:00'"),
                    link_rel = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    link_notes = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    link_rss = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_links", x => x.link_id);
                });

            migrationBuilder.CreateTable(
                name: "wp_options",
                columns: table => new
                {
                    option_id = table.Column<decimal>(nullable: false),
                    option_name = table.Column<string>(type: "varchar(191)", nullable: false, defaultValueSql: "''"),
                    option_value = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    autoload = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "'yes'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_options", x => x.option_id);
                });

            migrationBuilder.CreateTable(
                name: "wp_postmeta",
                columns: table => new
                {
                    meta_id = table.Column<decimal>(nullable: false),
                    post_id = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    meta_key = table.Column<string>(type: "varchar(255)", nullable: true),
                    meta_value = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_postmeta", x => x.meta_id);
                });

            migrationBuilder.CreateTable(
                name: "wp_posts",
                columns: table => new
                {
                    ID = table.Column<decimal>(nullable: false),
                    post_author = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    post_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'0000-00-00 00:00:00'"),
                    post_date_gmt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'0000-00-00 00:00:00'"),
                    post_content = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    post_title = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    post_excerpt = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    post_status = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "'publish'"),
                    comment_status = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "'open'"),
                    ping_status = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "'open'"),
                    post_password = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    post_name = table.Column<string>(type: "varchar(200)", nullable: false, defaultValueSql: "''"),
                    to_ping = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    pinged = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    post_modified = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'0000-00-00 00:00:00'"),
                    post_modified_gmt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'0000-00-00 00:00:00'"),
                    post_content_filtered = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    post_parent = table.Column<decimal>(nullable: true, defaultValueSql: "'0'"),
                    guid = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    menu_order = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'0'"),
                    post_type = table.Column<string>(type: "varchar(20)", nullable: false, defaultValueSql: "'post'"),
                    post_mime_type = table.Column<string>(type: "varchar(100)", nullable: false, defaultValueSql: "''"),
                    comment_count = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_posts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "wp_term_relationships",
                columns: table => new
                {
                    object_id = table.Column<decimal>(nullable: false),
                    term_taxonomy_id = table.Column<decimal>(nullable: false),
                    term_order = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_term_relationships", x => new { x.object_id, x.term_taxonomy_id });
                });

            migrationBuilder.CreateTable(
                name: "wp_term_taxonomy",
                columns: table => new
                {
                    term_taxonomy_id = table.Column<decimal>(nullable: false),
                    term_id = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    taxonomy = table.Column<string>(type: "varchar(32)", nullable: false, defaultValueSql: "''"),
                    description = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    parent = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    count = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_term_taxonomy", x => x.term_taxonomy_id);
                });

            migrationBuilder.CreateTable(
                name: "wp_termmeta",
                columns: table => new
                {
                    meta_id = table.Column<decimal>(nullable: false),
                    term_id = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    meta_key = table.Column<string>(type: "varchar(255)", nullable: true),
                    meta_value = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_termmeta", x => x.meta_id);
                });

            migrationBuilder.CreateTable(
                name: "wp_terms",
                columns: table => new
                {
                    term_id = table.Column<decimal>(nullable: false),
                    name = table.Column<string>(type: "varchar(200)", nullable: false, defaultValueSql: "''"),
                    slug = table.Column<string>(type: "varchar(200)", nullable: false, defaultValueSql: "''"),
                    term_group = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_terms", x => x.term_id);
                });

            migrationBuilder.CreateTable(
                name: "wp_usermeta",
                columns: table => new
                {
                    umeta_id = table.Column<decimal>(nullable: false),
                    user_id = table.Column<decimal>(nullable: false, defaultValueSql: "'0'"),
                    meta_key = table.Column<string>(type: "varchar(255)", nullable: true),
                    meta_value = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_usermeta", x => x.umeta_id);
                });

            migrationBuilder.CreateTable(
                name: "wp_users",
                columns: table => new
                {
                    ID = table.Column<decimal>(nullable: false),
                    user_login = table.Column<string>(type: "varchar(60)", nullable: false, defaultValueSql: "''"),
                    user_pass = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    user_nicename = table.Column<string>(type: "varchar(50)", nullable: false, defaultValueSql: "''"),
                    user_email = table.Column<string>(type: "varchar(100)", nullable: false, defaultValueSql: "''"),
                    user_url = table.Column<string>(type: "varchar(100)", nullable: false, defaultValueSql: "''"),
                    user_registered = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'0000-00-00 00:00:00'"),
                    user_activation_key = table.Column<string>(type: "varchar(255)", nullable: false, defaultValueSql: "''"),
                    user_status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'0'"),
                    display_name = table.Column<string>(type: "varchar(250)", nullable: false, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wp_users", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "comment_id",
                table: "wp_commentmeta",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "meta_key",
                table: "wp_commentmeta",
                column: "meta_key");

            migrationBuilder.CreateIndex(
                name: "comment_author_email",
                table: "wp_comments",
                column: "comment_author_email");

            migrationBuilder.CreateIndex(
                name: "comment_date_gmt",
                table: "wp_comments",
                column: "comment_date_gmt");

            migrationBuilder.CreateIndex(
                name: "comment_parent",
                table: "wp_comments",
                column: "comment_parent");

            migrationBuilder.CreateIndex(
                name: "comment_post_ID",
                table: "wp_comments",
                column: "comment_post_ID");

            migrationBuilder.CreateIndex(
                name: "comment_approved_date_gmt",
                table: "wp_comments",
                columns: new[] { "comment_approved", "comment_date_gmt" });

            migrationBuilder.CreateIndex(
                name: "link_visible",
                table: "wp_links",
                column: "link_visible");

            migrationBuilder.CreateIndex(
                name: "option_name",
                table: "wp_options",
                column: "option_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "meta_key",
                table: "wp_postmeta",
                column: "meta_key");

            migrationBuilder.CreateIndex(
                name: "post_id",
                table: "wp_postmeta",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "post_author",
                table: "wp_posts",
                column: "post_author");

            migrationBuilder.CreateIndex(
                name: "post_name",
                table: "wp_posts",
                column: "post_name");

            migrationBuilder.CreateIndex(
                name: "post_parent",
                table: "wp_posts",
                column: "post_parent");

            migrationBuilder.CreateIndex(
                name: "type_status_date",
                table: "wp_posts",
                columns: new[] { "post_type", "post_status", "post_date", "ID" });

            migrationBuilder.CreateIndex(
                name: "term_taxonomy_id",
                table: "wp_term_relationships",
                column: "term_taxonomy_id");

            migrationBuilder.CreateIndex(
                name: "taxonomy",
                table: "wp_term_taxonomy",
                column: "taxonomy");

            migrationBuilder.CreateIndex(
                name: "term_id_taxonomy",
                table: "wp_term_taxonomy",
                columns: new[] { "term_id", "taxonomy" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "meta_key",
                table: "wp_termmeta",
                column: "meta_key");

            migrationBuilder.CreateIndex(
                name: "term_id",
                table: "wp_termmeta",
                column: "term_id");

            migrationBuilder.CreateIndex(
                name: "name",
                table: "wp_terms",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "slug",
                table: "wp_terms",
                column: "slug");

            migrationBuilder.CreateIndex(
                name: "meta_key",
                table: "wp_usermeta",
                column: "meta_key");

            migrationBuilder.CreateIndex(
                name: "user_id",
                table: "wp_usermeta",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "user_email",
                table: "wp_users",
                column: "user_email");

            migrationBuilder.CreateIndex(
                name: "user_login_key",
                table: "wp_users",
                column: "user_login");

            migrationBuilder.CreateIndex(
                name: "user_nicename",
                table: "wp_users",
                column: "user_nicename");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wp_commentmeta");

            migrationBuilder.DropTable(
                name: "wp_comments");

            migrationBuilder.DropTable(
                name: "wp_links");

            migrationBuilder.DropTable(
                name: "wp_options");

            migrationBuilder.DropTable(
                name: "wp_postmeta");

            migrationBuilder.DropTable(
                name: "wp_posts");

            migrationBuilder.DropTable(
                name: "wp_term_relationships");

            migrationBuilder.DropTable(
                name: "wp_term_taxonomy");

            migrationBuilder.DropTable(
                name: "wp_termmeta");

            migrationBuilder.DropTable(
                name: "wp_terms");

            migrationBuilder.DropTable(
                name: "wp_usermeta");

            migrationBuilder.DropTable(
                name: "wp_users");
        }
    }
}
