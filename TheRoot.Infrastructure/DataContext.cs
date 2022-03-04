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
        public DbSet<AllianseBase> AllianseBases { get; set; }
        public DbSet<PiecesContainer<Warrior>> WarriorsContainers { get; set; }
        public DbSet<PiecesContainer<Token>> TokensContainers { get; set; }
        public DbSet<PiecesContainer<Building>> BuildingsContainers { get; set; }
        public DbSet<PiecesContainer<Card>> CardsContainers { get; set; }
        public DbSet<PiecesContainer<Item>> ItemsContainers { get; set; }

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
            modelBuilder.ApplyConfiguration(new PiecesContainerConfiguration<Warrior>());
            modelBuilder.ApplyConfiguration(new PiecesContainerConfiguration<Token>());
            modelBuilder.ApplyConfiguration(new PiecesContainerConfiguration<Building>());
            modelBuilder.ApplyConfiguration(new PiecesContainerConfiguration<Card>());
            modelBuilder.ApplyConfiguration(new PiecesContainerConfiguration<Item>());
            modelBuilder.ApplyConfiguration(new AllianseBaseConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class AllianseBaseConfiguration : IEntityTypeConfiguration<AllianseBase>
    {
        public void Configure(EntityTypeBuilder<AllianseBase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ClearingType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
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
            builder.HasOne(x => x.RuinItemsContainer)
                .WithOne()
                .HasForeignKey<Ruin>("RuinItemsContainerId")
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
            builder.HasOne(x => x.DeckCardsContainer)
                .WithOne()
                .HasForeignKey<Game>("DeckCardsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.DiscardCardsContainer)
                .WithOne()
                .HasForeignKey<Game>("DiscardCardsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.CraftableItemsContainer)
                .WithOne()
                .HasForeignKey<Game>("CraftableItemsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Factions)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Clearings)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Forests)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ClearingsPaths)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ClearingsRiverPaths)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ClearingForestPaths)
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
            builder.HasOne(x => x.FactionCardsContainer)
                .WithOne()
                .HasForeignKey<Faction>("FactionCardsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.FactionItemsContainer)
                .WithOne()
                .HasForeignKey<Faction>("FactionItemsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class PiecesContainerConfiguration<T> : IEntityTypeConfiguration<PiecesContainer<T>> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<PiecesContainer<T>> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Pieces)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class MarquiseFactionConfiguration : IEntityTypeConfiguration<MarquiseFaction>
    {
        public void Configure(EntityTypeBuilder<MarquiseFaction> builder)
        {
            builder.ToTable("MarquiseFactions");
            builder.HasOne(x => x.TokensContainer)
                .WithOne()
                .HasForeignKey<MarquiseFaction>("MarquiseTokensContainerId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.BuildingsContainer)
                .WithOne()
                .HasForeignKey<MarquiseFaction>("MarquiseBuildingsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarriorsContainer)
                .WithOne()
                .HasForeignKey<MarquiseFaction>("MarquiseWarriorsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class EyrieFactionConfiguration : IEntityTypeConfiguration<EyrieFaction>
    {
        public void Configure(EntityTypeBuilder<EyrieFaction> builder)
        {
            builder.ToTable("EyrieFactions");
            builder.HasOne(x => x.BuildingsContainer)
               .WithOne()
               .HasForeignKey<EyrieFaction>("EyrieBuildingsContainerId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarriorsContainer)
                .WithOne()
                .HasForeignKey<EyrieFaction>("EyrieWarriorsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class AllianceFactionConfiguration : IEntityTypeConfiguration<AllianceFaction>
    {
        public void Configure(EntityTypeBuilder<AllianceFaction> builder)
        {
            builder.ToTable("AllianceFactions");
            builder.HasOne(x => x.TokensContainer)
               .WithOne()
               .HasForeignKey<AllianceFaction>("AllianceTokensContainerId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.BuildingsContainer)
               .WithOne()
               .HasForeignKey<AllianceFaction>("AllianceBuildingsContainerId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarriorsContainer)
                .WithOne()
                .HasForeignKey<AllianceFaction>("AllianceWarriorsContainerId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.OfficersContainer)
                .WithOne()
                .HasForeignKey<AllianceFaction>("AllianceOfficersContainerId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.SupportCardsContainer)
                .WithOne()
                .HasForeignKey<AllianceFaction>("SupportCardsContainerId")
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
            builder.HasOne(x => x.TokensContainer)
               .WithOne()
               .HasForeignKey<Clearing>("ClearingTokensContainerId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.BuildingsContainer)
               .WithOne()
               .HasForeignKey<Clearing>("ClearingBuildingsContainerId")
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.WarriorsContainer)
                .WithOne()
                .HasForeignKey<Clearing>("ClearingWarriorsContainerId")
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
