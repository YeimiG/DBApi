using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemPenitentiary.Models;

public partial class UeUsocotContext : DbContext
{
    public UeUsocotContext()
    {
    }

    public UeUsocotContext(DbContextOptions<UeUsocotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animation> Animations { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Environment> Environments { get; set; }

    public virtual DbSet<Interaction> Interactions { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<UserProgress> UserProgresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animation>(entity =>
        {
            entity.HasKey(e => e.AnimationId).HasName("PK__animatio__4EE6C44F5C8AC6D1");

            entity.ToTable("animations");

            entity.HasIndex(e => e.AnimationName, "UQ__animatio__6463C9D9AB8B0F93").IsUnique();

            entity.HasIndex(e => e.AnimationUrl, "idx_animations_asset");

            entity.Property(e => e.AnimationId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("animation_id");
            entity.Property(e => e.AnimationName)
                .HasMaxLength(100)
                .HasColumnName("animation_name");
            entity.Property(e => e.AnimationType)
                .HasMaxLength(50)
                .HasColumnName("animation_type");
            entity.Property(e => e.AnimationUrl)
                .HasMaxLength(255)
                .HasColumnName("animation_url");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.ProcessId).HasColumnName("process_id");
            entity.Property(e => e.RequiresMaestroRole)
                .HasDefaultValue(false)
                .HasColumnName("requires_maestro_role");
            entity.Property(e => e.RequiresStudentRole)
                .HasDefaultValue(false)
                .HasColumnName("requires_student_role");

            entity.HasOne(d => d.Process).WithMany(p => p.Animations)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_animations_process");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__audit_lo__9E2397E0A95D977D");

            entity.ToTable("audit_log");

            entity.Property(e => e.LogId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("log_id");
            entity.Property(e => e.ActionDetails).HasColumnName("action_details");
            entity.Property(e => e.ActionTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("action_time");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .HasColumnName("action_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_audit_user");
        });

        modelBuilder.Entity<Environment>(entity =>
        {
            entity.HasKey(e => e.EnvironmentId).HasName("PK__environm__244BC568345DFF7D");

            entity.ToTable("environments");

            entity.HasIndex(e => e.EnvironmentName, "UQ__environm__52AC2909E92A8D92").IsUnique();

            entity.HasIndex(e => e.UnrealLevelPath, "idx_environments_level_path");

            entity.Property(e => e.EnvironmentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("environment_id");
            entity.Property(e => e.ConfigurationSettings).HasColumnName("configuration_settings");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.EnvironmentName)
                .HasMaxLength(100)
                .HasColumnName("environment_name");
            entity.Property(e => e.LastModifiedAt).HasColumnName("last_modified_at");
            entity.Property(e => e.ResponsibleUserId).HasColumnName("responsible_user_id");
            entity.Property(e => e.UnrealLevelPath)
                .HasMaxLength(255)
                .HasColumnName("unreal_level_path");

            entity.HasOne(d => d.ResponsibleUser).WithMany(p => p.Environments)
                .HasForeignKey(d => d.ResponsibleUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_environments_user");
        });

        modelBuilder.Entity<Interaction>(entity =>
        {
            entity.HasKey(e => e.InteractionId).HasName("PK__interact__605F8FE60BAB1551");

            entity.ToTable("interactions");

            entity.HasIndex(e => new { e.UserId, e.InteractionTime }, "idx_interactions_user_time");

            entity.Property(e => e.InteractionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("interaction_id");
            entity.Property(e => e.InteractionDetails).HasColumnName("interaction_details");
            entity.Property(e => e.InteractionTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("interaction_time");
            entity.Property(e => e.InteractionType)
                .HasMaxLength(50)
                .HasColumnName("interaction_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Interactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_interactions_user");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__notifica__E059842F9D1AADBC");

            entity.ToTable("notifications");

            entity.HasIndex(e => e.TargetUserId, "idx_notifications_target_user");

            entity.Property(e => e.NotificationId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("notification_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Delivered)
                .HasDefaultValue(false)
                .HasColumnName("delivered");
            entity.Property(e => e.NotificationData).HasColumnName("notification_data");
            entity.Property(e => e.NotificationType)
                .HasMaxLength(50)
                .HasColumnName("notification_type");
            entity.Property(e => e.TargetUserId).HasColumnName("target_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.TargetUser).WithMany(p => p.NotificationTargetUsers)
                .HasForeignKey(d => d.TargetUserId)
                .HasConstraintName("FK_notifications_target_user");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_notifications_user");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__permissi__E5331AFAD0DDB2C4");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("permission_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(50)
                .HasColumnName("permission_name");
        });

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__processe__9446C3E1AE3A11B0");

            entity.ToTable("processes");

            entity.HasIndex(e => e.ProcessName, "UQ__processe__A18241BC49CEE0F8").IsUnique();

            entity.Property(e => e.ProcessId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("process_id");
            entity.Property(e => e.Configuration).HasColumnName("configuration");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(100)
                .HasColumnName("process_name");
            entity.Property(e => e.TotalStages).HasColumnName("total_stages");
            entity.Property(e => e.UnrealAssetPath)
                .HasMaxLength(255)
                .HasColumnName("unreal_asset_path");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F774622BF");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E61649B30E04C").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC5727182F83E").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PermissionId }).HasName("PK__user_per__07ED06A010597442");

            entity.ToTable("user_permissions");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.GrantedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("granted_at");

            entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK_user_permissions_permission");

            entity.HasOne(d => d.User).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_permissions_user");
        });

        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.ProgressId).HasName("PK__user_pro__49B3D8C16C9EC114");

            entity.ToTable("user_progress");

            entity.HasIndex(e => new { e.UserId, e.ProcessId }, "idx_user_progress_user_process");

            entity.Property(e => e.ProgressId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("progress_id");
            entity.Property(e => e.CurrentStage)
                .HasMaxLength(100)
                .HasColumnName("current_stage");
            entity.Property(e => e.GameSpecificData).HasColumnName("game_specific_data");
            entity.Property(e => e.LastUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("last_updated_at");
            entity.Property(e => e.ProcessId).HasColumnName("process_id");
            entity.Property(e => e.ProcessPercentage).HasColumnName("process_percentage");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Process).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_progress_process");

            entity.HasOne(d => d.User).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_progress_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
