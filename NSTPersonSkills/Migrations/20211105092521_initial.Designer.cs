// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NSTPersonSkills.Models;

namespace NSTPersonSkills.Migrations
{
    [DbContext(typeof(CompanyContext))]
    [Migration("20211105092521_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NSTPersonSkills.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("NSTPersonSkills.Models.Skill", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<byte>("Level")
                        .HasColumnType("tinyint");

                    b.HasKey("Name", "PersonId")
                        .HasName("PK_Skill_1");

                    b.HasIndex("PersonId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("NSTPersonSkills.Models.Skill", b =>
                {
                    b.HasOne("NSTPersonSkills.Models.Person", "Person")
                        .WithMany("Skills")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK_Skill_Person")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("NSTPersonSkills.Models.Person", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
