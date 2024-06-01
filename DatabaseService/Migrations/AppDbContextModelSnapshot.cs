﻿// <auto-generated />
using System;
using DatabaseService.AppContextModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DatabaseService.DataModels.Authentication.Login", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RefreshTokenExpires")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_Login");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Friends.Friend", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsUnfriend")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserIdOne")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserIdTwo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_Friends");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Friends.FriendBlocked", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("BlockedUserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_FriendsBlocked");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Friends.Friendships", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ApprovedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FriendShipStatus")
                        .HasColumnType("int");

                    b.Property<string>("ReceiverUserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SenderUserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_FriendShips");
                });

            modelBuilder.Entity("DatabaseService.DataModels.OtpLogs.OtpLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Otp")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("OtpExpires")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_OtpLog");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Posts.Posts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<int>("PostAccessType")
                        .HasColumnType("int");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_Posts");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Posts.PostsComments", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CommentId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CommentImagePath")
                        .HasColumnType("longtext");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_PostsComments");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Posts.PostsCommentsLiked", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("PostCommentsId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostsCommentsLikedId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_PostsCommentsLiked");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Posts.PostsLiked", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LikedId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_PostsLiked");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Posts.PostsMedia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("PostsId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostsMediaId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostsMediaPath")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_PostsMedia");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Posts.PostsRestricted", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("PostsId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostsSharedUserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostsUserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_PostsRestricted");
                });

            modelBuilder.Entity("DatabaseService.DataModels.Posts.PostsShared", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("OriginalPostId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostSharedId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_PostsShared");
                });

            modelBuilder.Entity("Users", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedUser")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("ModifiedUser")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileImagePath")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tbl_Users");
                });
#pragma warning restore 612, 618
        }
    }
}
