﻿// <auto-generated />
using cimob.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace cimob.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180116190819_update-candidturas-estados")]
    partial class updatecandidturasestados
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("cimob.Models.Ajuda", b =>
                {
                    b.Property<int>("AjudaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Corpo");

                    b.Property<string>("Nome");

                    b.Property<string>("Pagina");

                    b.Property<string>("Titulo");

                    b.HasKey("AjudaID");

                    b.ToTable("Ajudas");
                });

            modelBuilder.Entity("cimob.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nome");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<int>("Numero");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("cimob.Models.Candidatura", b =>
                {
                    b.Property<int>("CandidaturaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnoLetivo");

                    b.Property<string>("ContactoPessoal");

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("EmailAlternativo");

                    b.Property<int>("EmegerenciaParentescoID");

                    b.Property<string>("EmergenciaContacto");

                    b.Property<string>("Entrevista");

                    b.Property<int>("EstadoCandidaturaID");

                    b.Property<short>("Estagio");

                    b.Property<int>("IpsCursoID");

                    b.Property<string>("Observacoes");

                    b.Property<int>("Pontuacao");

                    b.Property<string>("RejeicaoRazao");

                    b.Property<int>("Rejeitada");

                    b.Property<short>("Semestre");

                    b.Property<int>("TipoMobilidadeID");

                    b.Property<string>("UtilizadorID");

                    b.HasKey("CandidaturaID");

                    b.HasIndex("EmegerenciaParentescoID");

                    b.HasIndex("EstadoCandidaturaID");

                    b.HasIndex("IpsCursoID");

                    b.HasIndex("TipoMobilidadeID");

                    b.HasIndex("UtilizadorID");

                    b.ToTable("Candidaturas");
                });

            modelBuilder.Entity("cimob.Models.CandidaturaCursos", b =>
                {
                    b.Property<int>("CandidaturaCursosID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CandidaturaID");

                    b.Property<int>("CursoID");

                    b.HasKey("CandidaturaCursosID");

                    b.HasIndex("CandidaturaID");

                    b.HasIndex("CursoID");

                    b.ToTable("CandidaturaCursos");
                });

            modelBuilder.Entity("cimob.Models.CandidaturaDocumentos", b =>
                {
                    b.Property<int>("CandidaturaDocumentosID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CandidaturaID");

                    b.Property<int>("DocumentoID");

                    b.HasKey("CandidaturaDocumentosID");

                    b.HasIndex("CandidaturaID");

                    b.HasIndex("DocumentoID");

                    b.ToTable("CandidaturaDocumentos");
                });

            modelBuilder.Entity("cimob.Models.Curso", b =>
                {
                    b.Property<int>("CursoID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EscolaID");

                    b.Property<string>("Nome");

                    b.Property<int>("PaisID");

                    b.Property<int>("Vagas");

                    b.HasKey("CursoID");

                    b.HasIndex("EscolaID");

                    b.HasIndex("PaisID");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("cimob.Models.Documento", b =>
                {
                    b.Property<int>("DocumentoID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataUpload");

                    b.Property<string>("FicheiroCaminho");

                    b.Property<string>("FicheiroNome");

                    b.Property<int>("OrigemCimob");

                    b.HasKey("DocumentoID");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("cimob.Models.Edital", b =>
                {
                    b.Property<int>("EditalID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caminho");

                    b.Property<DateTime>("DataLimite");

                    b.Property<int>("Estado");

                    b.Property<string>("Nome");

                    b.Property<string>("NomeFicheiro");

                    b.Property<int>("TipoMobilidadeID");

                    b.HasKey("EditalID");

                    b.HasIndex("TipoMobilidadeID");

                    b.ToTable("Editais");
                });

            modelBuilder.Entity("cimob.Models.Erro", b =>
                {
                    b.Property<int>("ErroID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Mensagem");

                    b.Property<string>("Nome");

                    b.HasKey("ErroID");

                    b.ToTable("Erros");
                });

            modelBuilder.Entity("cimob.Models.Escola", b =>
                {
                    b.Property<int>("EscolaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<int>("Estado");

                    b.Property<string>("Nome");

                    b.Property<int>("PaisID");

                    b.Property<int>("TipoMobilidadeID");

                    b.HasKey("EscolaID");

                    b.HasIndex("PaisID");

                    b.HasIndex("TipoMobilidadeID");

                    b.ToTable("Escolas");
                });

            modelBuilder.Entity("cimob.Models.EstadoCandidatura", b =>
                {
                    b.Property<int>("EstadoCandidaturaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cor");

                    b.Property<string>("Descricao");

                    b.Property<string>("Icon");

                    b.HasKey("EstadoCandidaturaID");

                    b.ToTable("EstadosCandidatura");
                });

            modelBuilder.Entity("cimob.Models.IpsCurso", b =>
                {
                    b.Property<int>("IpsCursoID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IpsEscolaID");

                    b.Property<string>("Nome");

                    b.HasKey("IpsCursoID");

                    b.HasIndex("IpsEscolaID");

                    b.ToTable("IpsCursos");
                });

            modelBuilder.Entity("cimob.Models.IpsEscola", b =>
                {
                    b.Property<int>("IpsEscolaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.HasKey("IpsEscolaID");

                    b.ToTable("IpsEscolas");
                });

            modelBuilder.Entity("cimob.Models.Pais", b =>
                {
                    b.Property<int>("PaisID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.HasKey("PaisID");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("cimob.Models.Parentesco", b =>
                {
                    b.Property<int>("ParentescoID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.HasKey("ParentescoID");

                    b.ToTable("Parentescos");
                });

            modelBuilder.Entity("cimob.Models.TipoMobilidade", b =>
                {
                    b.Property<int>("TipoMobilidadeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.Property<int>("Estagio");

                    b.HasKey("TipoMobilidadeID");

                    b.ToTable("TiposMobilidade");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
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
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("cimob.Models.Candidatura", b =>
                {
                    b.HasOne("cimob.Models.Parentesco", "EmegerenciaParentesco")
                        .WithMany()
                        .HasForeignKey("EmegerenciaParentescoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.EstadoCandidatura", "EstadoCandidatura")
                        .WithMany()
                        .HasForeignKey("EstadoCandidaturaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.IpsCurso", "IpsCurso")
                        .WithMany()
                        .HasForeignKey("IpsCursoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.TipoMobilidade", "TipoMobilidade")
                        .WithMany()
                        .HasForeignKey("TipoMobilidadeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.ApplicationUser", "Utilizador")
                        .WithMany()
                        .HasForeignKey("UtilizadorID");
                });

            modelBuilder.Entity("cimob.Models.CandidaturaCursos", b =>
                {
                    b.HasOne("cimob.Models.Candidatura", "Candidatura")
                        .WithMany("Cursos")
                        .HasForeignKey("CandidaturaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("cimob.Models.CandidaturaDocumentos", b =>
                {
                    b.HasOne("cimob.Models.Candidatura", "Candidatura")
                        .WithMany("Documentos")
                        .HasForeignKey("CandidaturaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.Documento", "Documento")
                        .WithMany()
                        .HasForeignKey("DocumentoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("cimob.Models.Curso", b =>
                {
                    b.HasOne("cimob.Models.Escola", "Escola")
                        .WithMany("Cursos")
                        .HasForeignKey("EscolaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("cimob.Models.Edital", b =>
                {
                    b.HasOne("cimob.Models.TipoMobilidade", "TipoMobilidade")
                        .WithMany()
                        .HasForeignKey("TipoMobilidadeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("cimob.Models.Escola", b =>
                {
                    b.HasOne("cimob.Models.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.TipoMobilidade", "TipoMobilidade")
                        .WithMany()
                        .HasForeignKey("TipoMobilidadeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("cimob.Models.IpsCurso", b =>
                {
                    b.HasOne("cimob.Models.IpsEscola", "IpsEscola")
                        .WithMany("Cursos")
                        .HasForeignKey("IpsEscolaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("cimob.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("cimob.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cimob.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("cimob.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
