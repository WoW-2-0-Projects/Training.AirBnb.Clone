﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AirBnB.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AirBnB.Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<List<string>>("PhoneNumberCodes")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.GuestFeedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ListingId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("ListingId");

                    b.ToTable("GuestFeedbacks");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Locale")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Listing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("BuiltDate")
                        .HasColumnType("date");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DeletedByUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("HostId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.ListingCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSpecialCategory")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid>("StorageFileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("StorageFileId")
                        .IsUnique();

                    b.ToTable("ListingCategories");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.ListingCategoryAssociation", b =>
                {
                    b.Property<Guid>("ListingId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ListingCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("ListingId", "ListingCategoryId");

                    b.HasIndex("ListingCategoryId");

                    b.ToTable("ListingCategoryAssociations", (string)null);
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.ListingMediaFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ListingId")
                        .HasColumnType("uuid");

                    b.Property<byte>("OrderNumber")
                        .HasColumnType("smallint");

                    b.Property<Guid>("StorageFileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ListingId");

                    b.HasIndex("StorageFileId")
                        .IsUnique();

                    b.ToTable("ListingMediaFiles");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.NotificationHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ReceiverUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("NotificationHistories");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.NotificationTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1036288)
                        .HasColumnType("character varying(1036288)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TemplateType")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Type", "TemplateType")
                        .IsUnique();

                    b.ToTable("NotificationTemplates", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.StorageFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("StorageFiles");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEmailAddressVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.UserProfileMediaFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("StorageFileId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StorageFileId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfileMediaFiles");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.UserSettings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PreferredNotificationType")
                        .HasColumnType("integer");

                    b.Property<int>("PreferredTheme")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.VerificationCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<int>("CodeType")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("ExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("VerificationLink")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("VerificationCodes");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.EmailHistory", b =>
                {
                    b.HasBaseType("AirBnB.Domain.Entities.NotificationHistory");

                    b.Property<string>("ReceiverEmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderEmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.SmsHistory", b =>
                {
                    b.HasBaseType("AirBnB.Domain.Entities.NotificationHistory");

                    b.Property<string>("ReceiverPhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderPhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.EmailTemplate", b =>
                {
                    b.HasBaseType("AirBnB.Domain.Entities.NotificationTemplate");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.SmsTemplate", b =>
                {
                    b.HasBaseType("AirBnB.Domain.Entities.NotificationTemplate");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.UserInfoVerificationCode", b =>
                {
                    b.HasBaseType("AirBnB.Domain.Entities.VerificationCode");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.City", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Country", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirBnB.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.GuestFeedback", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.User", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirBnB.Domain.Entities.Listing", "Listing")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("AirBnB.Domain.Entities.Rating", "Rating", b1 =>
                        {
                            b1.Property<Guid>("GuestFeedbackId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Accuracy")
                                .HasColumnType("real");

                            b1.Property<float>("CheckIn")
                                .HasColumnType("real");

                            b1.Property<float>("Cleanliness")
                                .HasColumnType("real");

                            b1.Property<float>("Communication")
                                .HasColumnType("real");

                            b1.Property<float>("Location")
                                .HasColumnType("real");

                            b1.Property<float>("OverallRating")
                                .HasColumnType("real");

                            b1.Property<float>("Value")
                                .HasColumnType("real");

                            b1.HasKey("GuestFeedbackId");

                            b1.ToTable("GuestFeedbacks");

                            b1.WithOwner()
                                .HasForeignKey("GuestFeedbackId");
                        });

                    b.Navigation("Guest");

                    b.Navigation("Listing");

                    b.Navigation("Rating")
                        .IsRequired();
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Listing", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.User", "Host")
                        .WithMany("Listings")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("AirBnB.Domain.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("ListingId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasMaxLength(256)
                                .HasColumnType("character varying(256)");

                            b1.Property<Guid?>("CityId")
                                .HasColumnType("uuid");

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision");

                            b1.HasKey("ListingId");

                            b1.ToTable("Listings");

                            b1.WithOwner()
                                .HasForeignKey("ListingId");
                        });

                    b.OwnsOne("AirBnB.Domain.Entities.Rating", "Rating", b1 =>
                        {
                            b1.Property<Guid>("ListingId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Accuracy")
                                .HasColumnType("real");

                            b1.Property<float>("CheckIn")
                                .HasColumnType("real");

                            b1.Property<float>("Cleanliness")
                                .HasColumnType("real");

                            b1.Property<float>("Communication")
                                .HasColumnType("real");

                            b1.Property<float>("Location")
                                .HasColumnType("real");

                            b1.Property<float>("OverallRating")
                                .HasColumnType("real");

                            b1.Property<float>("Value")
                                .HasColumnType("real");

                            b1.HasKey("ListingId");

                            b1.ToTable("Listings");

                            b1.WithOwner()
                                .HasForeignKey("ListingId");
                        });

                    b.OwnsOne("AirBnB.Domain.Entities.Money", "PricePerNight", b1 =>
                        {
                            b1.Property<Guid>("ListingId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<Guid>("CurrencyId")
                                .HasColumnType("uuid");

                            b1.HasKey("ListingId");

                            b1.HasIndex("CurrencyId");

                            b1.ToTable("Listings");

                            b1.HasOne("AirBnB.Domain.Entities.Currency", "Currency")
                                .WithMany()
                                .HasForeignKey("CurrencyId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("ListingId");

                            b1.Navigation("Currency");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Host");

                    b.Navigation("PricePerNight")
                        .IsRequired();

                    b.Navigation("Rating")
                        .IsRequired();
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.ListingCategory", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.StorageFile", "ImageStorageFile")
                        .WithOne()
                        .HasForeignKey("AirBnB.Domain.Entities.ListingCategory", "StorageFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImageStorageFile");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.ListingCategoryAssociation", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.ListingCategory", "ListingCategory")
                        .WithMany()
                        .HasForeignKey("ListingCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirBnB.Domain.Entities.Listing", "Listing")
                        .WithMany()
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");

                    b.Navigation("ListingCategory");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.ListingMediaFile", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.Listing", "Listing")
                        .WithMany("ImagesStorageFile")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirBnB.Domain.Entities.StorageFile", "StorageFile")
                        .WithOne()
                        .HasForeignKey("AirBnB.Domain.Entities.ListingMediaFile", "StorageFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");

                    b.Navigation("StorageFile");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.NotificationHistory", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.NotificationTemplate", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.User", b =>
                {
                    b.OwnsOne("AirBnB.Domain.Entities.UserCredentials", "UserCredentials", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("PasswordHash")
                                .IsRequired()
                                .HasMaxLength(128)
                                .HasColumnType("character varying(128)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("UserCredentials")
                        .IsRequired();
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.UserProfileMediaFile", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.StorageFile", "StorageFile")
                        .WithOne()
                        .HasForeignKey("AirBnB.Domain.Entities.UserProfileMediaFile", "StorageFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirBnB.Domain.Entities.User", "User")
                        .WithOne("ProfilePicture")
                        .HasForeignKey("AirBnB.Domain.Entities.UserProfileMediaFile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StorageFile");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirBnB.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.UserSettings", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.User", "User")
                        .WithOne("UserSettings")
                        .HasForeignKey("AirBnB.Domain.Entities.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.UserInfoVerificationCode", b =>
                {
                    b.HasOne("AirBnB.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.Listing", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("ImagesStorageFile");
                });

            modelBuilder.Entity("AirBnB.Domain.Entities.User", b =>
                {
                    b.Navigation("Listings");

                    b.Navigation("ProfilePicture")
                        .IsRequired();

                    b.Navigation("UserSettings")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
