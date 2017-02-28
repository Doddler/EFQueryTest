using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EFTestProject.Data;

namespace EFTestProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFTestProject.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DeceasedDate");

                    b.Property<int>("Family");

                    b.Property<bool>("IsMember");

                    b.Property<int?>("LivesWithId");

                    b.HasKey("Id");

                    b.HasIndex("LivesWithId");

                    b.ToTable("Members");

                    b.HasAnnotation("SqlServer:TableName", "Members");
                });

            modelBuilder.Entity("EFTestProject.Models.Member", b =>
                {
                    b.HasOne("EFTestProject.Models.Member", "LivesWith")
                        .WithMany("LivingWith")
                        .HasForeignKey("LivesWithId");
                });
        }
    }
}
