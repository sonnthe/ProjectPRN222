using Microsoft.EntityFrameworkCore;

namespace ProjectPRN222.Models;

public partial class QuizOnlineContext : DbContext
{
    public QuizOnlineContext()
    {
    }

    public QuizOnlineContext(DbContextOptions<QuizOnlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonTopic> LessonTopics { get; set; }
    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<RegistrationStatus> RegistrationStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<TokenForgetPassword> TokenForgetPasswords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Package>()
        .ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<Lesson>()
        .ToTable(tb => tb.UseSqlOutputClause(false));

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CD9DB5C5FA");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAccount312752");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B47931DE62");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__6421F7BEF36EA832");

            entity.ToTable("Lesson");

            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.LessonContent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lesson_content");
            entity.Property(e => e.LessonName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lesson_name");
            entity.Property(e => e.LessonOrder).HasColumnName("lesson_order");
            entity.Property(e => e.LessonTopicId).HasColumnName("lesson_topic_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.Summary)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("summary");

            entity.HasOne(d => d.LessonTopic).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.LessonTopicId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FKLesson811441");


            entity.HasOne(d => d.Subject).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKLesson125338");
        });

        modelBuilder.Entity<LessonTopic>(entity =>
        {
            entity.HasKey(e => e.LessonTopicId).HasName("PK__Lesson_T__F92C2D0096E4A20B");

            entity.ToTable("Lesson_Topic");

            entity.Property(e => e.LessonTopicId).HasColumnName("lesson_topic_id");
            entity.Property(e => e.LessonTopicName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lesson_topic_name");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Subject).WithMany(p => p.LessonTopics)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Lesson_Topic_Subject");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Package__63846AE829E6F33B");

            entity.ToTable("Package");

            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.PackageName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("package_name");
            entity.Property(e => e.SalePrice).HasColumnName("salePrice");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Subject).WithMany(p => p.Packages)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FKPackage916382");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__22A298F6EFDDD144");

            entity.ToTable("Registration");

            entity.Property(e => e.RegistrationId).HasColumnName("registration_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.RegistrationTime)
                .HasColumnType("datetime")
                .HasColumnName("registration_time");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.ValidFrom).HasColumnName("valid_from");
            entity.Property(e => e.ValidTo).HasColumnName("valid_to");

            entity.HasOne(d => d.Account).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRegistrati648261");

            entity.HasOne(d => d.Package).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRegistrati736102");

            entity.HasOne(d => d.Status).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRegistrati334529");

            entity.HasOne(d => d.Subject).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRegistrati472927");
        });

        modelBuilder.Entity<RegistrationStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Registra__3683B531A9126D98");

            entity.ToTable("Registration_Status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC28176FF1");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__5004F660C4C1CAAD");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject_name");
            entity.Property(e => e.Tagline)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tagline");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("thumbnail");

            entity.HasOne(d => d.Account).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSubject564919");

            entity.HasOne(d => d.Category).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSubject483897");
        });

        modelBuilder.Entity<TokenForgetPassword>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TokenForgetPassword");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.ExpiryTime)
                .HasColumnType("datetime")
                .HasColumnName("expiryTime");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsUsed).HasColumnName("isUsed");
            entity.Property(e => e.Token)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("token");

            entity.HasOne(d => d.Account).WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTokenForge559928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
