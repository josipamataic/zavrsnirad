using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test10.Models;

namespace Test10.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Oglas> Oglas { get; set; }
        public virtual DbSet<Poruka> Poruka { get; set; }
        public virtual DbSet<Rezervacija> Rezervacija { get; set; }
        public virtual DbSet<TeniskiKlub> TeniskiKlub { get; set; }
        public virtual DbSet<Teren> Teren { get; set; }
        public virtual DbSet<Podloga> Podloga{ get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.Ime).HasMaxLength(50).IsRequired();
                b.Property(u => u.Prezime).HasMaxLength(50).IsRequired();
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.Email).IsRequired();
            });

            builder.Entity<Oglas>(entity =>
            {
                entity.HasKey(e => e.IdOglas);

                entity.Property(e => e.Datum);

                entity.Property(e => e.Opis).HasMaxLength(140).IsRequired();

                entity.Property(e => e.NazivOglas).HasMaxLength(80).IsRequired();

                entity.HasOne(d => d.Igrac)
                    .WithMany(p => p.Oglas)
                    .HasForeignKey(d => d.IgracId);

                entity.HasOne(d => d.TeniskiKlub)
                    .WithMany(p => p.Oglas)
                    .HasForeignKey(d => d.TeniskiKlubId);
                    
            });

            builder.Entity<Poruka>(entity =>
            {
                entity.HasKey(e => e.IdPoruka);

                entity.Property(e => e.TekstPoruke)
                    .IsRequired()
                    .HasMaxLength(280);

                entity.Property(e => e.NaslovPoruke)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Vrijeme);

                entity.HasOne(d => d.IgracPosiljatelj)
                    .WithMany(p => p.PorukaIgracPosiljatelj)
                    .HasForeignKey(d => d.IgracPosiljateljId);

                entity.HasOne(d => d.IgracPrimatelj)
                    .WithMany(p => p.PorukaIgracPrimatelj)
                    .HasForeignKey(d => d.IgracPrimateljId);
                  
            });

            builder.Entity<Rezervacija>(entity =>
            {
                entity.HasKey(e => e.IdRezervacija);

                entity.Property(e => e.DatumVrijeme).HasColumnType("datetime").IsRequired();

                entity.Property(e => e.Kraj).HasColumnType("datetime").IsRequired();


                entity.HasOne(d => d.Upravitelj)
                    .WithMany(p => p.RezervacijaUpravitelj)
                    .HasForeignKey(d => d.UpraviteljId);

                entity.HasOne(d => d.Teren)
                    .WithMany(p => p.Rezervacija)
                    .HasForeignKey(d => d.TerenId);                  

                entity.HasOne(d => d.Igrac)
                    .WithMany(p => p.RezervacijaIgrac)
                    .HasForeignKey(d => d.IgracId);
                    
            });

            builder.Entity<TeniskiKlub>(entity =>
            {
                entity.HasKey(e => e.IdTeniskiKlub);

                entity.Property(e => e.Adresa).HasMaxLength(50);

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BrojTerena).IsRequired();

                entity.HasOne(d => d.Upravitelj)
                    .WithMany(p => p.TeniskiKlub)
                    .HasForeignKey(d => d.UpraviteljId);                    
            });

            builder.Entity<Teren>(entity =>
            {
                entity.HasKey(e => e.IdTeren);

                entity.Property(e => e.Prostor).HasMaxLength(50);

                entity.Property(e => e.NazivTerena).HasMaxLength(50);

                entity.HasOne(d => d.TeniskiKlub)
                    .WithMany(p => p.Teren)
                    .HasForeignKey(d => d.TeniskiKlubId);

                entity.HasOne(d => d.Podloga)
                    .WithMany(p => p.Teren)
                    .HasForeignKey(d => d.PodlogaId);
            });

            builder.Entity<Podloga>(entity =>
            {
                entity.HasKey(e => e.IdPodloga);
                entity.Property(e => e.NazivPodloga).HasMaxLength(50);

            });
        }
    }
}
