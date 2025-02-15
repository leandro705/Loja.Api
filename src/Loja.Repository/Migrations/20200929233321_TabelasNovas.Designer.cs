﻿// <auto-generated />
using System;
using Loja.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Loja.Repository.Migrations
{
    [DbContext(typeof(LojaDbContext))]
    [Migration("20200929233321_TabelasNovas")]
    partial class TabelasNovas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Loja.Domain.Entities.Agendamento", b =>
                {
                    b.Property<int>("AgendamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAgendamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataFinalAgendamento")
                        .HasColumnType("datetime2");

                    b.Property<int>("EstabelecimentoId")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("varchar(500)");

                    b.Property<int>("ServicoId")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AgendamentoId");

                    b.HasIndex("EstabelecimentoId");

                    b.HasIndex("ServicoId");

                    b.HasIndex("UserId");

                    b.ToTable("Agendamento");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Atendimento", b =>
                {
                    b.Property<int>("AtendimentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAtendimento")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("EstabelecimentoId")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("varchar(500)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("AtendimentoId");

                    b.HasIndex("EstabelecimentoId");

                    b.HasIndex("UserId");

                    b.ToTable("Atendimento");
                });

            modelBuilder.Entity("Loja.Domain.Entities.AtendimentoItem", b =>
                {
                    b.Property<int>("AtendimentoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AtendimentoId")
                        .HasColumnType("int");

                    b.Property<int>("ServicoId")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("AtendimentoItemId");

                    b.HasIndex("AtendimentoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("AtendimentoItem");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Endereco", b =>
                {
                    b.Property<int>("EnderecoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("EnderecoId");

                    b.HasIndex("MunicipioId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Estabelecimento", b =>
                {
                    b.Property<int>("EstabelecimentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Facebook")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Instagran")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("EstabelecimentoId");

                    b.ToTable("Estabelecimento");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Estado", b =>
                {
                    b.Property<int>("EstadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.HasKey("EstadoId");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Municipio", b =>
                {
                    b.Property<int>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.HasKey("MunicipioId");

                    b.HasIndex("EstadoId");

                    b.ToTable("Municipio");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Servico", b =>
                {
                    b.Property<int>("ServicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ServicoId");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("Loja.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Celular")
                        .HasColumnType("varchar(12)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(256);

                    b.Property<int?>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFacebook")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGoogle")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(12)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Loja.Domain.Entities.UserEstabelecimento", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EstabelecimentoId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "EstabelecimentoId");

                    b.HasIndex("EstabelecimentoId");

                    b.ToTable("UserEstabelecimento");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Agendamento", b =>
                {
                    b.HasOne("Loja.Domain.Entities.Estabelecimento", "Estabelecimento")
                        .WithMany()
                        .HasForeignKey("EstabelecimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Loja.Domain.Entities.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Loja.Domain.Entities.User", "Usuario")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Loja.Domain.Entities.Atendimento", b =>
                {
                    b.HasOne("Loja.Domain.Entities.Estabelecimento", "Estabelecimento")
                        .WithMany()
                        .HasForeignKey("EstabelecimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Loja.Domain.Entities.User", "Usuario")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Loja.Domain.Entities.AtendimentoItem", b =>
                {
                    b.HasOne("Loja.Domain.Entities.Atendimento", null)
                        .WithMany("AtendimentoItens")
                        .HasForeignKey("AtendimentoId");

                    b.HasOne("Loja.Domain.Entities.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Loja.Domain.Entities.Endereco", b =>
                {
                    b.HasOne("Loja.Domain.Entities.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Loja.Domain.Entities.Municipio", b =>
                {
                    b.HasOne("Loja.Domain.Entities.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Loja.Domain.Entities.User", b =>
                {
                    b.HasOne("Loja.Domain.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");
                });

            modelBuilder.Entity("Loja.Domain.Entities.UserEstabelecimento", b =>
                {
                    b.HasOne("Loja.Domain.Entities.Estabelecimento", "Estabelecimento")
                        .WithMany("UserEstabelecimentos")
                        .HasForeignKey("EstabelecimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Loja.Domain.Entities.User", "User")
                        .WithMany("UserEstabelecimentos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Loja.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Loja.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Loja.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Loja.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
