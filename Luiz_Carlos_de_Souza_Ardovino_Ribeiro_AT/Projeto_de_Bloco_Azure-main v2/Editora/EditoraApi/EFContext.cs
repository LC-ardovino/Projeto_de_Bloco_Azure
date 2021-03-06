using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EditoraBLL.Models;
using Microsoft.AspNetCore.Identity;

namespace EditoraAPI.Models
{
    public class EFContext : IdentityDbContext<IdentityUser>
    {

        private string connectionString;

        public EFContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }


        public DbSet<Autor>? Autores { get; set; }
        public DbSet<AutorAsset>? AutorAssetses { get; set; }
        public DbSet<Quadrinho>? Quadrinhos { get; set; }
        public DbSet<Perfil>? Perfis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Autor
            modelBuilder.Entity<Autor>()
                .ToTable("Autores")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Autor>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Autor>()
                .Property(p => p.Nome)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            modelBuilder.Entity<Autor>()
                .Property(p => p.SobreNome)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            modelBuilder.Entity<Autor>()
                .Property(p => p.DataNascimento)
                .HasColumnType("datetime2")
                .IsRequired();


            modelBuilder.Entity<Autor>()
                .HasOne(p => p.AutorAssets)
                .WithOne(p => p.Autor)
                .HasConstraintName("FK_Autor_AutorAssets")
                .HasForeignKey<AutorAsset>(b => b.AutorId);



            ////////Relacionamento AspNetUsers e quadrinho
            //////modelBuilder.Entity<AspNetUsers>()
            //////    .HasMany(p => p.Quadrinhos)
            //////    .WithOne(p => p.AspNetUsers)
            //////    .HasConstraintName("FK_Autor_Quadrinho");


            //Relacionamento Autor e quadrinho
            modelBuilder.Entity<Autor>()
                .HasMany(p => p.Quadrinhos)
                .WithOne(p => p.Autor)
                .HasConstraintName("FK_Autor_Quadrinho");


            //Quadrinho
            modelBuilder.Entity<AutorAsset>()
                .ToTable("AutorAssets")
                .HasKey(p => p.Id);

            modelBuilder.Entity<AutorAsset>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Quadrinho>()
                .ToTable("Quadrinhos")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Quadrinho>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Quadrinho>()
                .Property(p => p.Titulo)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            modelBuilder.Entity<Quadrinho>()
                .Property(p => p.Editora)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            modelBuilder.Entity<Quadrinho>()
               .Property(p => p.Ano)
               .HasColumnType("VARCHAR(4)")
               .IsRequired();




            //Perfil
            modelBuilder.Entity<Perfil>()
                .ToTable("Perfis")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Perfil>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Perfil>()
                .Property(p => p.Username)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            modelBuilder.Entity<Perfil>()
                .Property(p => p.Nome)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            modelBuilder.Entity<Perfil>()
                .Property(p => p.Sobrenome)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();

            modelBuilder.Entity<Perfil>()
               .Property(p => p.Telefone)
               .HasColumnType("VARCHAR(20)")
               .IsRequired();

            modelBuilder.Entity<Perfil>()
                .Property(p => p.DataNascimento)
                .HasColumnType("datetime2")
                .IsRequired();

        }

        //    public DbSet<EditoraWeb.Models.AmigoViewModel>? AmigoEmailViewModel { get; set; }

    }
}
