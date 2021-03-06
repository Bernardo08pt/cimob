﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cimob.Models;

namespace cimob.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Corresponde a cada uma das tabelas da DB. É através dos DBSets que conseguimos 
        /// manipular a base de dados
        /// </summary>
        public DbSet<Ajuda> Ajudas { get; set; }
        public DbSet<Erro> Erros { get; set; }
        public DbSet<TipoMobilidade> TiposMobilidade { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<IpsEscola> IpsEscolas { get; set; }
        public DbSet<IpsCurso> IpsCursos { get; set; }
        public DbSet<Pais> Paises { get; set; } 
        public DbSet<Parentesco> Parentescos { get; set; }
        public DbSet<EstadoCandidatura> EstadosCandidatura { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }
        public DbSet<CandidaturaCursos> CandidaturaCursos { get; set; }
        public DbSet<CandidaturaDocumentos> CandidaturaDocumentos { get; set; }
        public DbSet<UCAvaliacao> UCAvaliacao { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Edital> Editais { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
