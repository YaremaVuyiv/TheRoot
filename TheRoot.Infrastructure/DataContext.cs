using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheRoot.Domain.Entities;

namespace TheRoot.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Clearing> Clearings { get; set; }
        public DbSet<ClearingsPath> ClearingsPaths { get; set; }
        public DbSet<ClearingsRiverPath> ClearingsRiverPaths { get; set; }
        public DbSet<ClearingForestPath> ClearingForestPaths { get; set; }
        public DbSet<FactionType> FactionTypes { get; set; }
        public DbSet<ClearingType> ClearingTypes { get; set; }
        public DbSet<CardSuit> CardSuits { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Forest> Forests { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CostType> CostTypes { get; set; }
        public DbSet<Warrior> Warriors { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<MarquiseFaction> MarquiseFactions { get; set; }
        public DbSet<EyrieFaction> EyrieFactions { get; set; }
        public DbSet<AllianceFaction> AllianceFactions { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<CraftingPiece> CraftingPieces {get;set;}
        public DbSet<Ruin> Ruins { get; set; }
        public DbSet<WarriorsAgregate> WarriorsAgregates { get; set; }
        public DbSet<TokensAgregate> TokensAgregates { get; set; }
        public DbSet<BuildingsAgregate> BuildingsAgregates { get; set; }
        public DbSet<CardsAgregate> CardsAgregates { get; set; }
        public DbSet<ItemsAgregate> ItemsAgregates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-9J00O8T\SQLEXPRESS;Initial Catalog=TheRoot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClearingsPathConfiguration());
            modelBuilder.ApplyConfiguration(new ClearingsRiverPathConfiguration());
            modelBuilder.ApplyConfiguration(new ClearingForestPathConfiguration());
            modelBuilder.ApplyConfiguration(new FactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClearingTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CardSuitConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CostTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ForestConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new WarriorConfiguration());
            modelBuilder.ApplyConfiguration(new BuildingConfiguration());
            modelBuilder.ApplyConfiguration(new TokenConfiguration());
            modelBuilder.ApplyConfiguration(new FactionConfiguration());
            modelBuilder.ApplyConfiguration(new MarquiseFactionConfiguration());
            modelBuilder.ApplyConfiguration(new EyrieFactionConfiguration());
            modelBuilder.ApplyConfiguration(new AllianceFactionConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new ClearingConfiguration());
            modelBuilder.ApplyConfiguration(new CraftingPieceConfiguration());
            modelBuilder.ApplyConfiguration(new RuinConfiguration());
            modelBuilder.ApplyConfiguration(new WarriorsAgregateConfiguration());
            modelBuilder.ApplyConfiguration(new TokensAgregateConfiguration());
            modelBuilder.ApplyConfiguration(new BuildingsAgregateConfiguration());
            modelBuilder.ApplyConfiguration(new CardsAgregateConfiguration());
            modelBuilder.ApplyConfiguration(new ItemsAgregateConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class WarriorConfiguration : IEntityTypeConfiguration<Warrior>
    {
        public void Configure(EntityTypeBuilder<Warrior> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.FactionType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class RuinConfiguration : IEntityTypeConfiguration<Ruin>
    {
        public void Configure(EntityTypeBuilder<Ruin> builder)
        {
            builder.ToTable("Ruins");
            builder.HasOne(x => x.RuinItemsAgregate)
                .WithOne()
                .HasForeignKey<Ruin>("RuinItemsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class CraftingPieceConfiguration : IEntityTypeConfiguration<CraftingPiece>
    {
        public void Configure(EntityTypeBuilder<CraftingPiece> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ClearingType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property<int>("_factionId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("FactionId")
                .IsRequired(true);

            builder.HasOne<Faction>()
                .WithMany(x => x.CraftingPieces)
                .HasForeignKey("_factionId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.DeckCardsAgregate)
                .WithOne()
                .HasForeignKey<Game>("DeckCardsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.DiscardCardsAgregate)
                .WithOne()
                .HasForeignKey<Game>("DiscardCardsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.CraftableItemsAgregate)
                .WithOne()
                .HasForeignKey<Game>("CraftableItemsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Factions)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class FactionConfiguration : IEntityTypeConfiguration<Faction>
    {
        public void Configure(EntityTypeBuilder<Faction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.FactionType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.FactionCardsAgregate)
                .WithOne()
                .HasForeignKey<Faction>("FactionCardsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.FactionItemsAgregate)
                .WithOne()
                .HasForeignKey<Faction>("FactionItemsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class WarriorsAgregateConfiguration : IEntityTypeConfiguration<WarriorsAgregate>
    {
        public void Configure(EntityTypeBuilder<WarriorsAgregate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Warriors)
                .WithOne(x => x.WarriorsAgregate)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class TokensAgregateConfiguration : IEntityTypeConfiguration<TokensAgregate>
    {
        public void Configure(EntityTypeBuilder<TokensAgregate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Tokens)
                .WithOne(x => x.TokensAgregate)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class BuildingsAgregateConfiguration : IEntityTypeConfiguration<BuildingsAgregate>
    {
        public void Configure(EntityTypeBuilder<BuildingsAgregate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Buildings)
                .WithOne(x => x.BuildingsAgregate)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class CardsAgregateConfiguration : IEntityTypeConfiguration<CardsAgregate>
    {
        public void Configure(EntityTypeBuilder<CardsAgregate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Cards)
                .WithOne(x => x.CardsAgregate)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ItemsAgregateConfiguration : IEntityTypeConfiguration<ItemsAgregate>
    {
        public void Configure(EntityTypeBuilder<ItemsAgregate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Items)
                .WithOne(x => x.ItemsAgregate)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class MarquiseFactionConfiguration : IEntityTypeConfiguration<MarquiseFaction>
    {
        public void Configure(EntityTypeBuilder<MarquiseFaction> builder)
        {
            builder.ToTable("MarquiseFactions");
            builder.HasOne(x => x.TokensAgregate)
                .WithOne()
                .HasForeignKey<MarquiseFaction>("MarquiseTokensAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.BuildingsAgregate)
                .WithOne()
                .HasForeignKey<MarquiseFaction>("MarquiseBuildingsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarriorsAgregate)
                .WithOne()
                .HasForeignKey<MarquiseFaction>("MarquiseWarriorsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class EyrieFactionConfiguration : IEntityTypeConfiguration<EyrieFaction>
    {
        public void Configure(EntityTypeBuilder<EyrieFaction> builder)
        {
            builder.ToTable("EyrieFactions");
            builder.HasOne(x => x.BuildingsAgregate)
               .WithOne()
               .HasForeignKey<EyrieFaction>("EyrieBuildingsAgregateId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarriorsAgregate)
                .WithOne()
                .HasForeignKey<EyrieFaction>("EyrieWarriorsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class AllianceFactionConfiguration : IEntityTypeConfiguration<AllianceFaction>
    {
        public void Configure(EntityTypeBuilder<AllianceFaction> builder)
        {
            builder.ToTable("AllianceFactions");
            builder.HasOne(x => x.TokensAgregate)
               .WithOne()
               .HasForeignKey<AllianceFaction>("AllianceTokensAgregateId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.BuildingsAgregate)
               .WithOne()
               .HasForeignKey<AllianceFaction>("AllianceBuildingsAgregateId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarriorsAgregate)
                .WithOne()
                .HasForeignKey<AllianceFaction>("AllianceWarriorsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.OfficersAgregate)
                .WithOne()
                .HasForeignKey<AllianceFaction>("AllianceOfficersAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.SupportCardsAgregate)
                .WithOne()
                .HasForeignKey<AllianceFaction>("SupportCardsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SlotPiece)
                .HasColumnType("nvarchar(24)");
        }
    }

    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TokenType)
                .HasColumnType("nvarchar(24)");
        }
    }

    public class ClearingConfiguration : IEntityTypeConfiguration<Clearing>
    {
        public void Configure(EntityTypeBuilder<Clearing> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ClearingType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.TokensAgregate)
               .WithOne()
               .HasForeignKey<Clearing>("ClearingTokensAgregateId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.BuildingsAgregate)
               .WithOne()
               .HasForeignKey<Clearing>("ClearingBuildingsAgregateId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarriorsAgregate)
                .WithOne()
                .HasForeignKey<Clearing>("ClearingWarriorsAgregateId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ClearingTypeConfiguration : IEntityTypeConfiguration<ClearingType>
    {
        public void Configure(EntityTypeBuilder<ClearingType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(Domain.Enumeration.GetAll<ClearingType>());
        }
    }

    public class CardSuitConfiguration : IEntityTypeConfiguration<CardSuit>
    {
        public void Configure(EntityTypeBuilder<CardSuit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(Domain.Enumeration.GetAll<CardSuit>());
        }
    }

    public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(Domain.Enumeration.GetAll<ItemType>());
        }
    }

    public class FactionTypeConfiguration : IEntityTypeConfiguration<FactionType>
    {
        public void Configure(EntityTypeBuilder<FactionType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(Domain.Enumeration.GetAll<FactionType>());

            builder.HasMany<Warrior>()
                .WithOne(x => x.FactionType);
        }
    }

    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CardType)
                .HasColumnType("nvarchar(24)");

            builder.HasMany(x => x.Cost)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CardSuit)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class CostTypeConfiguration : IEntityTypeConfiguration<CostType>
    {
        public void Configure(EntityTypeBuilder<CostType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(Domain.Enumeration.GetAll<CostType>());
        }
    }

    public class ClearingsPathConfiguration : IEntityTypeConfiguration<ClearingsPath>
    {
        public void Configure(EntityTypeBuilder<ClearingsPath> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.FromClearing)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ToClearing)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ClearingsRiverPathConfiguration : IEntityTypeConfiguration<ClearingsRiverPath>
    {
        public void Configure(EntityTypeBuilder<ClearingsRiverPath> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.FromClearing)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ToClearing)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ClearingForestPathConfiguration : IEntityTypeConfiguration<ClearingForestPath>
    {
        public void Configure(EntityTypeBuilder<ClearingForestPath> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Clearing)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Forest)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ForestConfiguration : IEntityTypeConfiguration<Forest>
    {
        public void Configure(EntityTypeBuilder<Forest> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
